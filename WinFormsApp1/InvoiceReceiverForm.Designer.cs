using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class InvoiceReceiverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceReceiverForm));
            homeBtn = new PressKeyPictureBox();
            panel1 = new Panel();
            backBtn = new PressKeyPictureBox();
            PhoneNoContainer = new Panel();
            clearBtn2 = new PictureBox();
            PhoneNoInput = new RichTextBox();
            Address2Input = new RichTextBox();
            AddressInput = new RichTextBox();
            NameContainer = new Panel();
            clearBtn1 = new PictureBox();
            NameInput = new RichTextBox();
            NextBtn = new PictureBox();
            sendForm = new PictureBox();
            AddressContainer = new Panel();
            clearBtn3 = new PictureBox();
            Address3Input = new RichTextBox();
            AddressSearch = new PressKeyPictureBox();
            AddressLabel = new PictureBox();
            Address2Label = new PictureBox();
            Address3Label = new PictureBox();
            clearButton = new PictureBox();
            webViewControl = new Microsoft.Web.WebView2.WinForms.WebView2();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)homeBtn).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)backBtn).BeginInit();
            PhoneNoContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)clearBtn2).BeginInit();
            NameContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)clearBtn1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sendForm).BeginInit();
            AddressContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)clearBtn3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AddressSearch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AddressLabel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Address2Label).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Address3Label).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clearButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // homeBtn
            // 
            homeBtn.Image = (Image)resources.GetObject("homeBtn.Image");
            homeBtn.Location = new Point(0, 0);
            homeBtn.Margin = new Padding(0);
            homeBtn.Name = "homeBtn";
            homeBtn.NormalImage = (Image)resources.GetObject("homeBtn.NormalImage");
            homeBtn.PressedImage = (Image)resources.GetObject("homeBtn.PressedImage");
            homeBtn.Size = new Size(180, 180);
            homeBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            homeBtn.TabIndex = 1;
            homeBtn.TabStop = false;
            homeBtn.Click += Go_Home;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(backBtn);
            panel1.Controls.Add(homeBtn);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 180);
            panel1.TabIndex = 0;
            // 
            // backBtn
            // 
            backBtn.Image = (Image)resources.GetObject("backBtn.Image");
            backBtn.Location = new Point(845, 0);
            backBtn.Margin = new Padding(0);
            backBtn.Name = "backBtn";
            backBtn.NormalImage = (Image)resources.GetObject("backBtn.NormalImage");
            backBtn.PressedImage = (Image)resources.GetObject("backBtn.PressedImage");
            backBtn.Size = new Size(180, 180);
            backBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            backBtn.TabIndex = 0;
            backBtn.TabStop = false;
            backBtn.Click += Go_Back;
            // 
            // PhoneNoContainer
            // 
            PhoneNoContainer.BackgroundImage = (Image)resources.GetObject("PhoneNoContainer.BackgroundImage");
            PhoneNoContainer.Controls.Add(clearBtn2);
            PhoneNoContainer.Controls.Add(PhoneNoInput);
            PhoneNoContainer.Location = new Point(118, 445);
            PhoneNoContainer.Name = "PhoneNoContainer";
            PhoneNoContainer.Size = new Size(797, 61);
            PhoneNoContainer.TabIndex = 1;
            // 
            // clearBtn2
            // 
            clearBtn2.Image = (Image)resources.GetObject("clearBtn2.Image");
            clearBtn2.Location = new Point(745, 15);
            clearBtn2.Name = "clearBtn2";
            clearBtn2.Size = new Size(32, 32);
            clearBtn2.TabIndex = 3;
            clearBtn2.TabStop = false;
            clearBtn2.Click += ClearButton_Click;
            // 
            // PhoneNoInput
            // 
            PhoneNoInput.AutoWordSelection = true;
            PhoneNoInput.BorderStyle = BorderStyle.None;
            PhoneNoInput.Font = new Font("Pretendard", 20.25F);
            PhoneNoInput.ImeMode = ImeMode.On;
            PhoneNoInput.Location = new Point(150, 15);
            PhoneNoInput.Multiline = false;
            PhoneNoInput.Name = "PhoneNoInput";
            PhoneNoInput.RightToLeft = RightToLeft.Yes;
            PhoneNoInput.ScrollBars = RichTextBoxScrollBars.None;
            PhoneNoInput.Size = new Size(500, 40);
            PhoneNoInput.TabIndex = 1;
            PhoneNoInput.Text = "";
            // 
            // Address2Input
            // 
            Address2Input.AutoWordSelection = true;
            Address2Input.BackColor = Color.White;
            Address2Input.BorderStyle = BorderStyle.None;
            Address2Input.Font = new Font("Pretendard", 15F);
            Address2Input.ImeMode = ImeMode.On;
            Address2Input.Location = new Point(21, 125);
            Address2Input.Multiline = false;
            Address2Input.Name = "Address2Input";
            Address2Input.ReadOnly = true;
            Address2Input.ScrollBars = RichTextBoxScrollBars.None;
            Address2Input.Size = new Size(372, 32);
            Address2Input.TabIndex = 3;
            Address2Input.Text = "";
            Address2Input.Click += AddressSearch_Click;
            // 
            // AddressInput
            // 
            AddressInput.AutoWordSelection = true;
            AddressInput.BackColor = Color.White;
            AddressInput.BorderStyle = BorderStyle.None;
            AddressInput.Font = new Font("Pretendard", 20.25F);
            AddressInput.ImeMode = ImeMode.On;
            AddressInput.Location = new Point(37, 68);
            AddressInput.MaxLength = 10;
            AddressInput.Multiline = false;
            AddressInput.Name = "AddressInput";
            AddressInput.ReadOnly = true;
            AddressInput.ScrollBars = RichTextBoxScrollBars.None;
            AddressInput.Size = new Size(116, 32);
            AddressInput.TabIndex = 2;
            AddressInput.Text = "";
            AddressInput.Click += AddressSearch_Click;
            // 
            // NameContainer
            // 
            NameContainer.BackgroundImage = (Image)resources.GetObject("NameContainer.BackgroundImage");
            NameContainer.Controls.Add(clearBtn1);
            NameContainer.Controls.Add(NameInput);
            NameContainer.Location = new Point(118, 332);
            NameContainer.Name = "NameContainer";
            NameContainer.Size = new Size(797, 61);
            NameContainer.TabIndex = 1;
            // 
            // clearBtn1
            // 
            clearBtn1.Image = (Image)resources.GetObject("clearBtn1.Image");
            clearBtn1.Location = new Point(745, 15);
            clearBtn1.Name = "clearBtn1";
            clearBtn1.Size = new Size(32, 32);
            clearBtn1.TabIndex = 1;
            clearBtn1.TabStop = false;
            clearBtn1.Click += ClearButton_Click;
            // 
            // NameInput
            // 
            NameInput.AutoWordSelection = true;
            NameInput.BorderStyle = BorderStyle.None;
            NameInput.Font = new Font("Pretendard", 20.25F);
            NameInput.ImeMode = ImeMode.On;
            NameInput.Location = new Point(150, 13);
            NameInput.Multiline = false;
            NameInput.Name = "NameInput";
            NameInput.RightToLeft = RightToLeft.Yes;
            NameInput.ScrollBars = RichTextBoxScrollBars.None;
            NameInput.Size = new Size(500, 40);
            NameInput.TabIndex = 0;
            NameInput.Text = "";
            // 
            // NextBtn
            // 
            NextBtn.Image = (Image)resources.GetObject("NextBtn.Image");
            NextBtn.Location = new Point(0, 1750);
            NextBtn.Name = "NextBtn";
            NextBtn.Size = new Size(1080, 170);
            NextBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            NextBtn.TabIndex = 0;
            NextBtn.TabStop = false;
            NextBtn.Click += Go_Next;
            // 
            // sendForm
            // 
            sendForm.Image = (Image)resources.GetObject("sendForm.Image");
            sendForm.Location = new Point(110, 235);
            sendForm.Name = "sendForm";
            sendForm.Size = new Size(805, 475);
            sendForm.TabIndex = 4;
            sendForm.TabStop = false;
            // 
            // AddressContainer
            // 
            AddressContainer.BackColor = Color.Transparent;
            AddressContainer.BackgroundImageLayout = ImageLayout.None;
            AddressContainer.Controls.Add(clearBtn3);
            AddressContainer.Controls.Add(Address2Input);
            AddressContainer.Controls.Add(Address3Input);
            AddressContainer.Controls.Add(AddressSearch);
            AddressContainer.Controls.Add(AddressInput);
            AddressContainer.Controls.Add(AddressLabel);
            AddressContainer.ForeColor = Color.Transparent;
            AddressContainer.Location = new Point(110, 552);
            AddressContainer.Name = "AddressContainer";
            AddressContainer.Size = new Size(805, 160);
            AddressContainer.TabIndex = 2;
            // 
            // clearBtn3
            // 
            clearBtn3.Image = (Image)resources.GetObject("clearBtn3.Image");
            clearBtn3.Location = new Point(753, 59);
            clearBtn3.Name = "clearBtn3";
            clearBtn3.Size = new Size(32, 32);
            clearBtn3.TabIndex = 6;
            clearBtn3.TabStop = false;
            clearBtn3.Click += ClearButton_Click;
            // 
            // Address3Input
            // 
            Address3Input.AutoWordSelection = true;
            Address3Input.BackColor = Color.White;
            Address3Input.BorderStyle = BorderStyle.None;
            Address3Input.Font = new Font("Pretendard", 17F);
            Address3Input.ImeMode = ImeMode.On;
            Address3Input.Location = new Point(411, 128);
            Address3Input.Multiline = false;
            Address3Input.Name = "Address3Input";
            Address3Input.ReadOnly = true;
            Address3Input.ScrollBars = RichTextBoxScrollBars.None;
            Address3Input.Size = new Size(374, 32);
            Address3Input.TabIndex = 4;
            Address3Input.Text = "";
            // 
            // AddressSearch
            // 
            AddressSearch.Image = (Image)resources.GetObject("AddressSearch.Image");
            AddressSearch.Location = new Point(210, 59);
            AddressSearch.Name = "AddressSearch";
            AddressSearch.NormalImage = (Image)resources.GetObject("AddressSearch.NormalImage");
            AddressSearch.PressedImage = (Image)resources.GetObject("AddressSearch.PressedImage");
            AddressSearch.Size = new Size(161, 41);
            AddressSearch.TabIndex = 5;
            AddressSearch.TabStop = false;
            AddressSearch.Click += AddressSearch_Click;
            // 
            // AddressLabel
            // 
            AddressLabel.Image = (Image)resources.GetObject("AddressLabel.Image");
            AddressLabel.Location = new Point(8, 3);
            AddressLabel.Name = "AddressLabel";
            AddressLabel.Size = new Size(167, 100);
            AddressLabel.TabIndex = 4;
            AddressLabel.TabStop = false;
            // 
            // Address2Label
            // 
            Address2Label.Image = (Image)resources.GetObject("Address2Label.Image");
            Address2Label.Location = new Point(118, 718);
            Address2Label.Name = "Address2Label";
            Address2Label.Size = new Size(395, 32);
            Address2Label.TabIndex = 6;
            Address2Label.TabStop = false;
            // 
            // Address3Label
            // 
            Address3Label.Image = (Image)resources.GetObject("Address3Label.Image");
            Address3Label.Location = new Point(519, 718);
            Address3Label.Name = "Address3Label";
            Address3Label.Size = new Size(396, 32);
            Address3Label.TabIndex = 7;
            Address3Label.TabStop = false;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(0, 0);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(100, 50);
            clearButton.TabIndex = 0;
            clearButton.TabStop = false;
            // 
            // webViewControl
            // 
            webViewControl.AllowExternalDrop = true;
            webViewControl.CreationProperties = null;
            webViewControl.DefaultBackgroundColor = Color.White;
            webViewControl.Location = new Point(12, 12);
            webViewControl.Name = "webViewControl";
            webViewControl.Size = new Size(800, 450);
            webViewControl.TabIndex = 0;
            webViewControl.ZoomFactor = 1D;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(930, 200);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 75);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            pictureBox1.Click += sampleinput;
            // 
            // InvoiceReceiverForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(pictureBox1);
            Controls.Add(AddressContainer);
            Controls.Add(Address2Label);
            Controls.Add(Address3Label);
            Controls.Add(NextBtn);
            Controls.Add(panel1);
            Controls.Add(NameContainer);
            Controls.Add(PhoneNoContainer);
            Controls.Add(sendForm);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "InvoiceReceiverForm";
            ShowInTaskbar = false;
            Text = "VoiceForm";
            WindowState = FormWindowState.Maximized;
            Load += VoiceForm_Load;
            ((System.ComponentModel.ISupportInitialize)homeBtn).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)backBtn).EndInit();
            PhoneNoContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)clearBtn2).EndInit();
            NameContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)clearBtn1).EndInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)sendForm).EndInit();
            AddressContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)clearBtn3).EndInit();
            ((System.ComponentModel.ISupportInitialize)AddressSearch).EndInit();
            ((System.ComponentModel.ISupportInitialize)AddressLabel).EndInit();
            ((System.ComponentModel.ISupportInitialize)Address2Label).EndInit();
            ((System.ComponentModel.ISupportInitialize)Address3Label).EndInit();
            ((System.ComponentModel.ISupportInitialize)clearButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel NameContainer;
        private Panel PhoneNoContainer;
        private PressKeyPictureBox homeBtn;
        private PressKeyPictureBox backBtn;
        private RichTextBox PhoneNoInput;
        private RichTextBox Address2Input;
        private RichTextBox AddressInput;
        private RichTextBox NameInput;
        private PictureBox NextBtn;
        private PictureBox sendForm;
        private PictureBox clearBtn1;
        private Panel AddressContainer;
        private RichTextBox Address3Input;
        private PictureBox AddressLabel;
        private PressKeyPictureBox AddressSearch;
        private PictureBox Address2Label;
        private PictureBox Address3Label;
        private PictureBox clearBtn2;
        private PictureBox clearBtn3;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewControl;
        private PictureBox pictureBox1;
    }
}