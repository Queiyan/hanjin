using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Threading.Tasks;
using System.Text;
using System.Xml.Linq;
using System.Timers;

namespace WinFormsApp1
{
    public partial class InvoiceSenderForm : Form
    {
        private readonly string mode;
        private CustomVirtualKeyboard keyboard;
        private NumpadForm numpad;

        private PictureBox clearButton;

        private bool isFormatting = false;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public InvoiceSenderForm(string sender)
        {

            InitializeComponent();
            this.Opacity = 0; // 초기 투명도 설정

            this.Show();
            // 폼에 더블 버퍼링 활성화
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            PhoneNoInput.GotFocus += PhonenoInput_GotFocus;
            PhoneNoInput.Validated += PhoneNoInput_FinalValidating;
            PhoneNoInput.TextChanged += PhoneNoInput_TextChanged;
            Address2Input.TextChanged += Address2Input_TextChanged;
            this.Focus();
            mode = sender;

            LoadData();
            this.Load += VoiceForm_Load; // 폼 로드 이벤트 핸들러 연결

            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                //this.BringToFront(); // 폼을 맨 앞으로 가져옴
                this.Opacity = 1; // 투명도 복원
            };

            // 60초 뒤 홈 화면으로 이동하는 타이머 초기화
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


        private void Address2Input_TextChanged(object sender, EventArgs e)
        {
            // 텍스트 박스의 커서를 텍스트의 끝으로 이동
            Address2Input.SelectionStart = Address2Input.Text.Length;
            Address2Input.ScrollToCaret();
        }

        private void PhoneNoInput_FinalValidating(object? sender, EventArgs e)
        {
            //keyboard = new CustomVirtualKeyboard(this.Handle);
            //keyboard.NextKeyPressed += Keyboard_NextKeyPressed;
            //ShowCustomVirtualKeyboard();
            //numpad.Close();
            if (keyboard != null && !keyboard.IsDisposed)
            {
                keyboard.Visible = true;
            }
            
            Task.Delay(300).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void PhonenoInput_GotFocus(object? sender, EventArgs e)
        {
            if (numpad == null || numpad.IsDisposed)
            {
                numpad = new NumpadForm();
                
                numpad.NextKeyPressed += numPad_NextKeyPressed;
                numpad.Show();
                Task.Delay(300).ContinueWith(t =>
                {
                    if (keyboard != null && !keyboard.IsDisposed)
                    {
                        keyboard.Visible = false;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            numpad.SetTargetInputField(PhoneNoInput); // 키보드의 대상 필드도 설정
            PhoneNoInput.Focus();
        }

        private void NameInput_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //SetCharacterSpacing(NameInput, NameInput.Text,5);
            string name = NameInput.Text;
            if (!string.IsNullOrEmpty(name))
            {
                NameInput.Clear();

                // 한글 이름을 입력한 경우, 각 글자 사이에 공백 추가
                NameInput.Text = string.Join(" ", name.ToCharArray());
            }
        }

        private void PhoneNoInput_TextChanged(object sender, EventArgs e)
        {
            if (isFormatting) return;

            isFormatting = true;

            string input = PhoneNoInput.Text.Replace("-", "");
            StringBuilder formatted = new StringBuilder();

            // 모든 가능한 한국 전화번호 접두사 목록
            string[] prefixes = {
                "010", "011", "016", "017", "018", "019",
                "02",
                "031", "032", "033", "041", "042", "043", "044",
                "051", "052", "053", "054", "055",
                "061", "062", "063", "064",
                "070", "080" };

            // 3자리 접두사 목록
            string[] threeDigitPrefixes = { "010", "011", "016", "017", "018", "019", "031", "032", "033", "041", "042", "043", "044",
                "051", "052", "053", "054", "055","061", "062", "063", "064", "070", "080"};

            // 입력된 번호의 접두사를 찾기
            string matchedPrefix = prefixes.FirstOrDefault(prefix => input.StartsWith(prefix));

            if (matchedPrefix != null)
            {
                formatted.Append(matchedPrefix);

                string remaining = input.Substring(matchedPrefix.Length);

                if (threeDigitPrefixes.Contains(matchedPrefix))
                {
                    if (remaining.Length > 0)
                    {
                        formatted.Append("-");
                        if (remaining.Length <= 7)
                        {
                            // 3자리 접두사: 010-xxx-xxxx
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
                            // 3자리 접두사: 010-xxxx-xxxx
                            if (remaining.Length >= 4)
                            {
                                formatted.Append(remaining.Substring(0, 4));
                                if (remaining.Length > 4)
                                {
                                    formatted.Append("-");
                                    formatted.Append(remaining.Substring(4, 4));
                                }
                            }
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
                            formatted.Append(remaining.Substring(4, 4));
                        }
                    }
                }
                else if (matchedPrefix.StartsWith("0502") || matchedPrefix.StartsWith("0503") ||
                         matchedPrefix.StartsWith("0504") || matchedPrefix.StartsWith("0505") ||
                         matchedPrefix.StartsWith("0506") || matchedPrefix.StartsWith("0507") ||
                         matchedPrefix.StartsWith("0508") || matchedPrefix.StartsWith("0509"))
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
                            formatted.Append(remaining.Substring(4, 4));
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
                            formatted.Append(remaining.Substring(4, 4));
                        }
                    }
                }
            }
            else
            {
                // 접두사가 리스트에 없는 경우 원래대로 표시
                formatted.Append(input);
            }

            PhoneNoInput.Text = formatted.ToString();
            PhoneNoInput.SelectionStart = PhoneNoInput.Text.Length;

            isFormatting = false;
        }





        private void RichTextBox_Click(object sender, EventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.FinalizeBlock();
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // 현재 ClearButton이 속한 패널을 가져옴
            Panel parentPanel = ((PictureBox)sender).Parent as Panel;

            if (parentPanel != null)
            {
                // 부모 패널 내의 모든 RichTextBox 컨트롤을 순회하며 Clear
                foreach (Control control in parentPanel.Controls)
                {
                    if (control is RichTextBox richTextBox)
                    {
                        richTextBox.Clear();
                        //richTextBox.Focus(); // RichTextBox에 포커스 설정
                    }
                }
            }

            // 가상 키보드의 조합 버퍼 초기화
            if (keyboard != null)
            {
                keyboard.ResetComposingBuffer();
            }
        }

        private void LoadData()
        {
            if (mode == "sender")
            {
                NameInput.Text = DataCtrl.SenderName;
                PhoneNoInput.Text = DataCtrl.SenderPhoneNo;
                AddressInput.Text = DataCtrl.SenderAddress;
                Address2Input.Text = DataCtrl.SenderAddress2;
                Address3Input.Text = DataCtrl.SenderAddress3;
            }
            else if (mode == "receiver")
            {
                NameInput.Text = DataCtrl.ReceiverName;
                PhoneNoInput.Text = DataCtrl.ReceiverPhoneNo;
                AddressInput.Text = DataCtrl.ReceiverAddress;
                Address2Input.Text = DataCtrl.ReceiverAddress2;
                Address3Input.Text = DataCtrl.ReceiverAddress3;
            }
        }

        // 화상키보드 확인 버튼
        private void Keyboard_NextKeyPressed(object sender, EventArgs e)
        {
            Console.WriteLine("Keyboard NextKeyPressed");
            Go_Next(sender, e);
        }

        // 넘패드 확인 버튼
        private void numPad_NextKeyPressed(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressInput.Text))
            {
                //numpad.Close();
                //AddressInput.Focus();
                if (keyboard != null && !keyboard.IsDisposed)
                {
                    keyboard.Visible = true;
                }
                AddressSearch_Click(sender, e);
                numpad.Close();
            } else
            {
                Go_Next(sender, e);
                numpad.Close();
            }
        }

        public void Go_Home(object sender, EventArgs e)
        {
            if (!MsgWindow.IsShowing)
            {
                DataCtrl.ClearAll();
                new HomeForm().Show();

                if (numpad != null && !numpad.IsDisposed)
                {
                    Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                    numpad.Close();
                }

                Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void Go_Back(object sender, EventArgs e)
        {
            if (!MsgWindow.IsShowing)
            {
                new TermsForm().Show();
                Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void Go_Next(object sender, EventArgs e)
        {
            Console.WriteLine("Go_Next 호출됨");
            if (!MsgWindow.IsShowing)
            {
                Console.WriteLine("메시지 없음");
                if (ValidateInputs())
                {
                    SaveData();

                    if (mode == "sender")
                    {
                        if (keyboard != null && !keyboard.IsDisposed)
                        {
                            keyboard.Close();
                        }
                        new InvoiceReceiverForm("receiver").Show();

                        // 마우스 커서 위치 초기화
                        Cursor.Position = new System.Drawing.Point(0, 300);

                        Task.Delay(500).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                        Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                    }

                }
                else
                {
                    Console.WriteLine("입력값 유효성 검사 실패");
                }
            }
            else
            {
                Console.WriteLine("메시지 창이 열려 있음");
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(NameInput.Text))
            {
                new MsgWindow("성명을 입력해 주세요").Show();
                NameInput.Focus();
                //keyboard.SetTargetInputField(NameInput);
                return false;
            }
            if (NameInput.Text.Trim().Length < 2)
            {
                new MsgWindow("성명은 최소\n2글자 이상이어야 합니다").Show();
                NameInput.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(PhoneNoInput.Text))
            {
                new MsgWindow("연락처를 입력해 주세요").Show();
                PhoneNoInput.Focus();
                //numpad.SetTargetInputField(PhoneNoInput);
                return false;
            }
            if (string.IsNullOrWhiteSpace(AddressInput.Text))
            {
                new MsgWindow("우편번호를 입력해 주세요").Show();
                return false;
            }
            if (string.IsNullOrWhiteSpace(Address2Input.Text))
            {
                new MsgWindow("주소를 입력해 주세요").Show();
                return false;
            }
            if (string.IsNullOrWhiteSpace(Address3Input.Text))
            {
                new MsgWindow("상세주소를 입력해 주세요").Show();
                return false;
            }

            // 전화번호 형식 검증
            string input = PhoneNoInput.Text;
            string[] validPrefixes = {
                "010", "011", "016", "017", "018", "019",
                "02",
                "031", "032", "033", "041", "042", "043", "044",
                "051", "052", "053", "054", "055",
                "061", "062", "063", "064",
                "070", "080"
            };

            bool hasValidPrefix = validPrefixes.Any(prefix => input.StartsWith(prefix + "-"));
            if (!hasValidPrefix)
            {
                if (!MsgWindow.IsShowing)
                {
                    new MsgWindow("유효한 연락처가 아닙니다").Show();
                    PhoneNoInput.Focus();
                    return false;
                }
            }
            else
            {
                var parts = input.Split('-');
                string prefix = parts[0];

                bool isValidFormat = false;

                if (prefix == "010")
                {
                    // 010-####-####
                    isValidFormat = parts.Length == 3 &&
                                    parts[1].Length == 4 &&
                                    parts[2].Length == 4 &&
                                    parts.All(p => p.All(char.IsDigit));
                }
                else if (prefix == "02")
                {
                    // 02-###-#### 또는 02-####-####
                    isValidFormat = (parts.Length == 3 &&
                                     (parts[1].Length == 3 || parts[1].Length == 4) &&
                                     parts[2].Length == 4 &&
                                     parts.All(p => p.All(char.IsDigit)));
                }
                else
                {
                    // 기타: xxx-xxx-xxxx 또는 xxx-xxxx-xxxx
                    isValidFormat = (parts.Length == 3 &&
                                     (parts[1].Length == 3 || parts[1].Length == 4) &&
                                     parts[2].Length == 4 &&
                                     parts.All(p => p.All(char.IsDigit)));
                }

                if (!isValidFormat)
                {
                    if (!MsgWindow.IsShowing)
                    {
                        new MsgWindow("유효한 연락처가 아닙니다").Show();
                        PhoneNoInput.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private void SaveData()
        {
            if (mode == "sender")
            {
                DataCtrl.SenderName = NameInput.Text;
                DataCtrl.SenderPhoneNo = PhoneNoInput.Text;
                DataCtrl.SenderAddress = AddressInput.Text;
                DataCtrl.SenderAddress2 = Address2Input.Text;
                DataCtrl.SenderAddress3 = Address3Input.Text;
            }
            else if (mode == "receiver")
            {
                DataCtrl.ReceiverName = NameInput.Text;
                DataCtrl.ReceiverPhoneNo = PhoneNoInput.Text;
                DataCtrl.ReceiverAddress = AddressInput.Text;
                DataCtrl.ReceiverAddress2 = Address2Input.Text;
                DataCtrl.ReceiverAddress3 = Address3Input.Text;
            }
        }

        

        private void VoiceForm_Load(object sender, EventArgs e)
        {
            // 가상 키보드 생성 및 표시
            ShowCustomVirtualKeyboard();

            // 폼 로드 시 첫 번째 입력 필드에 포커스 설정
            NameInput.Focus();
            keyboard.SetTargetInputField(NameInput); // 키보드의 대상 필드도 설정

            //Console.WriteLine($"ActiveControl after load: {this.ActiveControl?.Name ?? "null"}");

            // 모든 RichTextBox의 TextChanged 이벤트에 핸들러 추가
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control panelControl in panel.Controls)
                    {
                        if (panelControl is RichTextBox richTextBox)
                        {
                            //richTextBox.TextChanged += RichTextBox_TextChanged;
                            richTextBox.GotFocus += RichTextBox_GotFocus; // 포커스 이벤트 핸들러 추가
                            // MouseUp 이벤트 핸들러 등록
                            richTextBox.MouseUp += RichTextBox_MouseUp;
                            richTextBox.MouseDown += RichTextBox_MouseDown;
                            if (!richTextBox.Name.Equals("PhoneNoInput"))
                            {
                                //richTextBox.Click += RichTextBox_Click;
                               
                            }

                        }
                    }
                }
            }
        }
        // MouseUp 이벤트 핸들러 구현
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

        private void RichTextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                if (!richTextBox.Name.Equals("PhoneNoInput"))
                {
                    if (keyboard.Visible == false)
                    {
                        keyboard.Visible = true;
                    }
                    keyboard.SetTargetInputField(richTextBox);
                    richTextBox.Focus(); // 포커스 설정 (이미 포커스를 받았겠지만 명시적으로 설정)
                }
            }
        }
        private void ShowCustomVirtualKeyboard()
        {
            keyboard = new CustomVirtualKeyboard(this.Handle);
            keyboard.NextKeyPressed += Keyboard_NextKeyPressed;
            keyboard.Show();
        }

        // 우편번호 검색 버튼 클릭
        private void AddressSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (keyboard != null && !keyboard.IsDisposed)
                {
                    Task.Delay(800).ContinueWith(t => keyboard.Visible = false, TaskScheduler.FromCurrentSynchronizationContext());
                }

                var postcodeForm = new PostcodeSearchForm();

                postcodeForm.FormClosed += (s, args) =>
                {
                    if (postcodeForm.DialogResult == DialogResult.OK)
                    {
                        // 우편번호, 주소, 상세주소 가져오기
                        string zipCode = postcodeForm.ZipCode;
                        string address = postcodeForm.Address;
                        string detailedAddress = postcodeForm.DetailedAddress;

                        Console.WriteLine($"우편번호: {zipCode}, 주소: {address}, 상세 주소: {detailedAddress}"); // 디버깅 로그

                        // 우편번호 입력
                        HandleKeyInputAddress(zipCode);
                        // 주소 입력
                        HandleKeyInputAddress2(address);
                        // 상세 주소 입력
                        HandleKeyInputAddress3(detailedAddress);

                        if(AddressInput.Text != ""|| AddressInput.Text != null)
                        {
                            //numpad.Close();
                        }
                        Address3Input.Focus();
                        keyboard.SetTargetInputField(Address3Input);
                        //AddressInput.Focus();
                        numpad?.Close();

                    }

                    if (keyboard.Visible == false)
                    {
                        keyboard.Visible = true;
                        Address3Input.Focus();
                    }
                    this.ActiveControl = null;
                };

                postcodeForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // 가상 키보드에서 키 입력을 처리하는 메서드
        private void HandleKeyInputAddress(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            Console.WriteLine($"HandleKeyInputAddress 호출됨: {key}"); // 디버깅 로그

            // AddressInput에 입력
            AddressInput.Text = ""; // 텍스트 초기화
            AddressInput.AppendText(key);
        }

        private void HandleKeyInputAddress2(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            Console.WriteLine($"HandleKeyInputAddress2 호출됨: {key}"); // 디버깅 로그
            // Address2Input에 입력
            Address2Input.Text = ""; // 텍스트 초기화
            Address2Input.AppendText(key);
        }

        private void HandleKeyInputAddress3(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            Console.WriteLine($"HandleKeyInputAddress3 호출됨: {key}"); // 디버깅 로그
                                                                     // Address3Input에 입력
            Address3Input.Text = ""; // 텍스트 초기화
            Address3Input.AppendText(key);
        }

        private void sampleinput(object sender, EventArgs e)
        {
            NameInput.Text = "홍길동";
            PhoneNoInput.Text = "010-1234-5678";
            AddressInput.Text = "04098";
            Address2Input.Text = "서울특별시 마포구 창전로 70";
            Address3Input.Text = "6층";
        }
    }
}
