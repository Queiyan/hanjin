using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.ComponentModel.Com2Interop;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Security.Cryptography;

namespace WinFormsApp1
{
    public partial class PaymentForm : Form
    {

        private uint message;
        private IntPtr handle;
        public const uint HWND_BROADCAST = 0xffff;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern uint RegisterWindowMessage(string lpString);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Status", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Status();

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Check", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Check(string p_pszServer, string p_pszPort);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Create", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Create(string p_pszServer, string p_pszPort);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Destroy", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Destroy();

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_MkTranId", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_MkTranId(byte[] p_pszOutValueBuffer, int p_inOutValueBufferLen);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Init", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Init(string p_pszTranId, string p_pszMsgCmd, string p_pszMsgCert);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Term", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Term();

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Input", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Input(byte[] p_pszKey, string p_pszVal);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Input", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Input_char(char[] p_pszKey, string p_pszVal);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Execute", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Execute();

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Output", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Output(string p_pszKey, byte[] p_pszOutValueBuffer, int p_inOutValueBufferLen);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_GetNotifyMsg", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_GetNotifyMsg(byte[] p_pszOutNotifyMsgBuffer, int p_inOutNotifyMsgBufferLen);

        [DllImport(@"C:\Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_UserStop", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_UserStop();

        // 송장 request 데이터
        private string clientId;
        private string csrNum;
        private string svcDiv;
        private string request_address;
        private string sndZip;
        private string rcvZip;
        private string request_msgKey;

        // 한진 API 응답 데이터
        private string resultCode;
        private string resultMessage;
        private string msgKey;
        private string sTmlNam;
        private string sTmlCod;
        private string zipCod;
        private string tmlNam;
        private string tmlCod;
        private string cenNam;
        private string cenCod;
        private string pdTim;
        private string domRgn;
        private string hubCod;
        private string domMid;
        private string grpRnk;
        private string esNam;
        private string esCod;
        private string prtAdd;
        private string dlvTyp;
        private string svcNam;
        private string ptnSrt;
        private string srtNam;
        private string wblNum;

        // 응답 데이터 문자포매팅
        private string wblNumDashFormat;

        public bool isPaymentDone = false;
        public double weight = 0;
        public double boxLength = 0;
        public static int cost = 0;
        private bool isProcessing = false; // 결제버튼 플래그

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public PaymentForm(double boxLen, int cos)
        {
            // euc-kr 인코딩 제공자 등록 (.NET 5 이상일 때 사용)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.Opacity = 0;
            boxLength = boxLen;
            InitializeComponent();
            cost = cos;

            HanjinPrintAPI();

            this.Show();

            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                //this.BringToFront(); // 폼을 맨 앞으로 가져옴
                this.Opacity = 1; // 투명도 복원
            };

            InitializeInactivityHandler();
        }

        ////////////////////////////////////////////////////////////60초 뒤 홈 화면으로 이동하는 타이머 초기화//////////////////////////////////////////////////////////////////
        // 
        private void InitializeInactivityHandler()
        {
            InitializeInactivityTimer();
            HookUserActivityEvents();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 초기화
        private void InitializeInactivityTimer()
        {
            inactivityTimer = new System.Timers.Timer(60000); // 60초
            inactivityTimer.Elapsed += InactivityTimer_Elapsed;
            inactivityTimer.AutoReset = false;
            inactivityTimer.Start();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 이벤트 핸들러
        private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Go_Home(this, EventArgs.Empty);
            });
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 재설정
        private void ResetInactivityTimer()
        {
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
                inactivityTimer.Start();
            }
        }

        // 사용자 활동 이벤트 후킹
        private void HookUserActivityEvents()
        {
            this.MouseMove += UserActivity;
            this.KeyPress += UserActivity;
            foreach (Control control in this.Controls)
            {
                control.MouseMove += UserActivity;
                control.KeyPress += UserActivity;
            }
        }

        // 사용자 활동 이벤트 핸들러
        private void UserActivity(object sender, EventArgs e)
        {
            ResetInactivityTimer();
        }
        // 폼 활성화 이벤트 핸들러
        private void Form_Activated(object sender, EventArgs e)
        {
            ResetInactivityTimer();
        }

        // 폼 비활성화 이벤트 핸들러
        private void Form_Deactivate(object sender, EventArgs e)
        {
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
            }
        }
        ////////////////////////////////////////////////////////////////////60초 뒤 홈 화면으로 이동하는 타이머 초기화//////////////////////////////////////////////////////////


        public void Go_Home(object sender, EventArgs e)
        {
            DataCtrl.ClearAll();
            HomeForm home = new HomeForm();
            home.Show();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            VolumeResultForm weightsResult = new VolumeResultForm(DataCtrl.BoxWidth, DataCtrl.BoxDepth, DataCtrl.BoxHeight);
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async void Go_Next(object sender, EventArgs e)
        {
            // 결제 중인 경우 중복 실행 방지
            if (isProcessing) return; // 이미 실행 중이면 종료
            isProcessing = true; // 실행 중으로 설정
            ///////////////////////////////////////////////결제 처리////////////////////////////////////////////////////
            try
            {
                await Task.Run(() => btnCardAuth_Click(sender, e));
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"[ERROR] 시리얼 통신 중 오류 발생: {ex.Message}");
                Console.WriteLine($"[ERROR] {ex.Message}");
                new MsgWindow("결제 실패\n다시 시도 해주세요").Show();
            }

            // 결제 완료 시 다음 단계로 진행
            if (isPaymentDone)
            {
                isPaymentDone = false;
                InvoicePrintForm invoiceForm = new InvoicePrintForm(cost);
                Task.Delay(8000).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                // 마우스 커서 위치 초기화
                Cursor.Position = new System.Drawing.Point(0, 300);
            }
        }

        private async void HanjinPrintAPI()
        {
            //  한진 송장 출력 API 호출
            using (HttpClient client = new HttpClient())
            {
                string apiKey = "rVz901GWhd1GNnbU04Hov4dUHsXK1WwB1SNoWRKX";
                string requestUrl = $"https://ebbapd.hjt.co.kr/v1/wbl/SMARTCU/print-wbl";

                client.DefaultRequestHeaders.Add("x-api-key", apiKey);

                clientId = "SMARTCU";
                csrNum = "9117159";
                svcDiv = "GN";
                request_address = DataCtrl.ReceiverAddress2 + " " + DataCtrl.ReceiverAddress3;
                sndZip = DataCtrl.SenderAddress;
                rcvZip = DataCtrl.ReceiverAddress;
                request_msgKey = "00001";

                var data = new
                {
                    client_id = clientId,
                    csr_num = csrNum,
                    svc_div = svcDiv,
                    address = request_address,
                    snd_zip = sndZip,
                    rcv_zip = rcvZip,
                    msg_key = request_msgKey
                };

                try
                {
                    string json = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(requestUrl, content);
                    response.EnsureSuccessStatusCode();

                    string responseText = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseText);

                    // JSON 데이터 파싱
                    resultCode = jsonResponse["result_code"]?.ToString() ?? "결과 없음";
                    resultMessage = jsonResponse["result_message"]?.ToString() ?? "결과 없음";
                    msgKey = jsonResponse["msg_key"]?.ToString() ?? "";
                    sTmlNam = jsonResponse["s_tml_nam"]?.ToString() ?? "";
                    sTmlCod = jsonResponse["s_tml_cod"]?.ToString() ?? "";
                    zipCod = jsonResponse["zip_cod"]?.ToString() ?? "";
                    tmlNam = jsonResponse["tml_nam"]?.ToString() ?? "";
                    tmlCod = jsonResponse["tml_cod"]?.ToString() ?? "";
                    cenNam = jsonResponse["cen_nam"]?.ToString() ?? "";
                    cenCod = jsonResponse["cen_cod"]?.ToString() ?? "";
                    pdTim = jsonResponse["pd_tim"]?.ToString() ?? "";
                    domRgn = jsonResponse["dom_rgn"]?.ToString() ?? "";
                    hubCod = jsonResponse["hub_cod"]?.ToString() ?? "";
                    domMid = jsonResponse["dom_mid"]?.ToString() ?? "";
                    grpRnk = jsonResponse["grp_rnk"]?.ToString() ?? "";
                    esNam = jsonResponse["es_nam"]?.ToString() ?? "";
                    esCod = jsonResponse["es_cod"]?.ToString() ?? "";
                    prtAdd = jsonResponse["prt_add"]?.ToString() ?? "";
                    dlvTyp = jsonResponse["dlv_typ"]?.ToString() ?? "";
                    svcNam = jsonResponse["svc_nam"]?.ToString() ?? "";
                    ptnSrt = jsonResponse["ptn_srt"]?.ToString() ?? "";
                    srtNam = jsonResponse["srt_nam"]?.ToString() ?? "";
                    wblNum = jsonResponse["wbl_num"]?.ToString() ?? "";
                    

                    // VoiceDataCtrl에 데이터 저장
                    VoiceDataCtrl.SaveHanjinApiResponse(
                        resultCode, resultMessage, msgKey, sTmlNam, sTmlCod, zipCod, tmlNam, tmlCod,
                        cenNam, cenCod, pdTim, domRgn, hubCod, domMid, grpRnk, esNam, esCod, prtAdd,
                        dlvTyp, svcNam, ptnSrt, srtNam, wblNum);

                    if (!resultCode.Equals("OK"))
                    {
                        new MsgWindow("API 통신 실패").Show();
                    }
                    Console.WriteLine($"Result Code: {resultCode}\nResult Message: {resultMessage}\nMsg Key: {msgKey}\nS Tml Nam: {sTmlNam}\nS Tml Cod: {sTmlCod}\nZip Cod: {zipCod}\nTml Nam: {tmlNam}\nTml Cod: {tmlCod}\nCen Nam: {cenNam}\nCen Cod: {cenCod}\nPd Tim: {pdTim}\nDom Rgn: {domRgn}\nHub Cod: {hubCod}\nDom Mid: {domMid}\nGrp Rnk: {grpRnk}\nEs Nam: {esNam}\nEs Cod: {esCod}\nPrt Add: {prtAdd}\nDlv Typ: {dlvTyp}\nSvc Nam: {svcNam}\nPtn Srt: {ptnSrt}\nSrt Nam: {srtNam}\nWbl Num: {wblNum}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    new MsgWindow(ex.Message).Show();
                }

                Console.WriteLine($"domRgn: {domRgn}");
                Console.WriteLine($"cost: {cost}");

                // 제주권역 추가요금
                if (domRgn == "7")
                {
                    DataCtrl.ETC = 3000;
                    cost += 3000; // 추가요금 정해지면 변경
                }

                // 도서지역 추가요금
                if (domRgn == "9")
                {
                    DataCtrl.ETC = 5000;
                    cost += 5000; // 추가요금 정해지면 변경
                }

                // 요금 문자 포맷팅, 화면에 표시
                CostValue.Text = cost.ToString("N0");
            }
        }
        private void PaymentForm_Load(object sender, EventArgs e)
        {
        }
        private void CostValue_TextChanged(object sender, EventArgs e)
        {
        }
        private void btnCardAuth_Click(object sender, EventArgs e)
        {
            int iRet = FDK_WIN4POS_Check("127.0.0.1", "2003");
            if (iRet != 0 && iRet != 1)
            {
                Console.WriteLine($"Win4POS 체크 실패 FDK_WIN4POS_Check 반환 값: {iRet}");
                //MessageBox.Show("Win4POS 체크 실패");
            }
            else
            {
                Console.WriteLine($"Win4POS 체크 성공 FDK_WIN4POS_Check 반환 값: {iRet}");
                //MessageBox.Show("Win4POS 체크 성공");
            }
            byte[] OutTranId = new byte[256];
            byte[] OutValue = new byte[256];
            string strBizNo = "2048629073";
            string strSerialNo = "VR00427377";

            // 요금 문자변환
            string strCost = cost.ToString();

            Console.WriteLine($"strCost: {strCost}");

            iRet = FDK_WIN4POS_Status();

            if (iRet != 0)
            {
                MessageBox.Show("Win4POS 실행 확인 iRet = " + iRet);
                return;
            }

            iRet = FDK_WIN4POS_MkTranId(OutTranId, 50);

            if (iRet != 0)
            {
                MessageBox.Show("고유번호 확인 실패");
                return;
            }

            //결제처리 호출 초기화
            iRet = FDK_WIN4POS_Init(Encoding.GetEncoding("euc-kr").GetString(OutTranId), "PAY", "FDK");


            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Init 실패");
                FDK_WIN4POS_Term();
                return;
            }

            //신용승인 화면 호출
            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("거래구분"), "CRD");
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("사업자번호"), strBizNo);
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("제품일련번호"), strSerialNo);
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("요청금액"), strCost);
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("봉사료"), "0");
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("세금"), "0");
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            //할부 ( 설정 할 경우 신용카드 창에서 입력 불가, 취소시 원거래 할부 개월수 입력 필수 )
            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("할부"), "0");
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            iRet = FDK_WIN4POS_Execute();
            Console.WriteLine($"FDK_WIN4POS_Execute 반환 값: {iRet}");
            if (iRet != 0 && iRet != -203)
            {
                //MessageBox.Show("Win4POS Execute 실패");
                FDK_WIN4POS_Term();
                return;
            }


            ////////////////////////////////////////////////////////// 결제 로그  //////////////////////////////////////////

            FDK_WIN4POS_Output("프린터출력유무", OutValue, 256);
            Console.WriteLine("프린터출력유무 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("응답코드", OutValue, 256);
            Console.WriteLine("응답코드 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("승인번호", OutValue, 256);
            Console.WriteLine("승인번호 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("승인일자", OutValue, 256);
            Console.WriteLine("승인일자 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("가맹점번호", OutValue, 256);
            Console.WriteLine("가맹점번호 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("DDCFlag", OutValue, 256);
            Console.WriteLine("DDCFlag :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("DDC전표번호", OutValue, 256);
            Console.WriteLine("DDC전표번호 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("응답메시지1", OutValue, 256);
            Console.WriteLine("응답메시지1 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("응답메시지2", OutValue, 256);
            Console.WriteLine("응답메시지2 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("카드명", OutValue, 256);
            Console.WriteLine("카드명 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("발급사코드", OutValue, 256);
            Console.WriteLine("발급사코드 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("발급사명", OutValue, 256);
            Console.WriteLine("발급사명 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("매입사코드", OutValue, 256);
            Console.WriteLine("매입사코드 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("매입사명", OutValue, 256);
            Console.WriteLine("매입사명 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("잔액", OutValue, 256);
            Console.WriteLine("잔액 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("Notice", OutValue, 256);
            Console.WriteLine("Notice :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("알림1", OutValue, 256);
            Console.WriteLine("알림1 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("알림2", OutValue, 256);
            Console.WriteLine("알림2 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("알림3", OutValue, 256);
            Console.WriteLine("알림3 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("알림4", OutValue, 256);
            Console.WriteLine("알림4 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("마스킹카드번호", OutValue, 256);
            Console.WriteLine("마스킹카드번호 :" + Encoding.Default.GetString(OutValue));

            FDK_WIN4POS_Output("할부", OutValue, 256);
            Console.WriteLine("할부 :" + Encoding.Default.GetString(OutValue));
            //MessageBox.Show("신용승인 종료");
            FDK_WIN4POS_Term();

            if (iRet == 0)
            {
                isPaymentDone = true;
            }
            else
            {
                isPaymentDone = false;
            }
        }

        /// <summary>
        /// // 개발자 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devBtn_Click(object sender, EventArgs e)
        {
            InvoicePrintForm invoiceForm = new InvoicePrintForm(cost);
            Task.Delay(8000).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            // 마우스 커서 위치 초기화
            Cursor.Position = new System.Drawing.Point(0, 300);
        }
    }
}
