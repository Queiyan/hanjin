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
            panel1 = new Panel();
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
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
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(92, 200);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(841, 386);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.Location = new Point(92, 622);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(841, 386);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.Location = new Point(144, 1116);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(733, 139);
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // ReservationInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReservationInfo";
            ShowInTaskbar = false;
            Text = "EasyMenu";
            WindowState = FormWindowState.Maximized;
            Load += EasyMenu_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}