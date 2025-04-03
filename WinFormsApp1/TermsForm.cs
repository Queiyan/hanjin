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
using Microsoft.Web.WebView2.WinForms;


namespace WinFormsApp1
{
    public partial class TermsForm : Form
    {
        private bool isDragging = false;
        private Point dragStartPoint;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        // WebView2 컨트롤
        private WebView2 termsWebView;

        public TermsForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            InitializeComponent();
            InitializeTermsAndScrollBar();
            // 폼에 더블 버퍼링 활성화
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

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


        private void InitializeTermsAndScrollBar()
        {
            // WebView2 초기화
            termsWebView = new WebView2
            {
                Location = new Point(34, 214),
                Size = new Size(956, 859)
            };
            this.Controls.Add(termsWebView);
            termsWebView.BringToFront();
            termsWebView.NavigationCompleted += TermsWebView_NavigationCompleted;

            // 약관 페이지 로드
            termsWebView.Source = new Uri("https://app.hanjin.com/app/view/docs/agree4");

            // 커스텀 스크롤 바 이벤트 핸들러 추가
            // 필요 시 추가 구현
        }

        private async void TermsWebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            // 페이지 로드 완료 후 스크롤 처리 등 추가 로직 가능
            if (termsWebView.CoreWebView2 != null)
            {
                // 폰트 크기 증가 및 드래그 방지 스크립트 실행
                string script = @"
                    (function() {
                        document.body.style.fontSize = '18px';
                        document.body.style.userSelect = 'none';
                        document.body.style.webkitUserSelect = 'none';
                        document.body.style.msUserSelect = 'none';
                        document.body.style.MozUserSelect = 'none';
                        document.onselectstart = function() { return false; };
                        document.ondragstart = function() { return false; };
                    })();
                ";
                await termsWebView.ExecuteScriptAsync(script);
            }
        }

        private void ScrollHandle_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = e.Location;
        }

        private void ScrollHandle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int newY = scrollHandle.Top + (e.Y - dragStartPoint.Y);
                newY = Math.Max(0, Math.Min(customScrollBar.Height - scrollHandle.Height, newY));
                scrollHandle.Top = newY;

                // Calculate the scrolling percentage
                //float scrollPercentage = (float)newY / (customScrollBar.Height - scrollHandle.Height);
                //int scrollPosition = (int)(scrollPercentage * (termsTextBox.GetPositionFromCharIndex(termsTextBox.Text.Length - 1).Y - termsTextBox.ClientSize.Height));
                //termsTextBox.SelectionStart = termsTextBox.GetCharIndexFromPosition(new Point(0, scrollPosition));
                //termsTextBox.ScrollToCaret();
            }
        }

        private void ScrollHandle_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void TermsTextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            int lines = termsTextBox.GetLineFromCharIndex(termsTextBox.SelectionStart) - numberOfTextLinesToMove;
            if (lines < 0) lines = 0;
            if (lines > termsTextBox.Lines.Length - 1) lines = termsTextBox.Lines.Length - 1;
            termsTextBox.SelectionStart = termsTextBox.GetFirstCharIndexFromLine(lines);
            termsTextBox.ScrollToCaret();

            // Update scroll handle position
            UpdateScrollHandlePosition();
        }

        private void TermsTextBox_TextChanged(object sender, EventArgs e)
        {
            // Update scroll handle position
            UpdateScrollHandlePosition();
        }

        private void TermsTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Prevent text selection by setting SelectionStart and SelectionLength to 0
            termsTextBox.SelectionStart = 0;
            termsTextBox.SelectionLength = 0;
        }

        private void KioskForm_Load(object sender, EventArgs e)
        {
            // Initialize the selection state of the termsTextBox
            termsTextBox.SelectionStart = 0;
            termsTextBox.SelectionLength = 0;
        }

        private void UpdateScrollHandlePosition()
        {
            // Calculate the scrolling percentage
            int maxScrollY = termsTextBox.GetPositionFromCharIndex(termsTextBox.Text.Length - 1).Y - termsTextBox.ClientSize.Height;
            if (maxScrollY > 0)
            {
                float scrollPercentage = (float)termsTextBox.GetPositionFromCharIndex(termsTextBox.SelectionStart).Y / maxScrollY;
                scrollHandle.Top = (int)(scrollPercentage * (customScrollBar.Height - scrollHandle.Height));
            }
            else
            {
                scrollHandle.Top = 0;
            }
        }

        public void Go_Home(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            //this.Close();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            //this.Close();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
        //public void Go_HandWriter(object sender, EventArgs e)
        //{
        //    DataCtrl.ClearAll();
        //    //this.Close();
        //    HandwriterForm handwriterForm = new HandwriterForm("sender", true);
        //    Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        //}

        public void Go_Voice(object sender, EventArgs e)
        {
            //DataCtrl.ClearAll();
            InvoiceSenderForm voiceForm = new InvoiceSenderForm("sender");
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            //this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void customScrollBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void scrollHandle_Click(object sender, EventArgs e)
        {

        }
    }
}
