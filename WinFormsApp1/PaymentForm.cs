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

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Status", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Status();

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Check", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Check(string p_pszServer, string p_pszPort);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Create", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Create(string p_pszServer, string p_pszPort);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Destroy", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Destroy();

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_MkTranId", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_MkTranId(byte[] p_pszOutValueBuffer, int p_inOutValueBufferLen);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Init", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Init(string p_pszTranId, string p_pszMsgCmd, string p_pszMsgCert);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Term", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Term();

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Input", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Input(byte[] p_pszKey, string p_pszVal);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Input", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Input_char(char[] p_pszKey, string p_pszVal);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Execute", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Execute();

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_Output", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_Output(string p_pszKey, byte[] p_pszOutValueBuffer, int p_inOutValueBufferLen);

        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_GetNotifyMsg", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_GetNotifyMsg(byte[] p_pszOutNotifyMsgBuffer, int p_inOutNotifyMsgBufferLen);


        [DllImport(@"Win4POS\Win4POSDll.dll", EntryPoint = "FDK_WIN4POS_UserStop", CharSet = System.Runtime.InteropServices.CharSet.None)]
        private static extern int FDK_WIN4POS_UserStop();


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
            CostValue.Text = cos.ToString("N0");

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
            if (isProcessing) return; // 이미 실행 중이면 종료
            isProcessing = true; // 실행 중으로 설정

            try
            {
                //await Task.Run(() => btnCardAuth_Click(sender, e));
                isPaymentDone = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"[ERROR] 시리얼 통신 중 오류 발생: {ex.Message}");
                Console.WriteLine($"[ERROR] {ex.Message}");
                new MsgWindow("결제 실패").Show();
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
                //MessageBox.Show("Win4POS 체크 실패");
            }
            else
            {
                //MessageBox.Show("Win4POS 체크 성공");
            }
            //int iRet = -1;
            byte[] OutTranId = new byte[256];
            byte[] OutValue = new byte[256];
            string strBizNo = "1000000000";
            string strSerialNo = "1000000000";
            string strCost = cost.ToString();

            iRet = FDK_WIN4POS_Status();

            if (iRet != 0)
            {
                MessageBox.Show("Win4POS 실행 확인");
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

            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("세금"), "50");
            if (iRet != 0)
            {
                MessageBox.Show("Win4POS Input 실패");
                FDK_WIN4POS_Term();
                return;
            }

            //할부 ( 설정 할 경우 신용카드 창에서 입력 불가, 취소시 원거래 할부 개월수 입력 필수 )
            iRet = FDK_WIN4POS_Input(Encoding.GetEncoding("euc-kr").GetBytes("할부"), "03");
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


            ////////////////////////////////////////////////////////// 결제 로그 필요시 txtResult를 로그로 변환 후 사용 //////////////////////////////////////////

            //FDK_WIN4POS_Output("프린터출력유무", OutValue, 256);
            //txtResult.Text = "프린터출력유무 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("응답코드", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "응답코드 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("승인번호", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "승인번호 :" + Encoding.Default.GetString(OutValue);
            //txtAuthNo.Text = Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("승인일자", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "승인일자 :" + Encoding.Default.GetString(OutValue);
            //txtAuthDate.Text = Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("가맹점번호", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "가맹점번호 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("DDCFlag", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "DDCFlag :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("DDC전표번호", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "DDC전표번호 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("응답메시지1", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "응답메시지1 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("응답메시지2", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "응답메시지2 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("카드명", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "카드명 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("발급사코드", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "발급사코드 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("발급사명", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "발급사명 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("매입사코드", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "매입사코드 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("매입사명", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "매입사명 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("잔액", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "잔액 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("Notice", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "Notice :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("알림1", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "알림1 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("알림2", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "알림2 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("알림3", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "알림3 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("알림4", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "알림4 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("마스킹카드번호", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "마스킹카드번호 :" + Encoding.Default.GetString(OutValue);

            //FDK_WIN4POS_Output("할부", OutValue, 256);
            //txtResult.Text = txtResult.Text + Environment.NewLine + "할부 :" + Encoding.Default.GetString(OutValue);

            //MessageBox.Show("신용승인 종료");
            FDK_WIN4POS_Term();
        }
    }
}
