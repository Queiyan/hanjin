using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controls;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text;

namespace WinFormsApp1
{
    public partial class CustomVirtualKeyboard : Form
    {
        private System.Windows.Forms.Timer backspaceTimer;
        private const int DeleteInterval = 200; // 연속 삭제 간격 (밀리초)
        private bool isBackspacePressed = false;
        private DateTime lastBackspacePressTime;
        private const int TouchThreshold = 500; // 터치 감지 임계값 (밀리초)

        [DllImport("user32.dll")]
        private static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [In, Out] TOUCHINPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool RegisterTouchWindow(IntPtr hwnd, ulong ulFlags);

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

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)] public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        const int INPUT_KEYBOARD = 1;
        const int KEYEVENTF_UNICODE = 0x0004;
        const int KEYEVENTF_KEYUP = 0x0002;
        //const ushort VK_HANGUL = 0x15; // 한/영 키
        private const byte VK_HANGUL = 0x15; // 한/영 전환 키


        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);


        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        // 한글 유니코드 상수
        private const int HangulBase = 0xAC00;
        private const int InitialOffset = 588; // 21*28
        private const int MedialOffset = 28;

        // 키보드 입력을 보내는 함수
        private void SendKey(ushort unicode)
        {
            Console.WriteLine($"Sending key: {unicode}");

            INPUT[] inputs = new INPUT[2];

            // 키 누름
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].u.ki.wVk = 0;
            inputs[0].u.ki.wScan = unicode;
            inputs[0].u.ki.dwFlags = KEYEVENTF_UNICODE;
            inputs[0].u.ki.time = 0;
            inputs[0].u.ki.dwExtraInfo = IntPtr.Zero;

            // 키 떼기
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].u.ki.wVk = 0;
            inputs[1].u.ki.wScan = unicode;
            inputs[1].u.ki.dwFlags = KEYEVENTF_UNICODE | KEYEVENTF_KEYUP;
            inputs[1].u.ki.time = 0;
            inputs[1].u.ki.dwExtraInfo = IntPtr.Zero;

            uint result = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            if (result == 0)
            {
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine($"SendInput failed with error code: {error}");
            }
            else
            {
                Console.WriteLine("SendInput succeeded");
            }
        }


        // ★ 초성 19
        private static readonly string[] Initials = {
            "ㄱ","ㄲ","ㄴ","ㄷ","ㄸ","ㄹ","ㅁ","ㅂ","ㅃ","ㅅ",
            "ㅆ","ㅇ","ㅈ","ㅉ","ㅊ","ㅋ","ㅌ","ㅍ","ㅎ"
        };
        // ★ 중성 21
        private static readonly string[] Medials = {
            "ㅏ","ㅐ","ㅑ","ㅒ","ㅓ","ㅔ","ㅕ","ㅖ","ㅗ","ㅘ",
            "ㅙ","ㅚ","ㅛ","ㅜ","ㅝ","ㅞ","ㅟ","ㅠ","ㅡ","ㅢ","ㅣ"
        };
        // ★ 종성 28
        private static readonly string[] Finals = {
            "", "ㄱ","ㄲ","ㄳ","ㄴ","ㄵ","ㄶ","ㄷ","ㄹ","ㄺ","ㄻ","ㄼ","ㄽ","ㄾ","ㄿ","ㅀ",
            "ㅁ","ㅂ","ㅄ","ㅅ","ㅆ","ㅇ","ㅈ","ㅊ","ㅋ","ㅌ","ㅍ","ㅎ"
        };

        // 복합 종성 테이블
        private static readonly Dictionary<string, string> ComplexFinals
            = new Dictionary<string, string> {
                {"ㄱㅅ","ㄳ"}, {"ㄴㅈ","ㄵ"}, {"ㄴㅎ","ㄶ"},
                {"ㄹㄱ","ㄺ"}, {"ㄹㅁ","ㄻ"}, {"ㄹㅂ","ㄼ"},
                {"ㄹㅅ","ㄽ"}, {"ㄹㅌ","ㄾ"}, {"ㄹㅍ","ㄿ"},
                {"ㄹㅎ","ㅀ"}, {"ㅂㅅ","ㅄ"}
            };

        // (★) 복합 종성 분해 (멜트다운): ex) ㄳ -> (ㄱ, ㅅ)
        private static readonly Dictionary<string, (string first, string second)> MeltdownMap
            = new Dictionary<string, (string, string)>
        {
            {"ㄳ", ("ㄱ","ㅅ")}, {"ㄵ",("ㄴ","ㅈ")}, {"ㄶ",("ㄴ","ㅎ")},
            {"ㄺ",("ㄹ","ㄱ")}, {"ㄻ",("ㄹ","ㅁ")}, {"ㄼ",("ㄹ","ㅂ")},
            {"ㄽ",("ㄹ","ㅅ")}, {"ㄾ",("ㄹ","ㅌ")}, {"ㄿ",("ㄹ","ㅍ")},
            {"ㅀ",("ㄹ","ㅎ")}, {"ㅄ",("ㅂ","ㅅ")}
        };

        // (★) 초성만 단독 입력 시, 호환 자모(Compatibility Jamo)로 표시하기
        private static readonly Dictionary<string, int> JamoMap = new Dictionary<string, int>
        {
            // ㄱ=U+3131, ㄲ=U+3132, ㄴ=U+3134, ㄷ=3137, ㄸ=3138 ...
            {"ㄱ",0x3131},{"ㄲ",0x3132},{"ㄴ",0x3134},{"ㄷ",0x3137},{"ㄸ",0x3138},
            {"ㄹ",0x3139},{"ㅁ",0x3141},{"ㅂ",0x3142},{"ㅃ",0x3143},{"ㅅ",0x3145},
            {"ㅆ",0x3146},{"ㅇ",0x3147},{"ㅈ",0x3148},{"ㅉ",0x3149},{"ㅊ",0x314A},
            {"ㅋ",0x314B},{"ㅌ",0x314C},{"ㅍ",0x314D},{"ㅎ",0x314E}
        };

        // 확인 버튼 클릭 이벤트
        public event EventHandler NextKeyPressed;

        // 상태값
        public bool isShift = false;
        public bool isKor = true;
        public static string[][] keysCtrlName = new string[6][];
        public static string[][] keys = new string[6][];

        private int initialIndex = -1;
        private int medialIndex = -1;
        private int finalIndex = -1;

        private string currentInitial = null;
        private string currentMedial = null;
        private string currentFinal = null;

        private string composing = "";
        private string oldBlock = "";

        private bool hasSwitchedLang = false;
        private bool hasErased = false;

        private RichTextBox targetInputField;

        private IntPtr parentFormHandle;

        protected override void WndProc(ref Message m)
        {
            const int WM_TOUCH = 0x0240;
            const int WM_KEYDOWN = 0x0100;

            if (m.Msg == WM_TOUCH)
            {
                HandleTouchMessage(m);
            }
            else if (m.Msg == WM_KEYDOWN)
            {
                // 백스페이스 키 (VK_BACK = 0x08)
                if (m.WParam.ToInt32() == 0x08)
                {
                    if (!isBackspacePressed)
                    {
                        isBackspacePressed = true;
                        lastBackspacePressTime = DateTime.Now;
                        StartBackspaceTimer();
                    }
                    HandleBackspace();
                }
            }
            base.WndProc(ref m);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Back)
            {
                if (!isBackspacePressed)
                {
                    isBackspacePressed = true;
                    lastBackspacePressTime = DateTime.Now;
                    StartBackspaceTimer();
                }
                HandleBackspace();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            Control[] backspaceControls = this.Controls.Find("Backspace", true);
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

        public CustomVirtualKeyboard(IntPtr parentHandle)
        {
            parentFormHandle = parentHandle;
            InitializeKeyArrays();
            InitializeComponent();

            if (!DesignMode)
            {
                // 터치 입력 등록
                RegisterTouchWindow(this.Handle, 0);

                // 폼의 초기 위치와 크기 설정
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                this.Opacity = 0; // 초기 투명도 설정

                // 폼에 더블 버퍼링 활성화
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
                this.UpdateStyles();

                UpdateKeyboardLayout();

                // 폼이 완전히 로드된 후에 Z-Order와 투명도 설정
                this.Load += (sender, e) =>
                {
                    this.SendToBack(); // 폼을 맨 뒤로 보냄
                };

                // Deactivate 이벤트 핸들러 추가
                this.Deactivate += CustomVirtualKeyboard_Deactivate;
                this.Show();
                this.Shown += async (s, e) =>
                {
                    await Task.Delay(500);
                    this.Opacity = 1; // 투명도 복원
                };

                // 폼을 항상 최상위에 있도록 설정
                this.TopMost = true;

                // 백스페이스 버튼 이벤트 연결
                Control[] backspaceControls = this.Controls.Find("Backspace", true);
                if (backspaceControls.Length > 0 && backspaceControls[0] is PictureBox backspaceButton)
                {
                    backspaceButton.MouseDown += Backspace_MouseDown;
                    backspaceButton.MouseUp += Backspace_MouseUp;
                }
            }
        }
        private void FixFontSize(RichTextBox richTextBox, string fontFamily, float fontSize)
        {
            if (richTextBox != null)
            {
                richTextBox.Font = new Font(fontFamily, fontSize, richTextBox.Font.Style);
            }
        }
        private void CustomVirtualKeyboard_Deactivate(object sender, EventArgs e)
        {
            // 폼이 비활성화될 때 닫히지 않도록 설정
            this.TopMost = true;
            this.Activate();
        }
        public void SetTargetInputField(RichTextBox fieldBox)
        {
            // targetInputField가 변경될 때 composing 버퍼 초기화
            if (targetInputField != fieldBox)
            {
                FinalizeBlock(); // 현재 입력 중인 블록을 확정하고 초기화
                targetInputField = fieldBox;
                //FixFontSize(targetInputField, "Pretendard", 20.25f); // Pretendard 폰트로 20.25pt 크기로 고정
            }
        }

        private void InitializeKeyArrays()
        {
            keysCtrlName[0] = new string[] { "Next" };
            keysCtrlName[1] = new string[] {
                "num1","num2","num3","num4","num5","num6","num7","num8","num9","num0","numDash","Slash"
            };
            keysCtrlName[2] = new string[] {
                "q","w","e","r","t","y","u","i","o","p","OpenBrace","CloseBrace"
            };
            keysCtrlName[3] = new string[] {
                "a","s","d","f","g","h","j","k","l","Enter"
            };
            keysCtrlName[4] = new string[] {
                "Shift","z","x","c","v","b","n","m","Comma","Dot","Backspace"
            };
            keysCtrlName[5] = new string[] {
                "SwitchLang","Space","At","leftArrow","rightArrow"
            };
        }
        private IEnumerable<MyKeyButton> GetAllMyKeyButtons(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is MyKeyButton keyButton)
                    yield return keyButton;
                else
                {
                    foreach (var child in GetAllMyKeyButtons(ctrl))
                        yield return child;
                }
            }
        }
        // 키 입력
        public void InputKey(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                string keyStr = pic.Tag as string;
                if (!string.IsNullOrEmpty(keyStr))
                {
                    HandleKeyStr(keyStr);
                }
            }
        }
        private void SendKeyToTarget(string text)
        {
            if (targetInputField != null)
            {
                int selectionStart = targetInputField.SelectionStart;
                int selectionLength = targetInputField.SelectionLength;

                // 현재 폰트와 색상 가져오기
                Font currentFont = targetInputField.SelectionFont ?? new Font("Pretendard", 20.25f, FontStyle.Regular);
                Color currentColor = targetInputField.SelectionColor;

                // 삽입할 위치로 이동
                targetInputField.SelectionStart = selectionStart;
                targetInputField.SelectionLength = selectionLength;

                // 삽입할 텍스트의 폰트와 색상 설정
                targetInputField.SelectionFont = currentFont;
                targetInputField.SelectionColor = currentColor;

                // 텍스트 삽입
                targetInputField.SelectedText = text;

                // 삽입 후 커서를 텍스트 끝으로 이동
                targetInputField.SelectionStart = selectionStart + text.Length;
                targetInputField.SelectionLength = 0;
                targetInputField.Focus();
            }
        }

        private void HandleKeyStr(string keyStr)
        {
            // targetInputField가 포커스를 가지고 있는지 확인
            if (targetInputField != null && !targetInputField.Focused)
            {
                targetInputField.Focus();
            }

            if (targetInputField == null)
            {
                Console.WriteLine("targetInputField is null.");
            }

            // 숫자나 특수 문자의 경우 바로 삽입
            if (keyStr.Length == 1 && (char.IsDigit(keyStr[0]) || "-/!@#$%^&*()_?[]{},.<>".Contains(keyStr[0])))
            {
                // NameInput 필드가 아닌 경우에만 숫자와 특수문자 삽입
                if (targetInputField != null && targetInputField.Name != "NameInput")
                {
                    FinalizeBlock();
                    InsertTextAtCaret(keyStr);
                }
                else
                {
                    if (!KeyboardMsgWindow.IsShowing)
                    {
                        new KeyboardMsgWindow("성명란에는\n숫자나 특수문자를\n입력할 수 없습니다", this).Show();
                        Console.WriteLine("성명란에는 숫자나 특수문자를 입력할 수 없습니다");
                    }
                }
                return;
            }

            switch (keyStr)
            {
                case "Backspace":
                    if (!isBackspacePressed)
                    {
                        isBackspacePressed = true;
                        lastBackspacePressTime = DateTime.Now;
                        StartBackspaceTimer();
                    }
                    HandleBackspace();
                    break;

                case "Space":
                    FinalizeBlock();
                    InsertTextAtCaret(" ");
                    return;

                case "Enter":
                    SendKeys.Send("{TAB}");
                    FinalizeBlock();
                    break;

                case "SwitchLang":
                    isKor = !isKor;
                    foreach (var btn in GetAllMyKeyButtons(this))
                    {
                        btn.IsKor = isKor;
                    }
                    FinalizeBlock();
                    UpdateKeyboardLayout();
                    break;

                case "Shift":
                    isShift = !isShift;
                    foreach (var btn in GetAllMyKeyButtons(this))
                    {
                        btn.IsShift = isShift;
                    }
                    UpdateKeyboardLayout();
                    break;

                case "leftArrow":
                case "rightArrow":
                    // 조합 버퍼에 남은 내용이 있으면 입력 필드에 반영하고 버퍼 초기화
                    FinalizeBlock();

                    // 커서 이동 처리
                    int direction = keyStr == "leftArrow" ? -1 : 1;
                    MoveCaret(direction);
                    break;

                case "Next":
                    OnNextKeyPressed(EventArgs.Empty);
                    break;

                default:
                    if (isKor)
                    {
                        composing += keyStr;
                        string composedText = ProcessInput(composing);

                        if (!string.IsNullOrEmpty(oldBlock))
                        {
                            RemoveOldBlock();
                        }

                        if (!string.IsNullOrEmpty(composedText))
                        {
                            InsertTextAtCaret(composedText);
                            oldBlock = composedText;
                        }
                    }
                    else
                    {
                        InsertTextAtCaret(keyStr);
                    }
                    break;
            }
        }

        private void HandleBackspace()
        {
            if (composing.Length > 0)
            {
                // composing 버퍼에서 마지막 글자 제거
                composing = composing.Substring(0, composing.Length - 1);

                // 기존 입력된 문자 삭제
                RemoveOldBlock();

                // 새로운 조합 처리
                string newBlock = ProcessInput(composing);

                if (!string.IsNullOrEmpty(newBlock))
                {
                    InsertTextAtCaret(newBlock);
                    oldBlock = newBlock;
                }
                else
                {
                    oldBlock = "";
                }
            }
            else
            {
                // targetInputField에서 문자 삭제
                if (targetInputField.SelectionStart > 0)
                {
                    int selectionStart = targetInputField.SelectionStart;
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart - 1, 1);
                    targetInputField.SelectionStart = selectionStart - 1;
                }
            }
        }

        private void InsertTextAtCaret(string text)
        {
            if (targetInputField != null)
            {
                // 입력 필드가 NameInput인지 확인
                bool isNameInput = targetInputField.Name == "NameInput";

                //Console.WriteLine($"isNameInput: {isNameInput}");

                if (!isNameInput)
                {
                    // NameInput이 아닌 경우 RTF를 사용하여 자간 설정
                    string rtfText = ConvertTextToRtfWithSpacing(text, 20); // 원하는 자간 값으로 조정 가능
                    targetInputField.SelectedRtf = rtfText;
                }
                else
                {
                    // NameInput인 경우 기존 방식대로 텍스트 삽입
                    int selectionStart = targetInputField.SelectionStart;
                    int selectionLength = targetInputField.SelectionLength;

                    Font currentFont = targetInputField.SelectionFont ?? new Font("Pretendard", 20.25f, FontStyle.Regular);
                    Color currentColor = targetInputField.SelectionColor;

                    Console.WriteLine($"Inserting text '{text}' at position {selectionStart} with font size {currentFont.Size}");

                    targetInputField.SelectionStart = selectionStart;
                    targetInputField.SelectionLength = selectionLength;

                    targetInputField.SelectionFont = currentFont;
                    targetInputField.SelectionColor = currentColor;

                    targetInputField.SelectedText = text;

                    targetInputField.SelectionStart = selectionStart + text.Length;
                    targetInputField.SelectionLength = 0;
                    targetInputField.Focus();

                    Console.WriteLine($"After insertion, cursor at {targetInputField.SelectionStart}");
                }
            }
        }
        // 자간을 설정하여 RTF 형식의 문자열을 생성하는 헬퍼 함수
        private string ConvertTextToRtfWithSpacing(string text, int spacing)
        {
            // RTF 헤더 및 폰트 테이블 설정
            StringBuilder rtfBuilder = new StringBuilder();
            rtfBuilder.Append(@"{\rtf1\ansi\deff0");
            rtfBuilder.Append(@"{\fonttbl{\f0\fnil Pretendard;}}");

            // 현재 폰트 및 크기 설정
            Font currentFont = targetInputField.SelectionFont ?? new Font("Pretendard", 20.25f, FontStyle.Regular);
            rtfBuilder.Append($@"\f0\fs{(int)(currentFont.Size * 2)}"); // \fs는 반포인트 단위

            // 자간 설정 (\expndN: N은 자간 확장 비율)
            rtfBuilder.Append($@"\expnd{spacing} {text}");
            rtfBuilder.Append("}");

            return rtfBuilder.ToString();
        }
        // 확인 버튼 클릭 이벤트
        protected virtual void OnNextKeyPressed(EventArgs e)
        {
            NextKeyPressed?.Invoke(this, e);
        }

        private void UpdatePartialBlock()
        {
            string newBlock = ProcessInput(composing);
            RemoveOldBlock();
            InsertTextAtCaret(newBlock);
            oldBlock = newBlock;
        }
        private void CommitBlock()
        {
            // 현재 입력 중인 블록을 확정하고, composing 버퍼를 초기화
            composing = "";
            ResetIndexes();
        }

        private void RemoveOldBlock()
        {
            if (!string.IsNullOrEmpty(oldBlock))
            {
                int selectionStart = targetInputField.SelectionStart;
                int removeStart = selectionStart - oldBlock.Length;

                if (removeStart >= 0 && targetInputField.Text.Length >= removeStart + oldBlock.Length)
                {
                    // 삭제할 영역 선택
                    targetInputField.SelectionStart = removeStart;
                    targetInputField.SelectionLength = oldBlock.Length;

                    // 삭제
                    targetInputField.SelectedText = string.Empty;

                    // 커서 위치 업데이트
                    targetInputField.SelectionStart = removeStart;
                }
                oldBlock = "";
            }
        }
        // 다른 폼에서 조합 버퍼 초기화
        public void ResetComposingBuffer()
        {
            composing = "";
            oldBlock = "";
            ResetIndexes();
        }

        public void FinalizeBlock()
        {
            if (!string.IsNullOrEmpty(composing))
            {
                RemoveOldBlock();
                string finalText = ProcessInput(composing);
                if (!string.IsNullOrEmpty(finalText))
                {
                    InsertTextAtCaret(finalText);
                }
                composing = "";
                oldBlock = "";
                ResetIndexes();
            }
        }


        private void MoveCaret(int direction)
        {
            if (targetInputField != null)
            {
                int newPosition = targetInputField.SelectionStart + direction;
                // 범위를 벗어나지 않도록 제한
                newPosition = Math.Max(0, Math.Min(newPosition, targetInputField.Text.Length));
                targetInputField.SelectionStart = newPosition;
                targetInputField.SelectionLength = 0;
                targetInputField.Focus();
            }
        }

        public string ProcessInput(string input)
        {
            string result = "";
            ResetIndexes();

            foreach (char c in input)
            {
                if (IsConsonant(c))
                {
                    HandleConsonant(c, ref result);
                }
                else if (IsMedial(c))
                {
                    HandleVowel(c, ref result);
                }
                else
                {
                    // 기타
                    result += c;
                }
            }

            if (initialIndex != -1)
            {
                result += GetComposedCharacter();
            }
            return result;
        }

        private void HandleConsonant(char c, ref string result)
        {
            string s = c.ToString();

            if (initialIndex == -1)
            {
                // 아직 초성 없으므로 초성 설정
                initialIndex = Array.IndexOf(Initials, s);
                currentInitial = s;
                return;
            }

            if (medialIndex == -1)
            {
                // 초성만 있고 다시 자음 => 이전 초성만 finalize
                result += GetComposedCharacter();
                ResetIndexes();

                initialIndex = Array.IndexOf(Initials, s);
                currentInitial = s;
                return;
            }

            if (finalIndex == -1)
            {
                // 종성 없음 => 종성 시도
                int fi = Array.IndexOf(Finals, s);
                if (fi == -1)
                {
                    result += GetComposedCharacter();
                    ResetIndexes();

                    initialIndex = Array.IndexOf(Initials, s);
                    currentInitial = s;
                }
                else
                {
                    finalIndex = fi;
                    currentFinal = s;
                }
            }
            else
            {
                // 이미 종성 => 복합 종성 시도
                string combo = currentFinal + s;
                if (ComplexFinals.TryGetValue(combo, out string merged))
                {
                    finalIndex = Array.IndexOf(Finals, merged);
                    currentFinal = merged;
                }
                else
                {
                    result += GetComposedCharacter();
                    ResetIndexes();

                    initialIndex = Array.IndexOf(Initials, s);
                    currentInitial = s;
                }
            }
        }

        // (★) 복합 종성 파쇄 + 단일 파쇄 + 초성만 자모
        private void HandleVowel(char c, ref string result)
        {
            string s = c.ToString();

            if (initialIndex == -1)
            {
                // 초성 없고 모음만
                result += s;
                return;
            }

            if (finalIndex != -1)
            {
                // 종성 있음 => Meltdown
                string finStr = Finals[finalIndex];

                // (1) 복합 종성 분해?
                if (MeltdownMap.TryGetValue(finStr, out var pair))
                {
                    // ex) ㄳ -> (ㄱ,ㅅ)
                    // 이전 음절: ㄱ만 종성, 새 음절 초성=ㅅ
                    string remain = pair.first;
                    string move = pair.second;

                    int remainIdx = Array.IndexOf(Finals, remain);
                    if (remainIdx == -1)
                    {
                        // 못 찾으면 => finalize
                        result += GetComposedCharacter();
                        ResetIndexes();
                        result += s;
                        return;
                    }

                    // 이전 음절 종성 = remain
                    finalIndex = remainIdx;
                    currentFinal = remain;
                    // finalize
                    result += GetComposedCharacter();

                    ResetIndexes();
                    // 새 초성 = move
                    int newInitIdx = Array.IndexOf(Initials, move);
                    if (newInitIdx == -1)
                    {
                        // 초성 배열에 없으면 => 그냥 모음
                        result += s;
                        return;
                    }
                    initialIndex = newInitIdx;
                    currentInitial = move;

                    // 새 중성
                    int mid = Array.IndexOf(Medials, s);
                    if (mid == -1)
                    {
                        result += s;
                    }
                    else
                    {
                        medialIndex = mid;
                        currentMedial = s;
                    }
                }
                else
                {
                    // (2) 단일 종성이면서 초성 배열에 있으면 => 파쇄
                    string finStr2 = Finals[finalIndex];
                    int possibleInitIdx = Array.IndexOf(Initials, finStr2);

                    if (possibleInitIdx != -1 && finStr2.Length == 1)
                    {
                        // 이전 음절은 종성 제외 finalize
                        int codeNoFinal = HangulBase + (initialIndex * 21 * 28)
                                          + (medialIndex * 28);
                        result += char.ConvertFromUtf32(codeNoFinal);

                        ResetIndexes();
                        // 새 초성
                        initialIndex = possibleInitIdx;
                        currentInitial = finStr2;

                        // 새 중성
                        int mid = Array.IndexOf(Medials, s);
                        if (mid == -1)
                        {
                            result += s;
                        }
                        else
                        {
                            medialIndex = mid;
                            currentMedial = s;
                        }
                    }
                    else
                    {
                        // 종성 분리 불가 -> finalize + 모음
                        result += GetComposedCharacter();
                        ResetIndexes();
                        result += s;
                    }
                }
                return;
            }

            // (3) 종성 없고, 중성 있는지 검사
            if (medialIndex == -1)
            {
                // 첫 중성
                medialIndex = Array.IndexOf(Medials, s);
                currentMedial = s;
            }
            else
            {
                // 이미 중성이 있으면 => 복합 중성
                char m1 = currentMedial[0];
                var merged = Korean.MergeMoeum(m1, c);
                if ((int)merged != -1)
                {
                    medialIndex = (int)merged;
                    currentMedial = Medials[medialIndex];
                }
                else
                {
                    // finalize + 새 모음
                    result += GetComposedCharacter();
                    ResetIndexes();
                    result += s;
                }
            }
        }

        private string GetComposedCharacter()
        {
            // (★) 초성만 (medial=-1, final=-1) => 자모
            if (initialIndex != -1 && medialIndex == -1 && finalIndex == -1)
            {
                string cho = Initials[initialIndex]; // 예: "ㄱ"
                // 호환 자모 매핑
                if (JamoMap.TryGetValue(cho, out int jamoCode))
                {
                    return char.ConvertFromUtf32(jamoCode); // "ㄱ"
                }
                else
                {
                    return cho; // 혹시 못찾으면 그냥 문자열
                }
            }

            // 초성+중성 이상 => 완성형 음절
            if (initialIndex == -1) return "";

            int code = HangulBase + (initialIndex * 21 * 28);
            if (medialIndex != -1)
            {
                code += (medialIndex * 28);
            }
            if (finalIndex != -1)
            {
                code += finalIndex;
            }
            return char.ConvertFromUtf32(code);
        }

        private void ResetIndexes()
        {
            initialIndex = -1;
            medialIndex = -1;
            finalIndex = -1;
            currentInitial = null;
            currentMedial = null;
            currentFinal = null;
        }

        private bool IsConsonant(char c) => Initials.Contains(c.ToString());
        private bool IsMedial(char c) => Medials.Contains(c.ToString());

        // 자판 레이아웃
        public void UpdateKeyboardLayout()
        {
            if (isKor)
            {
                if (isShift) UpdateKeysToUpperKor();
                else UpdateKeysToLowerKor();
            }
            else
            {
                if (isShift) UpdateKeysToUpperEng();
                else UpdateKeysToLowerEng();
            }

            // 모든 MyKeyButton의 이미지를 갱신
            foreach (var btn in this.Controls.OfType<MyKeyButton>())
            {
                btn.RefreshImage();
            }
        }

        public void UpdateKeysToLayout()
        {
            for (int i = 0; i < keysCtrlName.Length; i++)
            {
                for (int j = 0; j < keysCtrlName[i].Length; j++)
                {
                    Control[] keyControl = this.Controls.Find(keysCtrlName[i][j], true);
                    string controlName = keysCtrlName[i][j];

                    if (keyControl.Length > 0)
                    {
                        PictureBox keyPictureBox = keyControl[0] as PictureBox;

                        //Console.WriteLine("keyPictureBox : " + keyPictureBox);
                        if (keyPictureBox != null)
                        {
                            //Console.WriteLine($"Found PictureBox: {controlName}");

                            // Tag 값 업데이트
                            keyPictureBox.Tag = keys[i][j];
                            //keyPictureBox.Image = GetKeyImage(keys[i][j]); // 키에 맞는 이미지를 설정
                            //keyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // 이미지 크기 설정
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Warning: Control '{keysCtrlName[i][j]}' not found.");
                    }
                }
            }
        }

        public void UpdateKeysToLowerEng()
        {
            keys[0] = new string[] { "Next" };
            keys[1] = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "/" };
            keys[2] = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]" };
            keys[3] = new string[] { "a", "s", "d", "f", "g", "h", "j", "k", "l", "Enter" };
            keys[4] = new string[] { "Shift", "z", "x", "c", "v", "b", "n", "m", ",", ".", "Backspace" };
            keys[5] = new string[] { "SwitchLang", "Space", "@", "leftArrow", "rightArrow" };
            UpdateKeysToLayout();
        }

        public void UpdateKeysToUpperEng()
        {
            keys[0] = new string[] { "Next" };
            keys[1] = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "?" };
            keys[2] = new string[] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "{", "}" };
            keys[3] = new string[] { "A", "S", "D", "F", "G", "H", "J", "K", "L", "Enter" };
            keys[4] = new string[] { "Shift", "Z", "X", "C", "V", "B", "N", "M", "<", ">", "Backspace" };
            keys[5] = new string[] { "SwitchLang", "Space", "@", "leftArrow", "rightArrow" };
            UpdateKeysToLayout();
        }

        public void UpdateKeysToLowerKor()
        {
            keys[0] = new string[] { "Next" };
            keys[1] = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "/" };
            keys[2] = new string[] { "ㅂ", "ㅈ", "ㄷ", "ㄱ", "ㅅ", "ㅛ", "ㅕ", "ㅑ", "ㅐ", "ㅔ", "[", "]" };
            keys[3] = new string[] { "ㅁ", "ㄴ", "ㅇ", "ㄹ", "ㅎ", "ㅗ", "ㅓ", "ㅏ", "ㅣ", "Enter" };
            keys[4] = new string[] { "Shift", "ㅋ", "ㅌ", "ㅊ", "ㅍ", "ㅠ", "ㅜ", "ㅡ", ",", ".", "Backspace" };
            keys[5] = new string[] { "SwitchLang", "Space", "@", "leftArrow", "rightArrow" };
            UpdateKeysToLayout();
        }

        public void UpdateKeysToUpperKor()
        {
            keys[0] = new string[] { "Next" };
            keys[1] = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "?" };
            keys[2] = new string[] { "ㅃ", "ㅉ", "ㄸ", "ㄲ", "ㅆ", "ㅛ", "ㅕ", "ㅑ", "ㅒ", "ㅖ", "{", "}" };
            keys[3] = new string[] { "ㅁ", "ㄴ", "ㅇ", "ㄹ", "ㅎ", "ㅗ", "ㅓ", "ㅏ", "ㅣ", "Enter" };
            keys[4] = new string[] { "Shift", "ㅋ", "ㅌ", "ㅊ", "ㅍ", "ㅠ", "ㅜ", "ㅡ", "<", ">", "Backspace" };
            keys[5] = new string[] { "SwitchLang", "Space", "@", "leftArrow", "rightArrow" };
            UpdateKeysToLayout();
        }

        private void keyLayout1_Paint(object sender, PaintEventArgs e) { }
        private void background_Paint(object sender, PaintEventArgs e) { }

        private void leftArrow_Click(object sender, EventArgs e)
        {
            if (targetInputField.SelectionStart > 0)
            {
                targetInputField.SelectionStart--;
            }
        }

        private void rightArrow_Click(object sender, EventArgs e)
        {
            if (targetInputField.SelectionStart < targetInputField.Text.Length)
            {
                targetInputField.SelectionStart++;
            }
        }

        private void closeex(object sender, EventArgs e)
        {
            Console.WriteLine("현재 폼 : ", parentFormHandle);
            this.Close();
        }

        public void EnableKeyboard(bool enable)
        {
            this.Enabled = enable;
        }

        private void Backspace_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isBackspacePressed)
            {
                isBackspacePressed = true;
                lastBackspacePressTime = DateTime.Now;
                StartBackspaceTimer();
            }
            HandleBackspace();
        }

        private void Backspace_MouseUp(object sender, MouseEventArgs e)
        {
            if (isBackspacePressed)
            {
                isBackspacePressed = false;
                StopBackspaceTimer();
            }
        }
    }

}

