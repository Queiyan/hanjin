namespace WinFormsApp1
{
    partial class MiniMsgWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniMsgWindow));
            MsgLayout = new Panel();
            msgText = new Label();
            CloseBtn = new PictureBox();
            background = new Panel();
            MsgLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CloseBtn).BeginInit();
            background.SuspendLayout();
            SuspendLayout();
            // 
            // MsgLayout
            // 
            MsgLayout.BackColor = Color.White;
            MsgLayout.BackgroundImage = (Image)resources.GetObject("MsgLayout.BackgroundImage");
            MsgLayout.BackgroundImageLayout = ImageLayout.None;
            MsgLayout.Controls.Add(msgText);
            MsgLayout.Controls.Add(CloseBtn);
            MsgLayout.Font = new Font("Pretendard", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            MsgLayout.ImeMode = ImeMode.NoControl;
            MsgLayout.Location = new Point(358, 305);
            MsgLayout.Name = "MsgLayout";
            MsgLayout.Size = new Size(309, 261);
            MsgLayout.TabIndex = 0;
            // 
            // msgText
            // 
            msgText.AutoSize = true;
            msgText.BackColor = Color.Transparent;
            msgText.Font = new Font("굴림", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            msgText.Location = new Point(347, 456);
            msgText.Name = "msgText";
            msgText.Size = new Size(0, 37);
            msgText.TabIndex = 1;
            msgText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CloseBtn
            // 
            CloseBtn.BackColor = Color.Transparent;
            CloseBtn.Image = (Image)resources.GetObject("CloseBtn.Image");
            CloseBtn.Location = new Point(9, 219);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(291, 32);
            CloseBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            CloseBtn.TabIndex = 0;
            CloseBtn.TabStop = false;
            CloseBtn.Click += CloseMsgWindow;
            // 
            // background
            // 
            background.BackColor = Color.Transparent;
            background.Controls.Add(MsgLayout);
            background.Location = new Point(0, 0);
            background.Name = "background";
            background.Size = new Size(1024, 1280);
            background.TabIndex = 1;
            // 
            // MiniMsgWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(128, 255, 128);
            ClientSize = new Size(1024, 1280);
            Controls.Add(background);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MiniMsgWindow";
            ShowInTaskbar = false;
            Text = "MsgWindow";
            TransparencyKey = Color.FromArgb(128, 255, 128);
            WindowState = FormWindowState.Maximized;
            MsgLayout.ResumeLayout(false);
            MsgLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CloseBtn).EndInit();
            background.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel MsgLayout;
        private PictureBox CloseBtn;
        private Label msgText;
        private Panel background;
    }
}