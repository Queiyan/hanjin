using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// 필요 없는 using 문은 제거하셔도 됩니다.
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinFormsApp1
{
    public partial class NumpadForm : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [In, Out] TOUCHINPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool RegisterTouchWindow(IntPtr hwnd, ulong ulFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TOUCHINPUT
        {
            public int x;
            public int y;
            public IntPtr hSource;
            public int dwID;
            public int dwFlags;
            public int dwMask;
            public int dwTime;
            public IntPtr dwExtraInfo;
            public int cxContact;
            public int cyContact;
        }

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000; // 절대 좌표 사용
        private const int MOUSEEVENTF_MOVE = 0x0001;

        private System.Windows.Forms.Timer backspaceTimer;
        private const int DeleteInterval = 200; // 연속 삭제 간격 (밀리초)
        private bool isBackspacePressed = false;
        private DateTime lastBackspacePressTime;
        private const int TouchThreshold = 500; // 터치 감지 임계값 (밀리초)

        public RichTextBox targetInputField;
        public event EventHandler NextKeyPressed;
        public string inputStr = "";

        public NumpadForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            this.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            InitializeComponent();
            this.Show();
            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                this.Opacity = 1; // 투명도 복원
            };
            this.TopMost = true;

            // 백스페이스 버튼 이벤트 연결
            Control[] backspaceControls = this.Controls.Find("BackSpace", true);
            if (backspaceControls.Length > 0 && backspaceControls[0] is PictureBox backspaceButton)
            {
                backspaceButton.MouseDown += BackSpace_MouseDown;
                backspaceButton.MouseUp += BackSpace_MouseUp;
            }

            // 터치 입력 등록
            RegisterTouchWindow(this.Handle, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_TOUCH = 0x0240;
            if (m.Msg == WM_TOUCH)
            {
                HandleTouchMessage(m);
            }
            base.WndProc(ref m);
        }

        private void HandleTouchMessage(Message m)
        {
            const int TOUCHEVENTF_DOWN = 0x0001;
            const int TOUCHEVENTF_UP = 0x0002;
            const int TOUCHEVENTF_MOVE = 0x0004;

            int inputCount = m.WParam.ToInt32();
            TOUCHINPUT[] inputs = new TOUCHINPUT[inputCount];

            if (GetTouchInputInfo(m.LParam, inputCount, inputs, Marshal.SizeOf(typeof(TOUCHINPUT))))
            {
                foreach (var input in inputs)
                {
                    if ((input.dwFlags & TOUCHEVENTF_DOWN) != 0)
                    {
                        HandleTouchDown(input);
                    }
                    else if ((input.dwFlags & TOUCHEVENTF_UP) != 0)
                    {
                        HandleTouchUp(input);
                    }
                }
            }
        }

        private void HandleTouchDown(TOUCHINPUT input)
        {
            // 백스페이스 버튼 영역 확인
            Control[] backspaceControls = this.Controls.Find("BackSpace", true);
            if (backspaceControls.Length > 0 && backspaceControls[0] is PictureBox backspaceButton)
            {
                // 터치 좌표가 백스페이스 버튼 영역 내에 있는지 확인
                Point touchPoint = new Point(input.x, input.y);
                if (backspaceButton.Bounds.Contains(touchPoint))
                {
                    if (!isBackspacePressed)
                    {
                        isBackspacePressed = true;
                        lastBackspacePressTime = DateTime.Now;
                        StartBackspaceTimer();
                    }
                }
            }
        }

        private void HandleTouchUp(TOUCHINPUT input)
        {
            if (isBackspacePressed)
            {
                isBackspacePressed = false;
                StopBackspaceTimer();
            }
        }

        private void StartBackspaceTimer()
        {
            if (backspaceTimer == null)
            {
                backspaceTimer = new System.Windows.Forms.Timer();
                backspaceTimer.Interval = DeleteInterval;
                backspaceTimer.Tick += BackspaceTimer_Tick;
            }
            backspaceTimer.Start();
        }

        private void StopBackspaceTimer()
        {
            if (backspaceTimer != null)
            {
                backspaceTimer.Stop();
                backspaceTimer.Tick -= BackspaceTimer_Tick;
                backspaceTimer.Dispose();
                backspaceTimer = null;
            }
        }

        private void BackspaceTimer_Tick(object sender, EventArgs e)
        {
            if (backspaceTimer != null && backspaceTimer.Enabled && isBackspacePressed)
            {
                HandleBackspace();
            }
        }

        private void HandleBackspace()
        {
            if (targetInputField == null) return;

            int selectionStart = targetInputField.SelectionStart;
            int selectionLength = targetInputField.SelectionLength;

            if (selectionLength > 0)
            {
                // 선택된 텍스트 삭제
                targetInputField.Text = targetInputField.Text.Remove(selectionStart, selectionLength);
            }
            else if (selectionStart > 0)
            {
                // 커서 앞의 한 문자 삭제
                targetInputField.Text = targetInputField.Text.Remove(selectionStart - 1, 1);
                targetInputField.SelectionStart = selectionStart - 1;
            }
        }

        public void EnterNumber(object sender, EventArgs e)
        {
            PictureBox numBox = (PictureBox)sender;
            string name = numBox.Name;

            if (targetInputField == null)
            {
                Console.WriteLine("NumPad targetInputField is null.");
                return;
            }

            if (!targetInputField.Focused)
            {
                targetInputField.Focus();
            }

            int selectionStart = targetInputField.SelectionStart;
            int selectionLength = targetInputField.SelectionLength;

            // 하이픈을 제거한 순수 숫자 문자열과 커서 이전의 숫자 개수 계산
            string unformattedText = targetInputField.Text.Replace("-", "");
            int digitsBeforeCursor = targetInputField.Text.Substring(0, selectionStart).Count(char.IsDigit);

            if (name == "BackSpace")
            {
                if (!isBackspacePressed)
                {
                    isBackspacePressed = true;
                    lastBackspacePressTime = DateTime.Now;
                    StartBackspaceTimer();
                }
                HandleBackspace();
            }
            else if (name == "Delete")
            {
                if (selectionLength > 0)
                {
                    // 선택된 텍스트 삭제
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart, selectionLength);
                }
                else if (selectionStart < targetInputField.Text.Length)
                {
                    // 커서 뒤의 한 문자 삭제
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart, 1);
                }
            }
            else if (name == "Enter")
            {
                OnNextKeyPressed(EventArgs.Empty);
                return;
            }
            else if (name.Contains("num"))
            {
                if (unformattedText.Length < 11) // 최대 11자리 숫자 제한
                {
                    string num = name.Replace("num", "");
                    if (selectionLength > 0)
                    {
                        // 선택된 텍스트 대체
                        targetInputField.Text = targetInputField.Text.Remove(selectionStart, selectionLength).Insert(selectionStart, num);
                    }
                    else
                    {
                        // 커서 위치에 숫자 삽입
                        targetInputField.Text = targetInputField.Text.Insert(selectionStart, num);
                    }
                    selectionStart += num.Length;
                    digitsBeforeCursor += num.Length;
                }
            }

            // 포맷팅 후 커서 위치 조정
            targetInputField.SelectionStart = GetCursorPositionFromDigitIndex(targetInputField.Text, digitsBeforeCursor);
            targetInputField.SelectionLength = 0;
            targetInputField.Focus();
        }

        private int GetCursorPositionFromDigitIndex(string formattedText, int digitIndex)
        {
            int digitsCount = 0;
            for (int i = 0; i < formattedText.Length; i++)
            {
                if (char.IsDigit(formattedText[i]))
                {
                    if (digitsCount == digitIndex)
                    {
                        return i;
                    }
                    digitsCount++;
                }
            }
            return formattedText.Length;
        }

        private string FormatPhoneNumber(string digits)
        {
            if (digits.StartsWith("02"))
            {
                // 02-XXXX-XXXX 형식
                if (digits.Length > 2)
                {
                    string part1 = digits.Substring(0, 2);
                    string part2 = digits.Length >= 6 ? digits.Substring(2, 4) : digits.Substring(2);
                    string part3 = digits.Length > 6 ? digits.Substring(6) : "";

                    return string.Join("-", new[] { part1, part2, part3 }.Where(s => !string.IsNullOrEmpty(s)));
                }
                else
                {
                    return digits;
                }
            }
            else if (digits.Length == 10)
            {
                // XXX-XXX-XXXX 형식
                string part1 = digits.Substring(0, 3);
                string part2 = digits.Substring(3, 3);
                string part3 = digits.Substring(6, 4);

                return $"{part1}-{part2}-{part3}";
            }
            else
            {
                // 휴대폰 번호 또는 기타 지역 번호
                if (digits.Length > 3)
                {
                    string part1 = digits.Substring(0, 3);
                    string part2 = digits.Length >= 7 ? digits.Substring(3, 4) : digits.Substring(3);
                    string part3 = digits.Length > 7 ? digits.Substring(7) : "";

                    return string.Join("-", new[] { part1, part2, part3 }.Where(s => !string.IsNullOrEmpty(s)));
                }
                else
                {
                    return digits;
                }
            }
        }

        public void SetTargetInputField(RichTextBox fieldBox)
        {
            if (targetInputField != fieldBox)
            {
                targetInputField = fieldBox;
            }
        }

        protected virtual void OnNextKeyPressed(EventArgs e)
        {
            NextKeyPressed?.Invoke(this, e);
        }

        // 필요하지 않은 메서드는 제거하거나 비워 둘 수 있습니다.
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void keyLayout1_Paint(object sender, PaintEventArgs e) { }
        private void NumpadForm_Load(object sender, EventArgs e) { }

        // 마우스 이벤트 핸들러 추가
        private void BackSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isBackspacePressed)
            {
                isBackspacePressed = true;
                lastBackspacePressTime = DateTime.Now;
                StartBackspaceTimer();
            }
            HandleBackspace();
        }

        private void BackSpace_MouseUp(object sender, MouseEventArgs e)
        {
            if (isBackspacePressed)
            {
                isBackspacePressed = false;
                StopBackspaceTimer();
            }
        }
    }
}
