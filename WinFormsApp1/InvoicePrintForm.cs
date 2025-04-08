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
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class InvoicePrintForm : Form
    {
        private System.Windows.Forms.Timer autoCloseTimer;
        private System.Windows.Forms.Timer countdownTimer;
        private int remainingTime = 30; // 30초 카운트다운


        private string coastFormat;

        string wblNumDashFormat = VoiceDataCtrl.WblNum.Insert(4, "-").Insert(9, "-");

        // 키오스크 식별번호 추후 ini파일로 설정
        private string stackAreaCode = "NHJ063800001";

        

        public InvoicePrintForm(int cos)
        {
            this.Opacity = 0;

            InitializeComponent();

            coastFormat = cos.ToString("N0");

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
                // 1. 라커 예약 처리
                await ProcessLockerReservation();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                new MsgWindow(ex.Message).Show();
            }

            ShowPrinterWindow();
            InitializeCountdownTimer();
        }

        // 라커 예약 처리를 위한 새로운 메서드들
        private async Task ProcessLockerReservation()
        {
            try
            {
                // TLS 1.2 설정
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string regId = await SaveToLocalDb();
                Console.WriteLine($"데이터베이스 저장 완료. RegId: {regId}");

                bool success = await SendParcelReservation(regId);
                Console.WriteLine($"예약 결과: {success}");

                if (!success)
                {
                    throw new Exception("예약 API가 실패 응답을 반환했습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예약 처리 상세 오류: {ex.Message}");
                Console.WriteLine($"스택 트레이스: {ex.StackTrace}");
                throw new Exception($"예약 처리 실패: {ex.Message}");
            }
        }

        private async Task<string> SaveToLocalDb()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=58.229.240.39,1433;Database=HANJIN_PARCEL_LOCKER;User Id=SMTUser;Password=SMTUserPass;TrustServerCertificate=True;Encrypt=True;TrustServerCertificate=True;"))
                {
                    await conn.OpenAsync();
                    Console.WriteLine("데이터베이스 연결 성공");

                    string query = @"
                        INSERT INTO tblParcelSendInfo (
                            IsMember, UserID, SenderName, SenderMobileNo, SenderPostCode, 
                            SenderAddressMain, SenderAddressDetail, SenderComment, RecvName, 
                            RecvMobileNo, RecvPostCode, RecvAddressMain, 
                            RecvAddressDetail, GOD_NAM, TRN_FRE, ETC_FRE, EXT_FRE, InvoiceTrackNo,
                            CreateDate, BoxSize, StackAreaCode, StackBoxNo, CreditNo
                        ) VALUES (
                            'N', @UserID, @SenderName, @SenderMobileNo, @SenderPostCode, 
                            @SenderAddressMain, @SenderAddressDetail, @SenderComment, @RecvName, 
                            @RecvMobileNo, @RecvPostCode, @RecvAddressMain, 
                            @RecvAddressDetail, @GOD_NAM, @TRN_FRE, @ETC_FRE, @EXT_FRE, @InvoiceTrackNo,
                            GETDATE(), @BoxSize, @StackAreaCode, @StackBoxNo, @CreditNo
                        );
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", DataCtrl.SenderPhoneNo);
                        cmd.Parameters.AddWithValue("@SenderName", DataCtrl.SenderName);
                        cmd.Parameters.AddWithValue("@SenderMobileNo", DataCtrl.SenderPhoneNo);
                        cmd.Parameters.AddWithValue("@SenderPostCode", DataCtrl.SenderAddress);
                        cmd.Parameters.AddWithValue("@SenderAddressMain", DataCtrl.SenderAddress2);
                        cmd.Parameters.AddWithValue("@SenderAddressDetail", DataCtrl.SenderAddress3);
                        cmd.Parameters.AddWithValue("@SenderComment", DataCtrl.RequestInputField);
                        cmd.Parameters.AddWithValue("@RecvName", DataCtrl.ReceiverName);
                        cmd.Parameters.AddWithValue("@RecvMobileNo", DataCtrl.ReceiverPhoneNo);
                        cmd.Parameters.AddWithValue("@RecvPostCode", DataCtrl.ReceiverAddress);
                        cmd.Parameters.AddWithValue("@RecvAddressMain", DataCtrl.ReceiverAddress2);
                        cmd.Parameters.AddWithValue("@RecvAddressDetail", DataCtrl.ReceiverAddress3);
                        cmd.Parameters.AddWithValue("@GOD_NAM", DataCtrl.ProductCategory);
                        cmd.Parameters.AddWithValue("@TRN_FRE", 0);
                        cmd.Parameters.AddWithValue("@ETC_FRE", 0);
                        cmd.Parameters.AddWithValue("@EXT_FRE", 0);
                        cmd.Parameters.AddWithValue("@InvoiceTrackNo", VoiceDataCtrl.WblNum);
                        cmd.Parameters.AddWithValue("@BoxSize", "M");
                        cmd.Parameters.AddWithValue("@StackAreaCode", stackAreaCode);
                        cmd.Parameters.AddWithValue("@StackBoxNo", 1);
                        cmd.Parameters.AddWithValue("@CreditNo", "5469086");

                        Console.WriteLine("데이터 저장 시도...");
                        var result = await cmd.ExecuteScalarAsync();
                        Console.WriteLine($"데이터 저장 결과: {result}");

                        if (result == null)
                        {
                            throw new Exception("데이터 저장 실패: SCOPE_IDENTITY()가 null을 반환했습니다.");
                        }

                        return result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"데이터베이스 저장 상세 오류: {ex.Message}");
                Console.WriteLine($"스택 트레이스: {ex.StackTrace}");
                throw;
            }
        }

        private async Task<bool> SendParcelReservation(string regId)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    // SSL 인증서 검증 우회
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        // TLS 1.2 설정
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        // 기본 헤더 설정
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        // 요청 데이터 구성
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("field", "H"),
                            new KeyValuePair<string, string>("src", "3"),
                            new KeyValuePair<string, string>("regId", regId)
                        });

                        Console.WriteLine($"라커 예약 요청 데이터: field=H&src=0&regId={regId}");

                        var response = await client.PostAsync("https://58.229.240.39/hanjin_parcel/dispatch_parcel.php", content);
                        var result = await response.Content.ReadAsStringAsync();

                        Console.WriteLine($"라커 예약 API 응답 상태 코드: {response.StatusCode}");
                        Console.WriteLine($"라커 예약 API 응답 헤더: {string.Join(", ", response.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");
                        Console.WriteLine($"라커 예약 API 응답: {result}");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"API 호출 실패: {response.StatusCode} - {result}");
                        }

                        // 응답 결과 확인
                        result = result.Trim();
                        Console.WriteLine($"처리된 응답 결과: {result}");

                        // 다양한 성공 응답 패턴 확인
                        if (result.Equals("Ok", StringComparison.OrdinalIgnoreCase) ||
                            result.Equals("OK", StringComparison.OrdinalIgnoreCase) ||
                            result.Equals("ok", StringComparison.OrdinalIgnoreCase) ||
                            result.Equals("success", StringComparison.OrdinalIgnoreCase) ||
                            result.Equals("Success", StringComparison.OrdinalIgnoreCase) ||
                            result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }

                        // 실패 시 상세 정보 로깅
                        Console.WriteLine($"예약 실패 - 응답 내용: {result}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"라커 예약 API 호출 상세 오류: {ex.Message}");
                Console.WriteLine($"스택 트레이스: {ex.StackTrace}");
                throw;
            }
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
    ^FO620,30^A0R,120,120^FD{VoiceDataCtrl.HubCod} {VoiceDataCtrl.TmlCod} {VoiceDataCtrl.DomMid}^FS

    ; 배송사원분류코드 16.es_cod, 배송사원명 11 es_nam
    ^FO680,530^A0R,60,70^FD{VoiceDataCtrl.EsCod}^FS
    ^FO620,500^A0R,55,55^FD{VoiceDataCtrl.EsNam}^FS

    ; 소분류 코드 - 배송사원 grp_rnk
    ^FO650,710^A0R,70,90^FD{VoiceDataCtrl.GrpRnk}^FS

    ; 제주/도서 구분 15.dom_rgn
    ^FO620,850^GB50,120,5^FS
    ^FO625,865^A0R,30,30^FD{VoiceDataCtrl.DomRgn}^FS

    ; 도착지 집배점코드 5.cen_cod, 도착지 집배점명 6.cen_nam
    ^FO620,680^A0R,25,25^FD{VoiceDataCtrl.CenCod} {VoiceDataCtrl.CenNam}^FS

    ; 발착 정보 s_tml_cod 7s_tml_nam
    ^FO620,50^A0R,20,20^FD발지:{VoiceDataCtrl.STmlCod} {VoiceDataCtrl.STmlNam}^FS

    ; 수령인 정보
    ^FO570,450^A0R,30,30^FD{DataCtrl.ReceiverPhoneNo}^FS
    ^FO570,50^A0R,30,30^FD{DataCtrl.ReceiverName}^FS
    ^FO535,50^A0R,25,25rcmd^FD{DataCtrl.ReceiverAddress2}^FS
    ^FO480,50^A0R,40,40^FD{DataCtrl.ReceiverAddress3}^FS

    ; 바코드 : 도착지 터미널 바코드 (세로 방향, 크기 조정) tml_cod
    ^FO530,730^BY3
    ^BCR,70,N,N,N
    ^FD{VoiceDataCtrl.TmlCod}^FS

    ; 운임지급 기준 박스
    ^FO460,700^GB50,270,5^FS
    ^FO465,740^A0R,35,35^FD선불 {coastFormat}원^FS

    ; 송하인 정보
    ^FO420,450^A0R,25,25^FD{DataCtrl.SenderPhoneNo}^FS
    ^FO425,50^A0R,20,20^FD{DataCtrl.SenderName}^FS
    ^FO400,50^A0R,20,20^FD{DataCtrl.SenderAddress2} {DataCtrl.SenderAddress3}^FS

    ; 바코드 (세로 방향, 크기 조정) wbl_num
    ^FO60,600^BY2
    ^BCR,120,N,N,N
    ^FD{VoiceDataCtrl.WblNum}^FS

    ; 바코드 하단 숫자 포맷 9.wbl_num
    ^FO20,680^A0R,30,30^FD{wblNumDashFormat}^FS

    ; 하단 배송메세지 (작은 폰트) 14.
    ^FO90,20^A0R,25,25^FD{DataCtrl.RequestInputField}^FS

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
                autoCloseTimer.Stop();
                countdownTimer.Stop();
                DataCtrl.ClearAll();
                VoiceDataCtrl.ClearAll();
                new HomeForm().Show();
                Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                // 마우스 커서 위치 초기화
                Cursor.Position = new System.Drawing.Point(0, 300);
            }
        }
    }
}
