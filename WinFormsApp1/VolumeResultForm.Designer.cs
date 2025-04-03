using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class VolumeResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VolumeResultForm));
            panel1 = new Panel();
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            pictureBox1 = new PictureBox();
            SizeValue = new TextBox();
            NextBtn = new PictureBox();
            RetryBtn = new PictureBox();
            panel3 = new Panel();
            heightValue = new TextBox();
            depthValue = new TextBox();
            widthValue = new TextBox();
            ControlArea = new Panel();
            pictureBox3 = new PressKeyPictureBox();
            pictureBox2 = new PressKeyPictureBox();
            SumArea = new Panel();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RetryBtn).BeginInit();
            panel3.SuspendLayout();
            ControlArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SumArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
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
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(5, 180);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1014, 629);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // SizeValue
            // 
            SizeValue.BackColor = Color.White;
            SizeValue.BorderStyle = BorderStyle.None;
            SizeValue.Font = new Font("Pretendard", 55F, FontStyle.Bold, GraphicsUnit.Point, 129);
            SizeValue.Location = new Point(181, 3);
            SizeValue.Name = "SizeValue";
            SizeValue.ReadOnly = true;
            SizeValue.ShortcutsEnabled = false;
            SizeValue.Size = new Size(195, 88);
            SizeValue.TabIndex = 3;
            SizeValue.TabStop = false;
            SizeValue.TextAlign = HorizontalAlignment.Right;
            SizeValue.WordWrap = false;
            SizeValue.TextChanged += SizeValue_TextChanged;
            // 
            // NextBtn
            // 
            NextBtn.Image = (Image)resources.GetObject("NextBtn.Image");
            NextBtn.Location = new Point(115, 1663);
            NextBtn.Name = "NextBtn";
            NextBtn.Size = new Size(369, 127);
            NextBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            NextBtn.TabIndex = 4;
            NextBtn.TabStop = false;
            NextBtn.Click += Go_Next;
            // 
            // RetryBtn
            // 
            RetryBtn.Image = (Image)resources.GetObject("RetryBtn.Image");
            RetryBtn.Location = new Point(580, 1663);
            RetryBtn.Name = "RetryBtn";
            RetryBtn.Size = new Size(369, 127);
            RetryBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            RetryBtn.TabIndex = 5;
            RetryBtn.TabStop = false;
            RetryBtn.Click += ReMeasure;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(heightValue);
            panel3.Controls.Add(depthValue);
            panel3.Controls.Add(widthValue);
            panel3.Location = new Point(34, 970);
            panel3.Name = "panel3";
            panel3.Size = new Size(956, 103);
            panel3.TabIndex = 9;
            // 
            // heightValue
            // 
            heightValue.BackColor = Color.White;
            heightValue.BorderStyle = BorderStyle.None;
            heightValue.Font = new Font("Pretendard", 40F, FontStyle.Bold);
            heightValue.Location = new Point(744, 21);
            heightValue.Name = "heightValue";
            heightValue.ReadOnly = true;
            heightValue.ShortcutsEnabled = false;
            heightValue.Size = new Size(120, 64);
            heightValue.TabIndex = 10;
            heightValue.TabStop = false;
            heightValue.TextAlign = HorizontalAlignment.Right;
            heightValue.TextChanged += heightValue_TextChanged;
            // 
            // depthValue
            // 
            depthValue.BackColor = Color.White;
            depthValue.BorderStyle = BorderStyle.None;
            depthValue.Font = new Font("Pretendard", 40F, FontStyle.Bold);
            depthValue.Location = new Point(491, 21);
            depthValue.Name = "depthValue";
            depthValue.ReadOnly = true;
            depthValue.ShortcutsEnabled = false;
            depthValue.Size = new Size(120, 64);
            depthValue.TabIndex = 9;
            depthValue.TabStop = false;
            depthValue.TextAlign = HorizontalAlignment.Right;
            depthValue.TextChanged += depthValue_TextChanged;
            // 
            // widthValue
            // 
            widthValue.BackColor = Color.White;
            widthValue.BorderStyle = BorderStyle.None;
            widthValue.Font = new Font("Pretendard", 40F, FontStyle.Bold);
            widthValue.Location = new Point(237, 21);
            widthValue.Name = "widthValue";
            widthValue.ReadOnly = true;
            widthValue.ShortcutsEnabled = false;
            widthValue.Size = new Size(119, 64);
            widthValue.TabIndex = 4;
            widthValue.TabStop = false;
            widthValue.TextAlign = HorizontalAlignment.Right;
            widthValue.TextChanged += widthValue_TextChanged;
            // 
            // ControlArea
            // 
            ControlArea.Controls.Add(pictureBox3);
            ControlArea.Controls.Add(pictureBox2);
            ControlArea.Location = new Point(0, 1107);
            ControlArea.Name = "ControlArea";
            ControlArea.Size = new Size(1024, 139);
            ControlArea.TabIndex = 10;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(791, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.NormalImage = (Image)resources.GetObject("pictureBox3.NormalImage");
            pictureBox3.PressedImage = (Image)resources.GetObject("pictureBox3.PressedImage");
            pictureBox3.Size = new Size(199, 139);
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            pictureBox3.Click += ReMeasure;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(34, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.NormalImage = (Image)resources.GetObject("pictureBox2.NormalImage");
            pictureBox2.PressedImage = (Image)resources.GetObject("pictureBox2.PressedImage");
            pictureBox2.Size = new Size(733, 139);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            pictureBox2.Click += Go_Next;
            // 
            // SumArea
            // 
            SumArea.Controls.Add(pictureBox5);
            SumArea.Controls.Add(pictureBox4);
            SumArea.Controls.Add(SizeValue);
            SumArea.Location = new Point(242, 840);
            SumArea.Name = "SumArea";
            SumArea.Size = new Size(525, 94);
            SumArea.TabIndex = 11;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(386, 4);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(136, 87);
            pictureBox5.TabIndex = 5;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(139, 94);
            pictureBox4.TabIndex = 4;
            pictureBox4.TabStop = false;
            // 
            // VolumeResultForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(SumArea);
            Controls.Add(ControlArea);
            Controls.Add(panel3);
            Controls.Add(RetryBtn);
            Controls.Add(NextBtn);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VolumeResultForm";
            ShowInTaskbar = false;
            Text = "WeightsResult";
            WindowState = FormWindowState.Maximized;
            Load += VolumeResultForm_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)RetryBtn).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ControlArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            SumArea.ResumeLayout(false);
            SumArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private PictureBox pictureBox1;
        private TextBox SizeValue;
        private PictureBox NextBtn;
        private PictureBox RetryBtn;
        private Panel panel3;
        private Panel ControlArea;
        private Panel SumArea;
        private PressKeyPictureBox pictureBox3;
        private PressKeyPictureBox pictureBox2;
        private TextBox heightValue;
        private TextBox depthValue;
        private TextBox widthValue;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
    }
}