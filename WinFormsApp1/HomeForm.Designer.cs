using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class HomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            kioskMenu = new PressKeyPictureBox();
            MobileBtn = new PressKeyPictureBox();
            EasyBtn = new PressKeyPictureBox();
            ((System.ComponentModel.ISupportInitialize)kioskMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MobileBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EasyBtn).BeginInit();
            SuspendLayout();
            // 
            // kioskMenu
            // 
            kioskMenu.Image = (Image)resources.GetObject("kioskMenu.Image");
            kioskMenu.Location = new Point(40, 461);
            kioskMenu.Name = "kioskMenu";
            kioskMenu.NormalImage = (Image)resources.GetObject("kioskMenu.NormalImage");
            kioskMenu.PressedImage = (Image)resources.GetObject("kioskMenu.PressedImage");
            kioskMenu.Size = new Size(952, 214);
            kioskMenu.SizeMode = PictureBoxSizeMode.AutoSize;
            kioskMenu.TabIndex = 2;
            kioskMenu.TabStop = false;
            kioskMenu.Click += Go_Kiosk;
            // 
            // MobileBtn
            // 
            MobileBtn.Image = (Image)resources.GetObject("MobileBtn.Image");
            MobileBtn.Location = new Point(40, 716);
            MobileBtn.Name = "MobileBtn";
            MobileBtn.NormalImage = (Image)resources.GetObject("MobileBtn.NormalImage");
            MobileBtn.PressedImage = (Image)resources.GetObject("MobileBtn.PressedImage");
            MobileBtn.Size = new Size(952, 214);
            MobileBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            MobileBtn.TabIndex = 1;
            MobileBtn.TabStop = false;
            MobileBtn.Click += Go_Easy;
            // 
            // EasyBtn
            // 
            EasyBtn.Image = (Image)resources.GetObject("EasyBtn.Image");
            EasyBtn.Location = new Point(40, 971);
            EasyBtn.Name = "EasyBtn";
            EasyBtn.NormalImage = (Image)resources.GetObject("EasyBtn.NormalImage");
            EasyBtn.PressedImage = (Image)resources.GetObject("EasyBtn.PressedImage");
            EasyBtn.Size = new Size(952, 214);
            EasyBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            EasyBtn.TabIndex = 0;
            EasyBtn.TabStop = false;
            EasyBtn.Click += Go_Mobile;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1024, 1280);
            Controls.Add(EasyBtn);
            Controls.Add(MobileBtn);
            Controls.Add(kioskMenu);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "HomeForm";
            ShowInTaskbar = false;
            Text = "HomeForm";
            WindowState = FormWindowState.Maximized;
            Load += HomeForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)kioskMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)MobileBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)EasyBtn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PressKeyPictureBox kioskMenu;
        private PressKeyPictureBox MobileBtn;
        private PressKeyPictureBox EasyBtn;
    }
}