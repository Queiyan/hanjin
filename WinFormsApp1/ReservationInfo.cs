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

        public ReservationInfo()
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
        public void ReadQRCode(object sender, KeyEventArgs e){}

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

        private void EasyMenu_Load(object sender, EventArgs e)
        {

        }

    }
}
