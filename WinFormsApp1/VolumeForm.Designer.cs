using AxWMPLib;
using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class VolumeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VolumeForm));
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            panel1 = new Panel();
            axWindowsMediaPlayer1 = new AxWindowsMediaPlayer();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PressKeyPictureBox();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // BackBtn
            // 
            BackBtn.Image = (Image)resources.GetObject("BackBtn.Image");
            BackBtn.Location = new Point(849, 0);
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
            HomeBtn.BackColor = Color.Transparent;
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
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(BackBtn);
            panel1.Controls.Add(HomeBtn);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 180);
            panel1.TabIndex = 5;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(0, 180);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(1024, 629);
            axWindowsMediaPlayer1.TabIndex = 6;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(84, 903);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(857, 103);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(34, 1107);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.NormalImage = (Image)resources.GetObject("pictureBox3.NormalImage");
            pictureBox3.PressedImage = (Image)resources.GetObject("pictureBox3.PressedImage");
            pictureBox3.Size = new Size(956, 139);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            pictureBox3.Click += Go_Next;
            // 
            // VolumeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(axWindowsMediaPlayer1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VolumeForm";
            ShowInTaskbar = false;
            Text = "WeightsForm";
            WindowState = FormWindowState.Maximized;
            Load += VolumeForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private Panel panel1;
        //private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PressKeyPictureBox pictureBox3;
        private AxWindowsMediaPlayer axWindowsMediaPlayer1; // 추가
    }
}