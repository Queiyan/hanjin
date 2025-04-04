using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class ReservationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReservationInfo));
            menuAria = new Panel();
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            senderAria = new Panel();
            senderHP = new Label();
            senderAddress = new Label();
            senderName = new Label();
            receiverAria = new Panel();
            receiverHP = new Label();
            receiverAddress = new Label();
            receiverName = new Label();
            nextBtn = new PictureBox();
            menuAria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            senderAria.SuspendLayout();
            receiverAria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nextBtn).BeginInit();
            SuspendLayout();
            // 
            // menuAria
            // 
            menuAria.BackColor = Color.Transparent;
            menuAria.BackgroundImage = (Image)resources.GetObject("menuAria.BackgroundImage");
            menuAria.Controls.Add(BackBtn);
            menuAria.Controls.Add(HomeBtn);
            menuAria.Location = new Point(0, 0);
            menuAria.Name = "menuAria";
            menuAria.Size = new Size(1024, 180);
            menuAria.TabIndex = 0;
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
            // senderAria
            // 
            senderAria.BackgroundImage = (Image)resources.GetObject("senderAria.BackgroundImage");
            senderAria.Controls.Add(senderHP);
            senderAria.Controls.Add(senderAddress);
            senderAria.Controls.Add(senderName);
            senderAria.Location = new Point(92, 234);
            senderAria.Name = "senderAria";
            senderAria.Size = new Size(841, 386);
            senderAria.TabIndex = 1;
            // 
            // senderHP
            // 
            senderHP.Font = new Font("Pretendard", 30F, FontStyle.Regular, GraphicsUnit.Point, 129);
            senderHP.Location = new Point(244, 312);
            senderHP.Name = "senderHP";
            senderHP.Size = new Size(318, 46);
            senderHP.TabIndex = 2;
            // 
            // senderAddress
            // 
            senderAddress.Font = new Font("Pretendard", 20F, FontStyle.Regular, GraphicsUnit.Point, 129);
            senderAddress.Location = new Point(244, 193);
            senderAddress.Name = "senderAddress";
            senderAddress.Size = new Size(563, 87);
            senderAddress.TabIndex = 1;
            // 
            // senderName
            // 
            senderName.Font = new Font("Pretendard", 28F, FontStyle.Regular, GraphicsUnit.Point, 129);
            senderName.Location = new Point(244, 83);
            senderName.Name = "senderName";
            senderName.Size = new Size(200, 50);
            senderName.TabIndex = 0;
            // 
            // receiverAria
            // 
            receiverAria.BackgroundImage = (Image)resources.GetObject("receiverAria.BackgroundImage");
            receiverAria.Controls.Add(receiverHP);
            receiverAria.Controls.Add(receiverAddress);
            receiverAria.Controls.Add(receiverName);
            receiverAria.Location = new Point(92, 656);
            receiverAria.Name = "receiverAria";
            receiverAria.Size = new Size(841, 386);
            receiverAria.TabIndex = 2;
            // 
            // receiverHP
            // 
            receiverHP.Font = new Font("Pretendard", 30F, FontStyle.Regular, GraphicsUnit.Point, 129);
            receiverHP.Location = new Point(244, 311);
            receiverHP.Name = "receiverHP";
            receiverHP.Size = new Size(318, 46);
            receiverHP.TabIndex = 5;
            // 
            // receiverAddress
            // 
            receiverAddress.Font = new Font("Pretendard", 20F, FontStyle.Regular, GraphicsUnit.Point, 129);
            receiverAddress.Location = new Point(244, 192);
            receiverAddress.Name = "receiverAddress";
            receiverAddress.Size = new Size(563, 87);
            receiverAddress.TabIndex = 4;
            // 
            // receiverName
            // 
            receiverName.Font = new Font("Pretendard", 28F, FontStyle.Regular, GraphicsUnit.Point, 129);
            receiverName.Location = new Point(244, 83);
            receiverName.Name = "receiverName";
            receiverName.Size = new Size(200, 50);
            receiverName.TabIndex = 3;
            // 
            // nextBtn
            // 
            nextBtn.BackgroundImage = (Image)resources.GetObject("nextBtn.BackgroundImage");
            nextBtn.Location = new Point(34, 1116);
            nextBtn.Name = "nextBtn";
            nextBtn.Size = new Size(956, 139);
            nextBtn.TabIndex = 3;
            nextBtn.TabStop = false;
            nextBtn.Click += Go_Next;
            // 
            // ReservationInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(nextBtn);
            Controls.Add(receiverAria);
            Controls.Add(senderAria);
            Controls.Add(menuAria);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReservationInfo";
            ShowInTaskbar = false;
            Text = "EasyMenu";
            WindowState = FormWindowState.Maximized;
            menuAria.ResumeLayout(false);
            menuAria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            senderAria.ResumeLayout(false);
            receiverAria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nextBtn).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel menuAria;
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private Panel senderAria;
        private Panel receiverAria;
        private PictureBox nextBtn;
        private Label senderName;
        private Label senderHP;
        private Label senderAddress;
        private Label receiverHP;
        private Label receiverAddress;
        private Label receiverName;
    }
}