using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class HomeForm : Form
    {
        private System.Windows.Forms.Timer idleTimer;
        private DateTime lastActivityTime;
        private VideoPlayerForm videoPlayerForm;

        public HomeForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            InitializeComponent();
            this.Show();
            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                this.Opacity = 1; // 투명도 복원
            };

            // 사용자 활동 감지를 위한 이벤트 핸들러 등록
            this.MouseMove += HomeForm_MouseMove;
            this.MouseClick += HomeForm_MouseClick;
            this.KeyPress += HomeForm_KeyPress;

            // 30초 타이머 초기화
            idleTimer = new System.Windows.Forms.Timer();
            idleTimer.Interval = 30000; // 30초
            idleTimer.Tick += IdleTimer_Tick;
            lastActivityTime = DateTime.Now;
            idleTimer.Start();
        }

        private void HomeForm_MouseMove(object sender, MouseEventArgs e)
        {
            ResetIdleTimer();
        }

        private void HomeForm_MouseClick(object sender, MouseEventArgs e)
        {
            ResetIdleTimer();
        }

        private void HomeForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetIdleTimer();
        }

        private void ResetIdleTimer()
        {
            lastActivityTime = DateTime.Now;
            if (videoPlayerForm != null && !videoPlayerForm.IsDisposed)
            {
                videoPlayerForm.Close();
                videoPlayerForm = null;
            }
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - lastActivityTime).TotalSeconds >= 30)
            {
                if (videoPlayerForm == null || videoPlayerForm.IsDisposed)
                {
                    videoPlayerForm = new VideoPlayerForm();
                    videoPlayerForm.FormClosed += (s, args) => 
                    {
                        videoPlayerForm = null;
                        lastActivityTime = DateTime.Now;
                    };
                    videoPlayerForm.Show();
                }
            }
        }

        public void Go_Kiosk(object sender, EventArgs e)
        {
            TermsForm kioskForm = new TermsForm();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Easy(object sender, EventArgs e)
        {
            ReservationMenu easyMenu = new ReservationMenu();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Mobile(object sender, EventArgs e)
        {
            // dev 테스트용
            //MobileMenu mobileMenu = new MobileMenu();
            //Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());

            CautionForm cautionForm = new CautionForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Cursor.Position = new System.Drawing.Point(0, 200);

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void HomeForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
