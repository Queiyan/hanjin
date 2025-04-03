using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class ReservationMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReservationMenu));
            panel1 = new Panel();
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            ReservNumInput = new RichTextBox();
            ReservNumInputArea = new Panel();
            backSpaceBtn = new PictureBox();
            pictureBox1 = new PictureBox();
            devBtn = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            ReservNumInputArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)backSpaceBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)devBtn).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(BackBtn);
            panel1.Controls.Add(HomeBtn);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 180);
            panel1.TabIndex = 0;
            // 
            // BackBtn
            // 
            BackBtn.Image = (Image)resources.GetObject("BackBtn.Image");
            BackBtn.Location = new Point(844, 0);
            BackBtn.Name = "BackBtn";
            BackBtn.NormalImage = (Image)resources.GetObject("BackBtn.NormalImage");
            BackBtn.PressedImage = (Image)resources.GetObject("BackBtn.PressedImage");
            BackBtn.Size = new Size(180, 180);
            BackBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            BackBtn.TabIndex = 1;
            BackBtn.TabStop = false;
            BackBtn.Click += Go_Back;
            // 
            // HomeBtn
            // 
            HomeBtn.Image = (Image)resources.GetObject("HomeBtn.Image");
            HomeBtn.Location = new Point(0, 0);
            HomeBtn.Name = "HomeBtn";
            HomeBtn.NormalImage = (Image)resources.GetObject("HomeBtn.NormalImage");
            HomeBtn.PressedImage = (Image)resources.GetObject("HomeBtn.PressedImage");
            HomeBtn.Size = new Size(180, 180);
            HomeBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            HomeBtn.TabIndex = 0;
            HomeBtn.TabStop = false;
            HomeBtn.Click += Go_Home;
            // 
            // ReservNumInput
            // 
            ReservNumInput.BackColor = Color.White;
            ReservNumInput.BorderStyle = BorderStyle.None;
            ReservNumInput.Font = new Font("Pretendard", 50F);
            ReservNumInput.Location = new Point(31, 34);
            ReservNumInput.Multiline = false;
            ReservNumInput.Name = "ReservNumInput";
            ReservNumInput.RightToLeft = RightToLeft.No;
            ReservNumInput.ScrollBars = RichTextBoxScrollBars.None;
            ReservNumInput.Size = new Size(369, 85);
            ReservNumInput.TabIndex = 1;
            ReservNumInput.Text = "";
            // 
            // ReservNumInputArea
            // 
            ReservNumInputArea.BackgroundImage = (Image)resources.GetObject("ReservNumInputArea.BackgroundImage");
            ReservNumInputArea.Controls.Add(backSpaceBtn);
            ReservNumInputArea.Controls.Add(ReservNumInput);
            ReservNumInputArea.Location = new Point(245, 308);
            ReservNumInputArea.Name = "ReservNumInputArea";
            ReservNumInputArea.Size = new Size(534, 154);
            ReservNumInputArea.TabIndex = 2;
            // 
            // backSpaceBtn
            // 
            backSpaceBtn.Image = (Image)resources.GetObject("backSpaceBtn.Image");
            backSpaceBtn.Location = new Point(406, 38);
            backSpaceBtn.Name = "backSpaceBtn";
            backSpaceBtn.Size = new Size(118, 74);
            backSpaceBtn.TabIndex = 2;
            backSpaceBtn.TabStop = false;
            backSpaceBtn.MouseDown += Backspace_MouseDown;
            backSpaceBtn.MouseUp += Backspace_MouseUp;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(84, 558);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(857, 103);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // devBtn
            // 
            devBtn.Location = new Point(51, 218);
            devBtn.Name = "devBtn";
            devBtn.Size = new Size(100, 100);
            devBtn.TabIndex = 4;
            devBtn.TabStop = false;
            devBtn.Click += DevBtn_Click;
            // 
            // ReservationMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(devBtn);
            Controls.Add(pictureBox1);
            Controls.Add(ReservNumInputArea);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReservationMenu";
            ShowInTaskbar = false;
            Text = "EasyMenu";
            WindowState = FormWindowState.Maximized;
            Load += EasyMenu_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            ReservNumInputArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)backSpaceBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)devBtn).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private RichTextBox ReservNumInput;
        private Panel ReservNumInputArea;
        private PictureBox backSpaceBtn;
        private PictureBox pictureBox1;
        private PictureBox devBtn;
    }
}