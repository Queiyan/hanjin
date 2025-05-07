using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WinFormsApp1
{
    public partial class DetailedAddressForm : Form
    {
        public string DetailedAddress { get; private set; }

        private CustomVirtualKeyboard keyboard;
        private PostcodeSearchForm parentForm;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public DetailedAddressForm(string address, PostcodeSearchForm parentForm)
        {
            this.Opacity = 0; // 초기 투명도 설정
            InitializeComponent();
            AddressInputed.Text = address;
            this.parentForm = parentForm;
            this.Load += VoiceForm_Load;
            txtDetailedAddress.GotFocus += RichTextBox_GotFocus;

            this.Shown += async (s, e) =>
            {
                await Task.Delay(300);
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
            inactivityTimer = new System.Timers.Timer(60000); // 60초로 통일
            inactivityTimer.Elapsed += InactivityTimer_Elapsed;
            inactivityTimer.AutoReset = false;
            inactivityTimer.Start();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 이벤트 핸들러
        private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    if (keyboard != null && !keyboard.IsDisposed)
                    {
                        keyboard.Close();
                        keyboard.Dispose();
                    }
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
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

        private void VoiceForm_Load(object sender, EventArgs e)
        {
            // 가상 키보드 생성 및 표시
            ShowCustomVirtualKeyboard();

            // 폼 로드 시 첫 번째 입력 필드에 포커스 설정
            txtDetailedAddress.Focus();
            keyboard.SetTargetInputField(txtDetailedAddress); // 키보드의 대상 필드도 설정

            foreach (Control control in this.Controls)
            {
                if (control is RichTextBox richTextBox)
                {
                    richTextBox.GotFocus += RichTextBox_GotFocus;
                    richTextBox.MouseUp += RichTextBox_MouseUp;
                    richTextBox.MouseDown += RichTextBox_MouseDown;
                    richTextBox.Click += RichTextBox_Click;
                }
            }

        }
        private void RichTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.ResetComposingBuffer();
            }
        }
        private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.ResetComposingBuffer();
            }
        }
        private void RichTextBox_Click(object sender, EventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.FinalizeBlock();
            }
        }
        private void RichTextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                keyboard.SetTargetInputField(richTextBox);
                richTextBox.Focus(); // 포커스 설정 (이미 포커스를 받았겠지만 명시적으로 설정)
            }
        }
        private void ShowCustomVirtualKeyboard()
        {
            keyboard = new CustomVirtualKeyboard(this.Handle);
            keyboard.Location = new Point(0, 764);
            keyboard.NextKeyPressed += Keyboard_NextKeyPressed;
            keyboard.Show();
        }

        private void Keyboard_NextKeyPressed(object sender, EventArgs e)
        {
            //Go_Next(sender, e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDetailedAddress.Text))
            {
                new MiniMsgWindow("상세 주소를 입력하세요").Show();
                return;
            }

            DetailedAddress = txtDetailedAddress.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
            Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            keyboard.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => keyboard.Dispose(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
                inactivityTimer.Dispose();
            }
            if (keyboard != null && !keyboard.IsDisposed)
            {
                keyboard.Close();
                keyboard.Dispose();
            }
        }
    }
}
