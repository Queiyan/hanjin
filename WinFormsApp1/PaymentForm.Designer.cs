using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class PaymentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentForm));
            panel1 = new Panel();
            devBtn = new PictureBox();
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            panel2 = new Panel();
            pictureBox4 = new PressKeyPictureBox();
            NextBtn = new PressKeyPictureBox();
            panel3 = new Panel();
            CostValue = new TextBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)devBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(devBtn);
            panel1.Controls.Add(BackBtn);
            panel1.Controls.Add(HomeBtn);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 180);
            panel1.TabIndex = 1;
            // 
            // devBtn
            // 
            devBtn.Location = new Point(372, 46);
            devBtn.Name = "devBtn";
            devBtn.Size = new Size(263, 91);
            devBtn.TabIndex = 3;
            devBtn.TabStop = false;
            devBtn.Click += devBtn_Click;
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
            BackBtn.TabIndex = 2;
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
            HomeBtn.TabIndex = 1;
            HomeBtn.TabStop = false;
            HomeBtn.Click += Go_Home;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox4);
            panel2.Controls.Add(NextBtn);
            panel2.Location = new Point(0, 1080);
            panel2.Name = "panel2";
            panel2.Size = new Size(1024, 200);
            panel2.TabIndex = 13;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(791, 38);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.NormalImage = (Image)resources.GetObject("pictureBox4.NormalImage");
            pictureBox4.PressedImage = (Image)resources.GetObject("pictureBox4.PressedImage");
            pictureBox4.Size = new Size(199, 139);
            pictureBox4.TabIndex = 16;
            pictureBox4.TabStop = false;
            pictureBox4.Click += Go_Back;
            // 
            // NextBtn
            // 
            NextBtn.Image = (Image)resources.GetObject("NextBtn.Image");
            NextBtn.Location = new Point(34, 38);
            NextBtn.Name = "NextBtn";
            NextBtn.NormalImage = (Image)resources.GetObject("NextBtn.NormalImage");
            NextBtn.PressedImage = (Image)resources.GetObject("NextBtn.PressedImage");
            NextBtn.Size = new Size(733, 139);
            NextBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            NextBtn.TabIndex = 15;
            NextBtn.TabStop = false;
            NextBtn.Click += Go_Next;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(CostValue);
            panel3.Location = new Point(177, 832);
            panel3.Name = "panel3";
            panel3.Size = new Size(710, 94);
            panel3.TabIndex = 14;
            // 
            // CostValue
            // 
            CostValue.BorderStyle = BorderStyle.None;
            CostValue.Font = new Font("굴림", 48F, FontStyle.Regular, GraphicsUnit.Point, 129);
            CostValue.Location = new Point(306, 14);
            CostValue.Name = "CostValue";
            CostValue.ReadOnly = true;
            CostValue.Size = new Size(326, 74);
            CostValue.TabIndex = 0;
            CostValue.TabStop = false;
            CostValue.TextAlign = HorizontalAlignment.Right;
            CostValue.TextChanged += CostValue_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(5, 180);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1014, 629);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 15;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(84, 938);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(857, 103);
            pictureBox3.TabIndex = 16;
            pictureBox3.TabStop = false;
            // 
            // PaymentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1024, 1280);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PaymentForm";
            ShowInTaskbar = false;
            Text = "PaymentForm";
            WindowState = FormWindowState.Maximized;
            Load += PaymentForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)devBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private Panel panel2;
        private Panel panel3;
        private TextBox CostValue;
        private PressKeyPictureBox NextBtn;
        private PressKeyPictureBox pictureBox4;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox devBtn;
    }
}