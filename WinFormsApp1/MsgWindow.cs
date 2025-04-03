using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MsgWindow : Form
    {
        public static bool IsShowing { get; private set; } = false;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public MsgWindow(string msg)
        {

            if (IsShowing)
            {
                return; // 이미 메시지 창이 열려 있으면 새로운 창을 열지 않음
            }

            InitializeComponent();
            msgText.Text = msg;
            msgText.Font = new Font("Pretendard", 40F);

            // 메시지 위치 조정
            int msgX = (MsgLayout.Width - msgText.Width) / 2;
            int msgY = (MsgLayout.Height - msgText.Height) / 2;
            msgText.Location = new Point(msgX, msgY);

            IsShowing = true;
            this.FormClosed += (s, e) => IsShowing = false; // 창 닫힐 때 상태 초기화

            this.TopMost = true; // 최상단에 위치하도록 설정

            this.Show();

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
            inactivityTimer = new System.Timers.Timer(10000); // 10초
            inactivityTimer.Elapsed += InactivityTimer_Elapsed;
            inactivityTimer.AutoReset = false;
            inactivityTimer.Start();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 이벤트 핸들러
        private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                IsShowing = false;
                this.Close();
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


        public void CloseMsgWindow(object sender, EventArgs e)
        {
            if (IsShowing)
            {
                IsShowing = false;
                this.Close();
            }
        }
    }
}
