using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class InvoicePrintForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoicePrintForm));
            TitlePanel = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PressKeyPictureBox();
            countdownLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // TitlePanel
            // 
            TitlePanel.BackgroundImage = (Image)resources.GetObject("TitlePanel.BackgroundImage");
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(1024, 170);
            TitlePanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(162, 200);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 602);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(84, 906);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(857, 179);
            pictureBox2.TabIndex = 2;
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
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            pictureBox3.Click += Go_Home;
            // 
            // countdownLabel
            // 
            countdownLabel.BackColor = Color.Transparent;
            countdownLabel.Font = new Font("Pretendard", 30F, FontStyle.Regular, GraphicsUnit.Point, 129);
            countdownLabel.ForeColor = Color.FromArgb(0, 60, 180);
            countdownLabel.Image = (Image)resources.GetObject("countdownLabel.Image");
            countdownLabel.Location = new Point(475, 818);
            countdownLabel.Name = "countdownLabel";
            countdownLabel.Size = new Size(74, 74);
            countdownLabel.TabIndex = 17;
            countdownLabel.Text = "30";
            countdownLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // InvoicePrintForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(countdownLabel);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(TitlePanel);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Name = "InvoicePrintForm";
            ShowInTaskbar = false;
            Text = "InvoiceForm";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel TitlePanel;
        private Label label2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PressKeyPictureBox pictureBox3;
        private Label countdownLabel;
    }
}