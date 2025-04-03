namespace WinFormsApp1
{
    partial class MobileMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MobileMenu));
            QRcode = new Panel();
            BackBtn = new PictureBox();
            HomeBtn = new PictureBox();
            TitlePanel = new Panel();
            MsgLabel = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // QRcode
            // 
            QRcode.BackColor = Color.Transparent;
            QRcode.BackgroundImage = (Image)resources.GetObject("QRcode.BackgroundImage");
            QRcode.BackgroundImageLayout = ImageLayout.None;
            QRcode.Location = new Point(377, 350);
            QRcode.Name = "QRcode";
            QRcode.Size = new Size(270, 270);
            QRcode.TabIndex = 0;
            // 
            // BackBtn
            // 
            BackBtn.Image = (Image)resources.GetObject("BackBtn.Image");
            BackBtn.Location = new Point(844, 0);
            BackBtn.Name = "BackBtn";
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
            HomeBtn.Size = new Size(180, 180);
            HomeBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            HomeBtn.TabIndex = 0;
            HomeBtn.TabStop = false;
            HomeBtn.Click += Go_Home;
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Transparent;
            TitlePanel.BackgroundImage = (Image)resources.GetObject("TitlePanel.BackgroundImage");
            TitlePanel.Controls.Add(BackBtn);
            TitlePanel.Controls.Add(HomeBtn);
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(1024, 180);
            TitlePanel.TabIndex = 1;
            // 
            // MsgLabel
            // 
            MsgLabel.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point, 129);
            MsgLabel.Image = (Image)resources.GetObject("MsgLabel.Image");
            MsgLabel.Location = new Point(115, 819);
            MsgLabel.Name = "MsgLabel";
            MsgLabel.Size = new Size(795, 244);
            MsgLabel.TabIndex = 2;
            MsgLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(201, 700);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(622, 53);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(34, 1107);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(956, 139);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += devTest;
            // 
            // MobileMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1024, 1280);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(MsgLabel);
            Controls.Add(TitlePanel);
            Controls.Add(QRcode);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MobileMenu";
            ShowInTaskbar = false;
            Text = "MobileMenu";
            WindowState = FormWindowState.Maximized;
            Load += MobileMenu_Load;
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            TitlePanel.ResumeLayout(false);
            TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel QRcode;
        private PictureBox BackBtn;
        private PictureBox HomeBtn;
        private Panel TitlePanel;
        private Label MsgLabel;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}