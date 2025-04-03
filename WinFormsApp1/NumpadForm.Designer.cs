namespace WinFormsApp1
{
    partial class NumpadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumpadForm));
            panel1 = new Panel();
            Enter = new Controls.PressKeyPictureBox();
            BackSpace = new Controls.PressKeyPictureBox();
            num1 = new Controls.PressKeyPictureBox();
            num9 = new Controls.PressKeyPictureBox();
            num0 = new Controls.PressKeyPictureBox();
            num8 = new Controls.PressKeyPictureBox();
            num7 = new Controls.PressKeyPictureBox();
            num6 = new Controls.PressKeyPictureBox();
            num5 = new Controls.PressKeyPictureBox();
            num4 = new Controls.PressKeyPictureBox();
            num3 = new Controls.PressKeyPictureBox();
            num2 = new Controls.PressKeyPictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Enter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BackSpace).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(Enter);
            panel1.Controls.Add(BackSpace);
            panel1.Controls.Add(num1);
            panel1.Controls.Add(num9);
            panel1.Controls.Add(num0);
            panel1.Controls.Add(num8);
            panel1.Controls.Add(num7);
            panel1.Controls.Add(num6);
            panel1.Controls.Add(num5);
            panel1.Controls.Add(num4);
            panel1.Controls.Add(num3);
            panel1.Controls.Add(num2);
            panel1.Location = new Point(0, 764);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 516);
            panel1.TabIndex = 2;
            // 
            // Enter
            // 
            Enter.BackColor = Color.Transparent;
            Enter.Image = (Image)resources.GetObject("Enter.Image");
            Enter.Location = new Point(629, 380);
            Enter.Name = "Enter";
            Enter.NormalImage = (Image)resources.GetObject("Enter.NormalImage");
            Enter.PressedImage = (Image)resources.GetObject("Enter.PressedImage");
            Enter.Size = new Size(210, 102);
            Enter.SizeMode = PictureBoxSizeMode.StretchImage;
            Enter.TabIndex = 9;
            Enter.TabStop = false;
            Enter.Tag = "Enter";
            Enter.Click += EnterNumber;
            // 
            // BackSpace
            // 
            BackSpace.BackColor = Color.Transparent;
            BackSpace.Image = (Image)resources.GetObject("BackSpace.Image");
            BackSpace.Location = new Point(186, 380);
            BackSpace.Name = "BackSpace";
            BackSpace.NormalImage = (Image)resources.GetObject("BackSpace.NormalImage");
            BackSpace.PressedImage = (Image)resources.GetObject("BackSpace.PressedImage");
            BackSpace.Size = new Size(210, 102);
            BackSpace.SizeMode = PictureBoxSizeMode.StretchImage;
            BackSpace.TabIndex = 10;
            BackSpace.TabStop = false;
            BackSpace.Tag = "Backspace";
            BackSpace.Click += EnterNumber;
            // 
            // num1
            // 
            num1.BackColor = Color.Transparent;
            num1.Image = (Image)resources.GetObject("num1.Image");
            num1.Location = new Point(186, 35);
            num1.Name = "num1";
            num1.NormalImage = (Image)resources.GetObject("num1.NormalImage");
            num1.PressedImage = (Image)resources.GetObject("num1.PressedImage");
            num1.Size = new Size(210, 102);
            num1.SizeMode = PictureBoxSizeMode.StretchImage;
            num1.TabIndex = 0;
            num1.TabStop = false;
            num1.Tag = "1";
            num1.Click += EnterNumber;
            // 
            // num9
            // 
            num9.BackColor = Color.Transparent;
            num9.Image = (Image)resources.GetObject("num9.Image");
            num9.Location = new Point(629, 265);
            num9.Name = "num9";
            num9.NormalImage = (Image)resources.GetObject("num9.NormalImage");
            num9.PressedImage = (Image)resources.GetObject("num9.PressedImage");
            num9.Size = new Size(210, 102);
            num9.SizeMode = PictureBoxSizeMode.StretchImage;
            num9.TabIndex = 8;
            num9.TabStop = false;
            num9.Tag = "9";
            num9.Click += EnterNumber;
            // 
            // num0
            // 
            num0.BackColor = Color.Transparent;
            num0.Image = (Image)resources.GetObject("num0.Image");
            num0.Location = new Point(408, 380);
            num0.Name = "num0";
            num0.NormalImage = (Image)resources.GetObject("num0.NormalImage");
            num0.PressedImage = (Image)resources.GetObject("num0.PressedImage");
            num0.Size = new Size(210, 102);
            num0.SizeMode = PictureBoxSizeMode.StretchImage;
            num0.TabIndex = 9;
            num0.TabStop = false;
            num0.Tag = "0";
            num0.Click += EnterNumber;
            // 
            // num8
            // 
            num8.BackColor = Color.Transparent;
            num8.Image = (Image)resources.GetObject("num8.Image");
            num8.Location = new Point(408, 265);
            num8.Name = "num8";
            num8.NormalImage = (Image)resources.GetObject("num8.NormalImage");
            num8.PressedImage = (Image)resources.GetObject("num8.PressedImage");
            num8.Size = new Size(210, 102);
            num8.SizeMode = PictureBoxSizeMode.StretchImage;
            num8.TabIndex = 7;
            num8.TabStop = false;
            num8.Tag = "8";
            num8.Click += EnterNumber;
            // 
            // num7
            // 
            num7.BackColor = Color.Transparent;
            num7.Image = (Image)resources.GetObject("num7.Image");
            num7.Location = new Point(186, 265);
            num7.Name = "num7";
            num7.NormalImage = (Image)resources.GetObject("num7.NormalImage");
            num7.PressedImage = (Image)resources.GetObject("num7.PressedImage");
            num7.Size = new Size(210, 102);
            num7.SizeMode = PictureBoxSizeMode.StretchImage;
            num7.TabIndex = 6;
            num7.TabStop = false;
            num7.Tag = "7";
            num7.Click += EnterNumber;
            // 
            // num6
            // 
            num6.BackColor = Color.Transparent;
            num6.Image = (Image)resources.GetObject("num6.Image");
            num6.Location = new Point(629, 150);
            num6.Name = "num6";
            num6.NormalImage = (Image)resources.GetObject("num6.NormalImage");
            num6.PressedImage = (Image)resources.GetObject("num6.PressedImage");
            num6.Size = new Size(210, 102);
            num6.SizeMode = PictureBoxSizeMode.StretchImage;
            num6.TabIndex = 5;
            num6.TabStop = false;
            num6.Tag = "6";
            num6.Click += EnterNumber;
            // 
            // num5
            // 
            num5.BackColor = Color.Transparent;
            num5.Image = (Image)resources.GetObject("num5.Image");
            num5.Location = new Point(408, 150);
            num5.Name = "num5";
            num5.NormalImage = (Image)resources.GetObject("num5.NormalImage");
            num5.PressedImage = (Image)resources.GetObject("num5.PressedImage");
            num5.Size = new Size(210, 102);
            num5.SizeMode = PictureBoxSizeMode.StretchImage;
            num5.TabIndex = 4;
            num5.TabStop = false;
            num5.Tag = "5";
            num5.Click += EnterNumber;
            // 
            // num4
            // 
            num4.BackColor = Color.Transparent;
            num4.Image = (Image)resources.GetObject("num4.Image");
            num4.Location = new Point(186, 150);
            num4.Name = "num4";
            num4.NormalImage = (Image)resources.GetObject("num4.NormalImage");
            num4.PressedImage = (Image)resources.GetObject("num4.PressedImage");
            num4.Size = new Size(210, 102);
            num4.SizeMode = PictureBoxSizeMode.StretchImage;
            num4.TabIndex = 3;
            num4.TabStop = false;
            num4.Tag = "4";
            num4.Click += EnterNumber;
            // 
            // num3
            // 
            num3.BackColor = Color.Transparent;
            num3.Image = (Image)resources.GetObject("num3.Image");
            num3.Location = new Point(629, 35);
            num3.Name = "num3";
            num3.NormalImage = (Image)resources.GetObject("num3.NormalImage");
            num3.PressedImage = (Image)resources.GetObject("num3.PressedImage");
            num3.Size = new Size(210, 102);
            num3.SizeMode = PictureBoxSizeMode.StretchImage;
            num3.TabIndex = 2;
            num3.TabStop = false;
            num3.Tag = "3";
            num3.Click += EnterNumber;
            // 
            // num2
            // 
            num2.BackColor = Color.Transparent;
            num2.Image = (Image)resources.GetObject("num2.Image");
            num2.Location = new Point(408, 35);
            num2.Name = "num2";
            num2.NormalImage = (Image)resources.GetObject("num2.NormalImage");
            num2.PressedImage = (Image)resources.GetObject("num2.PressedImage");
            num2.Size = new Size(210, 102);
            num2.SizeMode = PictureBoxSizeMode.StretchImage;
            num2.TabIndex = 1;
            num2.TabStop = false;
            num2.Tag = "2";
            num2.Click += EnterNumber;
            // 
            // NumpadForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(128, 255, 128);
            ClientSize = new Size(1024, 1280);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "NumpadForm";
            ShowInTaskbar = false;
            Text = "NumpadForm";
            TransparencyKey = Color.FromArgb(128, 255, 128);
            WindowState = FormWindowState.Maximized;
            Load += NumpadForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Enter).EndInit();
            ((System.ComponentModel.ISupportInitialize)BackSpace).EndInit();
            ((System.ComponentModel.ISupportInitialize)num1).EndInit();
            ((System.ComponentModel.ISupportInitialize)num9).EndInit();
            ((System.ComponentModel.ISupportInitialize)num0).EndInit();
            ((System.ComponentModel.ISupportInitialize)num8).EndInit();
            ((System.ComponentModel.ISupportInitialize)num7).EndInit();
            ((System.ComponentModel.ISupportInitialize)num6).EndInit();
            ((System.ComponentModel.ISupportInitialize)num5).EndInit();
            ((System.ComponentModel.ISupportInitialize)num4).EndInit();
            ((System.ComponentModel.ISupportInitialize)num3).EndInit();
            ((System.ComponentModel.ISupportInitialize)num2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TextBox InputField;
        private Label numEqual;
        private Label Colon;
        private Label appost;
        private FlowLayoutPanel keyLAyout5;
        private Panel panel1;
        private Controls.PressKeyPictureBox num1;
        private Controls.PressKeyPictureBox num2;
        private Controls.PressKeyPictureBox num3;
        private Controls.PressKeyPictureBox num4;
        private Controls.PressKeyPictureBox num5;
        private Controls.PressKeyPictureBox num6;
        private Controls.PressKeyPictureBox num7;
        private Controls.PressKeyPictureBox num8;
        private Controls.PressKeyPictureBox num9;
        private Controls.PressKeyPictureBox Enter;
        private Controls.PressKeyPictureBox num0;
        private Controls.PressKeyPictureBox BackSpace;
    }
}