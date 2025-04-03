using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using AxWMPLib;
using Microsoft.VisualBasic.Devices;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Net;
using System.Security.Cryptography.Xml;

namespace WinFormsApp1
{
    public partial class ReservationMenu : Form
    {
        private NumpadForm numpad;

        private System.Timers.Timer holdDeleteTimer;
        private const int DeleteInterval = 200; // 연속 삭제 간격 (밀리초)

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        private String reservNum;

        public ReservationMenu()
        {
            InitializeComponent();
            this.Opacity = 0; // 초기 투명도 설정

            this.Show();

            this.Load += EasyMenu_Load;

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
        public void ReadQRCode(object sender, KeyEventArgs e) { }
        private void Backspace_MouseDown(object sender, MouseEventArgs e)
        {
            DeleteCharacter();
            holdDeleteTimer = new System.Timers.Timer(DeleteInterval);
            holdDeleteTimer.Elapsed += (s, args) =>
            {
                this.Invoke(new Action(DeleteCharacter));
            };
            holdDeleteTimer.Start();
        }

        private void Backspace_MouseUp(object sender, MouseEventArgs e)
        {
            if (holdDeleteTimer != null)
            {
                holdDeleteTimer.Stop();
                holdDeleteTimer.Dispose();
                holdDeleteTimer = null;
            }
        }

        private void DeleteCharacter()
        {
            if (ReservNumInput.TextLength > 0)
            {
                int selectionStart = ReservNumInput.SelectionStart;
                ReservNumInput.Text = ReservNumInput.Text.Remove(selectionStart - 1, 1);
                ReservNumInput.SelectionStart = selectionStart - 1;
            }
        }

        public void Go_Home(object sender, EventArgs e)
        {
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            HomeForm home = new HomeForm();
        }

        public void Go_Back(object sender, EventArgs e)
        {
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            HomeForm home = new HomeForm();
        }

        public void Go_Next(object sender, EventArgs e)
        {
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            HomeForm home = new HomeForm();
        }

        private async void Keyboard_NextKeyPressed(object sender, EventArgs e)
        {
            reservNum = ReservNumInput.Text;

            // API 엔드포인트 URL
            string apiUrl = $"https://api-stg.hanjin.com/parcel-delivery/v1/rsv/retrieve-rsvno/{reservNum}";
            HttpMethod method = HttpMethod.Get;

            try
            {
                // API 요청 보내기
                string responseJson = await ApiHelper.SendRequestAsync(apiUrl, method);

                Console.WriteLine($"API 응답: {responseJson}");

                // JSON 파싱
                using (JsonDocument jsonDoc = JsonDocument.Parse(responseJson))
                {
                    JsonElement root = jsonDoc.RootElement;

                    // 필요한 데이터 추출 (예: status 값)
                    if (root.TryGetProperty("status", out JsonElement statusElement))
                    {
                        string status = statusElement.GetString();

                        if (status == "success")
                        {
                            Go_Next(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("예약 정보를 찾을 수 없습니다.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("API 응답 데이터 형식이 올바르지 않습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
                Console.WriteLine($"오류 발생: {ex.Message}");
            }
        }

        private void EasyMenu_Load(object sender, EventArgs e)
        {
            ReservNumInput.Focus();

            ShowCustomVirtualKeyboard();
        }

        private void ShowCustomVirtualKeyboard()
        {
            numpad = new NumpadForm();
            numpad.Location = new Point(0, 764);
            numpad.NextKeyPressed += Keyboard_NextKeyPressed;
            numpad.Show();
            numpad.SetTargetInputField(ReservNumInput);
            ReservNumInput.Focus();
        }

        // 넘패드 확인 버튼
        private void numPad_NextKeyPressed(object sender, EventArgs e)
        {
            Go_Next(sender, e);
            numpad.Close();
        }


        // 개발자 버튼
        private void DevBtn_Click(object sender, EventArgs e)
        {
            ReservNumInput.Text = "650306985";
        }
    }
}
