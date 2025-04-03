using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class TermsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TermsForm));
            backBtn = new PressKeyPictureBox();
            panel1 = new Panel();
            homeBtn = new PressKeyPictureBox();
            VoiceBtn = new PressKeyPictureBox();
            customScrollBar = new Panel();
            scrollHandle = new PictureBox();
            termsTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)backBtn).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)homeBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VoiceBtn).BeginInit();
            customScrollBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scrollHandle).BeginInit();
            SuspendLayout();
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
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(backBtn);
            panel1.Controls.Add(homeBtn);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1025, 180);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
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
            // VoiceBtn
            // 
            VoiceBtn.BackColor = Color.White;
            VoiceBtn.Image = (Image)resources.GetObject("VoiceBtn.Image");
            VoiceBtn.Location = new Point(34, 1107);
            VoiceBtn.Margin = new Padding(0);
            VoiceBtn.Name = "VoiceBtn";
            VoiceBtn.NormalImage = (Image)resources.GetObject("VoiceBtn.NormalImage");
            VoiceBtn.PressedImage = (Image)resources.GetObject("VoiceBtn.PressedImage");
            VoiceBtn.Size = new Size(956, 139);
            VoiceBtn.SizeMode = PictureBoxSizeMode.Zoom;
            VoiceBtn.TabIndex = 2;
            VoiceBtn.TabStop = false;
            VoiceBtn.Click += Go_Voice;
            // 
            // customScrollBar
            // 
            customScrollBar.BackColor = Color.White;
            customScrollBar.BackgroundImage = (Image)resources.GetObject("customScrollBar.BackgroundImage");
            customScrollBar.BackgroundImageLayout = ImageLayout.None;
            customScrollBar.Controls.Add(scrollHandle);
            customScrollBar.ImeMode = ImeMode.NoControl;
            customScrollBar.Location = new Point(992, 300);
            customScrollBar.Margin = new Padding(0);
            customScrollBar.Name = "customScrollBar";
            customScrollBar.Size = new Size(26, 688);
            customScrollBar.TabIndex = 0;
            customScrollBar.Visible = false;
            customScrollBar.Paint += customScrollBar_Paint;
            // 
            // scrollHandle
            // 
            scrollHandle.BackColor = Color.DarkGray;
            scrollHandle.BackgroundImage = (Image)resources.GetObject("scrollHandle.BackgroundImage");
            scrollHandle.Location = new Point(2, 0);
            scrollHandle.Name = "scrollHandle";
            scrollHandle.Size = new Size(22, 85);
            scrollHandle.TabIndex = 0;
            scrollHandle.TabStop = false;
            scrollHandle.Click += scrollHandle_Click;
            scrollHandle.MouseDown += ScrollHandle_MouseDown;
            scrollHandle.MouseMove += ScrollHandle_MouseMove;
            scrollHandle.MouseUp += ScrollHandle_MouseUp;
            // 
            // termsTextBox
            // 
            termsTextBox.BackColor = Color.White;
            termsTextBox.BorderStyle = BorderStyle.None;
            termsTextBox.Enabled = false;
            termsTextBox.Font = new Font("Arial", 24F);
            termsTextBox.Location = new Point(34, 214);
            termsTextBox.Margin = new Padding(0);
            termsTextBox.Multiline = true;
            termsTextBox.Name = "termsTextBox";
            termsTextBox.ReadOnly = true;
            termsTextBox.ShortcutsEnabled = false;
            termsTextBox.Size = new Size(956, 859);
            termsTextBox.TabIndex = 1;
            termsTextBox.TabStop = false;
            termsTextBox.Text = resources.GetString("termsTextBox.Text");
            termsTextBox.TextChanged += TermsTextBox_TextChanged;
            termsTextBox.MouseDown += TermsTextBox_MouseDown;
            // 
            // TermsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(customScrollBar);
            Controls.Add(termsTextBox);
            Controls.Add(VoiceBtn);
            Controls.Add(panel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "TermsForm";
            ShowInTaskbar = false;
            Text = "KioskForm";
            WindowState = FormWindowState.Maximized;
            Load += KioskForm_Load;
            ((System.ComponentModel.ISupportInitialize)backBtn).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)homeBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)VoiceBtn).EndInit();
            customScrollBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scrollHandle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PressKeyPictureBox backBtn;
        private System.Windows.Forms.Panel panel1;
        private PressKeyPictureBox homeBtn;
        private PressKeyPictureBox VoiceBtn;
        private System.Windows.Forms.TextBox termsTextBox;
        private System.Windows.Forms.Panel customScrollBar;
        private System.Windows.Forms.PictureBox scrollHandle;
    }
}

