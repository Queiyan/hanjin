using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using AxWMPLib;
using Microsoft.VisualBasic.Devices;

namespace WinFormsApp1
{
    public partial class ReservationInfo : Form
    {
        private NumpadForm numpad;

        private System.Timers.Timer holdDeleteTimer;
        private const int DeleteInterval = 200; // 연속 삭제 간격 (밀리초)

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        ReservationResponse response = new ReservationResponse();

        public ReservationInfo(ReservationResponse response)
        {
            this.response = response;

            InitializeComponent();
            this.Opacity = 0; // 초기 투명도 설정

            this.Show();

            ReservationResponse_Load();

            DataCtrlSave();

            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                //this.BringToFront(); // 폼을 맨 앞으로 가져옴
                this.Opacity = 1; // 투명도 복원
            };

            // 60초 뒤 홈 화면으로 이동하는 타이머 초기화
            InitializeInactivityHandler();
        }

        ////////////////////////////////////////////////////////////60초 뒤 홈 화면으로 이동하는 타이머 //////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////60초 뒤 홈 화면으로 이동하는 타이머//////////////////////////////////////////////////////////


        /// 추 후 바코드 인식 추가 시 작성 ///
        public void ReadQRCode(object sender, KeyEventArgs e){}

        public void Go_Home(object sender, EventArgs e)
        {
            DataCtrl.ClearAll();
            response.ClearData();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            HomeForm home = new HomeForm();
        }

        public void Go_Back(object sender, EventArgs e)
        {
            DataCtrl.ClearAll();
            response.ClearData();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            ReservationMenu reservationMenu = new ReservationMenu();
        }

        public void Go_Next(object sender, EventArgs e)
        {
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            VolumeForm volumeForm = new VolumeForm();
        }

        private void ReservationResponse_Load()
        {
            senderName.Text = response.SndrNm;
            senderHP.Text = FormatPhoneNumber(response.SndrMobileNo);
            senderAddress.Text = response.SndrBaseAddr + " " + response.SndrDtlAddr;

            receiverName.Text = response.RcvrNm;
            receiverHP.Text = FormatPhoneNumber(response.RcvrMobileNo);
            receiverAddress.Text = response.RcvrBaseAddr + " " + response.RcvrDtlAddr;
        }

        private string FormatPhoneNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = input.Replace("-", "");
            StringBuilder formatted = new StringBuilder();

            string[] prefixes = {
        "010", "011", "016", "017", "018", "019",
        "02",
        "031", "032", "033", "041", "042", "043", "044",
        "051", "052", "053", "054", "055",
        "061", "062", "063", "064",
        "070", "080"
    };

            string matchedPrefix = prefixes.FirstOrDefault(prefix => input.StartsWith(prefix));

            if (matchedPrefix != null)
            {
                formatted.Append(matchedPrefix);

                string remaining = input.Substring(matchedPrefix.Length);

                if (matchedPrefix.Length == 3)
                {
                    if (remaining.Length > 0)
                    {
                        formatted.Append("-");
                        if (remaining.Length <= 7)
                        {
                            if (remaining.Length <= 3)
                            {
                                formatted.Append(remaining);
                            }
                            else
                            {
                                formatted.Append(remaining.Substring(0, 3));
                                formatted.Append("-");
                                formatted.Append(remaining.Substring(3));
                            }
                        }
                        else
                        {
                            formatted.Append(remaining.Substring(0, 4));
                            formatted.Append("-");
                            formatted.Append(remaining.Substring(4));
                        }
                    }
                }
                else if (matchedPrefix.StartsWith("02"))
                {
                    if (remaining.Length > 0)
                    {
                        formatted.Append("-");
                        if (remaining.Length <= 3)
                        {
                            formatted.Append(remaining);
                        }
                        else if (remaining.Length <= 7)
                        {
                            formatted.Append(remaining.Substring(0, 3));
                            formatted.Append("-");
                            formatted.Append(remaining.Substring(3));
                        }
                        else
                        {
                            formatted.Append(remaining.Substring(0, 4));
                            formatted.Append("-");
                            formatted.Append(remaining.Substring(4));
                        }
                    }
                }
                else
                {
                    if (remaining.Length > 0)
                    {
                        formatted.Append("-");
                        if (remaining.Length <= 4)
                        {
                            formatted.Append(remaining);
                        }
                        else if (remaining.Length <= 8)
                        {
                            formatted.Append(remaining.Substring(0, 4));
                            formatted.Append("-");
                            formatted.Append(remaining.Substring(4));
                        }
                        else
                        {
                            formatted.Append(remaining.Substring(0, 4));
                            formatted.Append("-");
                            formatted.Append(remaining.Substring(4));
                        }
                    }
                }
            }
            else
            {
                formatted.Append(input);
            }

            return formatted.ToString();
        }




        private void DataCtrlSave()
        {
            DataCtrl.SenderName = response.SndrNm;
            DataCtrl.SenderPhoneNo = response.SndrMobileNo;
            DataCtrl.SenderAddress = response.SndrZip;
            DataCtrl.SenderAddress2 = response.SndrBaseAddr;
            DataCtrl.SenderAddress3 = response.SndrDtlAddr;

            DataCtrl.ReceiverName = response.RcvrNm;
            DataCtrl.ReceiverPhoneNo = response.RcvrMobileNo;
            DataCtrl.ReceiverAddress = response.RcvrZip;
            DataCtrl.ReceiverAddress2 = response.RcvrBaseAddr;
            DataCtrl.ReceiverAddress3 = response.RcvrDtlAddr;
        }
    }
}
