using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Collections;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Net;

namespace WinFormsApp1
{
    public partial class InvoicePrintForm : Form
    {
        private System.Windows.Forms.Timer autoCloseTimer;
        private System.Windows.Forms.Timer countdownTimer;
        private int remainingTime = 30; // 30초 카운트다운

        // 데이터
        private string receiverPhoneNo;
        private string receiverName;
        private string receiverAddress;
        private string receiverAddress2;
        private string receiverAddress3;
        private string senderPhoneNo;
        private string senderName;
        private string senderAddress;
        private string senderAddress2;
        private string senderAddress3;
        private string productCategory;
        private string productPriceInput;
        private string requestInputField;

        private string coastFormat;

        // request 데이터
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

        public InvoicePrintForm(int cos)
        {
            this.Opacity = 0;

            InitializeComponent();

            coastFormat = cos.ToString("N0");

            receiverPhoneNo = DataCtrl.ReceiverPhoneNo;
            receiverName = DataCtrl.ReceiverName;
            receiverAddress = DataCtrl.ReceiverAddress;
            receiverAddress2 = DataCtrl.ReceiverAddress2;
            receiverAddress3 = DataCtrl.ReceiverAddress3;

            senderPhoneNo = DataCtrl.SenderPhoneNo;
            senderName = DataCtrl.SenderName;
            senderAddress = DataCtrl.SenderAddress;
            senderAddress2 = DataCtrl.SenderAddress2;
            senderAddress3 = DataCtrl.SenderAddress3;

            productCategory = DataCtrl.ProductCategory;
            productPriceInput = DataCtrl.ProductPriceInput;
            requestInputField = DataCtrl.RequestInputField;

            this.Show();

            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                this.Opacity = 1; // 투명도 복원
            };

            Task.Delay(1000).Wait();
            HanjinAPI();
            
            // 30초 후에 Go_Home 메서드 호출을 위한 타이머 설정
            autoCloseTimer = new System.Windows.Forms.Timer();
            autoCloseTimer.Interval = 30000; // 30초 (30000 밀리초)
            autoCloseTimer.Tick += AutoCloseTimer_Tick;
            autoCloseTimer.Start();

            
        }
        private void InitializeCountdownTimer()
        {
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000; // 1초 간격
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownLabel.TextAlign = ContentAlignment.MiddleCenter; // 가운데 정렬
            countdownLabel.Text = remainingTime.ToString();
            countdownTimer.Start();
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
                countdownLabel.Text = remainingTime.ToString();
            }
            else
            {
                countdownTimer.Stop();
                // 시간이 다 되면 추가 작업 수행 (예: 홈으로 이동)
                Go_Home(sender, e);
            }
        }
        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            autoCloseTimer.Stop();
            Go_Home(sender, e);
        }

        public Point UpdateLocationBySize(Size parentSize, object target)
        {
            Label label = (Label)target;
            int posX = parentSize.Width / 2 - label.Width / 2;
            int posY = parentSize.Height / 2 - label.Height / 2;
            Point res = new Point(posX, posY);

            return res;
        }

        private async void HanjinAPI()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiKey = "rVz901GWhd1GNnbU04Hov4dUHsXK1WwB1SNoWRKX"; // API 키 사용
                    string requestUrl = $"https://ebbapd.hjt.co.kr/v1/wbl/SMARTCU/print-wbl";

                    // 헤더에 x-api-key 추가
                    client.DefaultRequestHeaders.Add("x-api-key", apiKey);

                    clientId = "SMARTCU";
                    csrNum = "9117159";
                    svcDiv = "GN";
                    request_address = "서울시 중구 소공로 88 한진빌딩 신관 9 층";
                    sndZip = "04532";
                    rcvZip = "04532";
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
                        wblNumDashFormat = wblNum.Insert(4, "-").Insert(9, "-");

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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                new MsgWindow(ex.Message).Show();
            }


            ShowPrinterWindow();

            // 카운트다운 타이머 초기화
            InitializeCountdownTimer();
        }

        public void ShowPrinterWindow()
        {
            // device.json 경로 설정
            string deviceJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appassets", "device.json");

            // device.json 파일에서 COM 포트 읽기
            string jsonContent = File.ReadAllText(deviceJsonPath);
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            string comPort = jsonDoc.RootElement.GetProperty("InvoicePrinter").GetProperty("port").GetString();

            // 시리얼 포트 설정
            SerialPort serialPort = new SerialPort(comPort, 115200);

            try
            {
                serialPort.Open();

                // ZPL 코드 생성
                string zplCode = $@"^XA
    ^POI^PW984^LL984    ; 123mm = 984dots, 100mm = 800dots (8dots/mm)

    ^SC0,1
    ^CI28
    ^CW1,E:KFONT.TTF^FO25,1140

    ; 운송장번호 (상단) 9.wbl_num
    ^FO760,130^A0R,40,40^FD{wblNumDashFormat}^FS

    ; 메인 식별자 (CA 174 F) 및 번호 1.hub_cod 2.tml_cod 4.dom_mid
    ^FO620,30^A0R,120,120^FD{hubCod} {tmlCod} {domMid}^FS

    ; 배송사원분류코드 16.es_cod, 배송사원명 11 es_nam
    ^FO680,530^A0R,60,70^FD{esCod}^FS
    ^FO620,500^A0R,55,55^FD{esNam}^FS

    ; 소분류 코드 - 배송사원 grp_rnk
    ^FO650,710^A0R,70,90^FD{grpRnk}^FS

    ; 제주/도서 구분 15.dom_rgn
    ^FO620,850^GB50,120,5^FS
    ^FO625,865^A0R,30,30^FD{domRgn}^FS

    ; 도착지 집배점코드 5.cen_cod, 도착지 집배점명 6.cen_nam
    ^FO620,680^A0R,25,25^FD{cenCod} {cenNam}^FS

    ; 발착 정보 s_tml_cod 7s_tml_nam
    ^FO620,50^A0R,20,20^FD발지:{sTmlCod} {sTmlNam}^FS

    ; 수령인 정보
    ^FO570,450^A0R,30,30^FD{receiverPhoneNo}^FS
    ^FO570,50^A0R,30,30^FD{receiverName}^FS
    ^FO535,50^A0R,25,25rcmd^FD{receiverAddress2}^FS
    ^FO480,50^A0R,40,40^FD{receiverAddress3}^FS

    ; 바코드 : 도착지 터미널 바코드 (세로 방향, 크기 조정) tml_cod
    ^FO530,730^BY3
    ^BCR,70,N,N,N
    ^FD{tmlCod}^FS

    ; 운임지급 기준 박스
    ^FO460,700^GB50,270,5^FS
    ^FO465,740^A0R,35,35^FD선불 {coastFormat}원^FS

    ; 송하인 정보
    ^FO420,450^A0R,25,25^FD{senderPhoneNo}^FS
    ^FO425,50^A0R,20,20^FD{senderName}^FS
    ^FO400,50^A0R,20,20^FD{senderAddress2} {senderAddress3}^FS

    ; 물품 정보 (FS형)
    ^FO310,10^A0R,50,50^FD분류 : {productCategory}^FS
    ^FO225,10^A0R,40,40^FD물품가액 : {productPriceInput} 원^FS

    ; 바코드 (세로 방향, 크기 조정) wbl_num
    ^FO60,600^BY2
    ^BCR,120,N,N,N
    ^FD{wblNum}^FS

    ; 바코드 하단 숫자 포맷 9.wbl_num
    ^FO20,680^A0R,30,30^FD{wblNumDashFormat}^FS

    ; 하단 배송메세지 (작은 폰트) 14.
    ^FO90,20^A0R,25,25^FD{requestInputField}^FS

    ^XZ";

                // ZPL 코드 전송 (UTF-8 인코딩)
                byte[] zplBytes = Encoding.UTF8.GetBytes(zplCode);
                serialPort.Write(zplBytes, 0, zplBytes.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"프린터 통신 중 오류가 발생했습니다: {ex.Message}");
            }
            finally
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }

            // 추가 동작 수행
            Task.Delay(2000).Wait();
        }


        public void Go_Home(object sender, EventArgs e)
        {
            if (!MsgWindow.IsShowing)
            {
                countdownTimer.Stop();
                DataCtrl.ClearAll();
                new HomeForm().Show();
                Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                // 마우스 커서 위치 초기화
                Cursor.Position = new System.Drawing.Point(0, 300);
            }
        }
    }
}
