using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Threading.Tasks;
using System.Text;
using System.Xml.Linq;
using System.Timers;

namespace WinFormsApp1
{
    public partial class InvoiceProductForm : Form
    {
        public string productCategory = "";
        public string goodsName = "";
        public string requests = "";

        public bool showUnabled = false;
        public bool showInfoBox = false;

        private CustomVirtualKeyboard keyboard;

        private NumpadForm numpad;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public InvoiceProductForm()
        {
            this.Opacity = 0;
            InitializeComponent();

            this.Show();
            // 폼에 더블 버퍼링 활성화
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            //ProductPriceInput.Click += ProductPriceInput_ClickHandler;
            ProductPriceInput.GotFocus += ProductPriceInput_GotFocus;
            ProductPriceInput.Validated += ProductPriceInput_FinalValidating;
            ProductPriceInput.TextChanged += ProductPriceInput_TextChanged;
            ProductPriceInput.Enter += Control_Enter;

            RequestInputField.GotFocus += RichTextBox_GotFocus;
            RequestInputField.MouseUp += RichTextBox_MouseUp;
            RequestInputField.MouseDown += RichTextBox_MouseDown;

            // Enter 이벤트 핸들러 추가
            ProductCategory.MouseClick += Control_Enter;
            ProductCategory.SelectedIndexChanged += ProductCategorySelected;
            
            // 콤보박스 항목 설정
            //ProductCategory.Items.Add("");
            ProductCategory.Items.Add("컴퓨터류");
            ProductCategory.Items.Add("가전제품");
            ProductCategory.Items.Add("소형전자");
            ProductCategory.Items.Add("과일류");
            ProductCategory.Items.Add("육류");
            ProductCategory.Items.Add("비닐팩류");
            ProductCategory.Items.Add("일반식품");
            ProductCategory.Items.Add("농산물");
            ProductCategory.Items.Add("수산물");
            ProductCategory.Items.Add("김치류");
            ProductCategory.Items.Add("생활용품");
            ProductCategory.Items.Add("의류잡화");
            ProductCategory.Items.Add("서적문구");
            ProductCategory.Items.Add("귀금속");
            ProductCategory.Items.Add("가구류");
            ProductCategory.Items.Add("기타");
            // 콤보박스 초기 값 설정
            ProductCategory.Text = "";

            LoadData();
            this.Load += VoiceForm_Load;
            this.Focus();

            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                if (ProductCategory.Text == "")
                {
                    ProductCategory.DroppedDown = true;
                }
                this.Opacity = 1;
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

        private void Control_Enter(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                comboBox.DroppedDown = true; // 드롭다운 열기
                comboBox.SelectionStart = comboBox.Text.Length; // 텍스트 선택 해제
                comboBox.SelectionLength = 0; // 텍스트 선택 해제
            }
            else if (sender is RichTextBox richTextBox)
            {
                richTextBox.SelectionStart = richTextBox.Text.Length; // 텍스트 선택 해제
                richTextBox.SelectionLength = 0; // 텍스트 선택 해제
            }
        }

        private void ProductCategory_Enter()
        {
            Task.Delay(100).ContinueWith(t => ProductCategory.DroppedDown = true, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ProductPriceInput_FinalValidating(object? sender, EventArgs e)
        {
            //keyboard = new CustomVirtualKeyboard(this.Handle);
            //keyboard.NextKeyPressed += Keyboard_NextKeyPressed;
            //keyboard.Show();
            
            Task.Delay(100).ContinueWith(t => numpad.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(100).ContinueWith(t => keyboard.Enabled = true, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ProductPriceInput_GotFocus(object? sender, EventArgs e)
        {
            if (numpad == null || numpad.IsDisposed)
            {
                numpad = new NumpadForm();

                numpad.NextKeyPressed += Numpad_NextKeyPressed;
                numpad.Show();
                Task.Delay(550).ContinueWith(t => keyboard.Enabled = false, TaskScheduler.FromCurrentSynchronizationContext());
            }
            numpad.SetTargetInputField(ProductPriceInput); // 키보드의 대상 필드도 설정
            ProductPriceInput.Focus();
        }
        private void ShowCustomVirtualKeyboard()
        {
            keyboard = new CustomVirtualKeyboard(this.Handle);
            keyboard.NextKeyPressed += Keyboard_NextKeyPressed;
            keyboard.Show();
        }

        private void Keyboard_NextKeyPressed(object sender, EventArgs e)
        {
            Go_Next(sender, e);
        }
        private void Numpad_NextKeyPressed(object sender, EventArgs e)
        {
            Task.Delay(300).ContinueWith(t => keyboard.Enabled = true, TaskScheduler.FromCurrentSynchronizationContext());
            keyboard.SetTargetInputField(RequestInputField); // 키보드의 대상 필드도 설정
            RequestInputField.Focus();
        }
        public void OpenNumpad(object sender, EventArgs e)
        {

            RichTextBox target = (RichTextBox)sender;

            VirtualKeyboardCtrl.OpenVirtualNumpad(target);
        }
        public void CloseKeyboard(object sender, EventArgs e)
        {
            VirtualKeyboardCtrl.CloseVirtualKeyboard();
        }

        public void CloseNumpad(object sender, EventArgs e)
        {
            VirtualKeyboardCtrl.CloseVirtualNumpad();
        }
        public void Go_Home(object sender, EventArgs e)
        {
            DataCtrl.ClearAll();
            new HomeForm();
            keyboard.Close();
            Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            //this.Close();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            if (!MsgWindow.IsShowing)
            {
                new InvoiceReceiverForm("receiver").Show();
                //keyboard.Close();
                Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                //this.Close();
                Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void Go_Next(object sender, EventArgs e)
        {
            if (ProductCategory.Text != "")
            {
                if (ProductPriceInput.Text != "")
                {
                    SaveData();
                    //CloseAllKeyboards();
                    VolumeForm weightsForm = new VolumeForm();
                    //keyboard.Close();
                    Task.Delay(600).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                    //this.Close();
                    Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                    // 마우스 커서 위치 초기화
                    Cursor.Position = new System.Drawing.Point(0, 300);
                }
                else
                {
                    Console.WriteLine("물품 가액을 입력해주세요.");
                    MsgWindow msgWindow = new MsgWindow("물품 가액을 입력해주세요");
                    msgWindow.FormClosed += (s, e) => ProductPriceInput_GotFocus(sender, e);

                    //ProductPriceInput_GotFocus(sender, e);

                }
            }
            else
            {
                Console.WriteLine("물품정보를 선택해주세요.");
                MsgWindow msgWindow = new MsgWindow("물품정보를 선택해주세요");
                msgWindow.FormClosed += (s, e) => ProductCategory_Enter();
                //ProductCategory_Enter();
            }
        }
        private void SaveData()
        {
            DataCtrl.ProductCategory = ProductCategory.Text;
            DataCtrl.ProductPriceInput = ProductPriceInput.Text;
            DataCtrl.RequestInputField = RequestInputField.Text;
        }
        private void LoadData()
        {
            ProductCategory.Text = DataCtrl.ProductCategory;
            ProductPriceInput.Text = DataCtrl.ProductPriceInput;
            RequestInputField.Text = DataCtrl.RequestInputField;
        }
        private void ProductCategorySelected(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                // 선택된 항목의 텍스트를 productCategory 변수에 저장
                productCategory = comboBox.Text;

                // 텍스트 선택 해제
                comboBox.SelectionStart = comboBox.Text.Length;
                comboBox.SelectionLength = 0;
            }
        }

        private void GoodsNameEntered(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goodsName = ProductPriceInput.Text.Replace("\n", "").Replace("\r", "");
            }
        }

        //private void RequestSelected(object sender, EventArgs e)
        //{
        //    requests = RequestComboBox.Text;
        //}

        private void RequestInput(object sender, EventArgs e)
        {
            RequestInputField.BringToFront();
        }

        private void ShowUnabledItems(object sender, EventArgs e)
        {
            showUnabled = !showUnabled;
            if (showUnabled)
            {
                UnabledInformation.Height = 900;
            }
            else
            {
                UnabledInformation.Height = 0;
            }

            showInfoBox = false;
            InformationBox.Height = 0;
        }

        private void ShowInformation(object sender, EventArgs e)
        {
            showInfoBox = !showInfoBox;
            if (showInfoBox)
            {
                InformationBox.Height = 900;
            }
            else
            {
                InformationBox.Height = 0;
            }

            showUnabled = false;
            UnabledInformation.Height = 0;
        }
        private void VoiceForm_Load(object sender, EventArgs e)
        {
            // 가상 키보드 생성 및 표시
            ShowCustomVirtualKeyboard();

            // 폼 로드 시 첫 번째 입력 필드에 포커스 설정
            RequestInputField.Focus();
            keyboard.SetTargetInputField(RequestInputField); // 키보드의 대상 필드도 설정

            Console.WriteLine($"ActiveControl after load: {this.ActiveControl?.Name ?? "null"}");


        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null)
            {
                Panel parentPanel = richTextBox.Parent as Panel;
                if (parentPanel != null)
                {
                    foreach (Control control in parentPanel.Controls)
                    {
                        if (control is PictureBox pictureBox && pictureBox.Name.Contains("Example"))
                        {
                            pictureBox.Visible = string.IsNullOrWhiteSpace(richTextBox.Text);
                        }
                    }
                }
            }
        }

        // ProductPriceInput에 대한 자동 콤마 삽입
        private void ProductPriceInput_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = ProductPriceInput.SelectionStart;
            string text = ProductPriceInput.Text.Replace(",", "");
            if (decimal.TryParse(text, out decimal value))
            {
                if (value == 0)
                {
                    MsgWindow msgWindow = new MsgWindow("가격은 0보다 커야 합니다");
                    ProductPriceInput.Text = "";
                    return;
                }
                string formatted = value.ToString("N0");
                if (formatted != ProductPriceInput.Text)
                {
                    ProductPriceInput.Text = formatted;
                    ProductPriceInput.SelectionStart = Math.Min(selectionStart + (formatted.Length - text.Length), formatted.Length);
                }
            }
        }

        private void RichTextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                if (!richTextBox.Name.Equals("ProductPriceInput"))
                {
                    keyboard.Enabled = true;
                    keyboard.SetTargetInputField(richTextBox);
                    richTextBox.Focus(); // 포커스 설정 (이미 포커스를 받았겠지만 명시적으로 설정)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Focus(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                Panel parentPanel = pictureBox.Parent as Panel;
                if (parentPanel != null)
                {
                    foreach (Control control in parentPanel.Controls)
                    {
                        if (control is ComboBox comboBox)
                        {
                            comboBox.DroppedDown = true;
                            comboBox.SelectionStart = comboBox.Text.Length; // 텍스트 선택 해제
                            comboBox.SelectionLength = 0; // 텍스트 선택 해제
                            return;
                        }
                        else if (control is TextBox textBox)
                        {
                            textBox.Focus();
                            return;
                        }
                        else if (control is RichTextBox richTextBox)
                        {
                            richTextBox.Focus();
                            return;
                        }
                    }
                }
            }
        }
        private void sampleinput(object sender, EventArgs e)
        {
            ProductCategory.Text = "가전제품";
            ProductPriceInput.Text = "1,000,000";
        }
    }
}
