using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class DetailedAddressForm
    {
        private System.ComponentModel.IContainer components = null;
        private RichTextBox txtDetailedAddress;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedAddressForm));
            txtDetailedAddress = new RichTextBox();
            CancelBtn = new PictureBox();
            pictureBox2 = new PressKeyPictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox8 = new PictureBox();
            btnSearch = new PressKeyPictureBox();
            rtxtSearch = new RichTextBox();
            pictureBox9 = new PictureBox();
            AddressInputed = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)CancelBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSearch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            SuspendLayout();
            // 
            // txtDetailedAddress
            // 
            txtDetailedAddress.BorderStyle = BorderStyle.None;
            txtDetailedAddress.Font = new Font("Pretendard", 20F);
            txtDetailedAddress.Location = new Point(40, 410);
            txtDetailedAddress.Multiline = false;
            txtDetailedAddress.Name = "txtDetailedAddress";
            txtDetailedAddress.ScrollBars = RichTextBoxScrollBars.None;
            txtDetailedAddress.Size = new Size(618, 35);
            txtDetailedAddress.TabIndex = 0;
            txtDetailedAddress.Text = "";
            // 
            // CancelBtn
            // 
            CancelBtn.BackColor = Color.Transparent;
            CancelBtn.Location = new Point(649, 3);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(47, 44);
            CancelBtn.TabIndex = 3;
            CancelBtn.TabStop = false;
            CancelBtn.Click += btnCancel_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(223, 488);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.NormalImage = (Image)resources.GetObject("pictureBox2.NormalImage");
            pictureBox2.PressedImage = (Image)resources.GetObject("pictureBox2.PressedImage");
            pictureBox2.Size = new Size(255, 40);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += btnOK_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.Location = new Point(26, 405);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(648, 45);
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImage = (Image)resources.GetObject("pictureBox4.BackgroundImage");
            pictureBox4.Location = new Point(26, 258);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(648, 45);
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.BackgroundImage = (Image)resources.GetObject("pictureBox6.BackgroundImage");
            pictureBox6.Location = new Point(26, 359);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(128, 23);
            pictureBox6.TabIndex = 8;
            pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.BackgroundImage = (Image)resources.GetObject("pictureBox5.BackgroundImage");
            pictureBox5.Location = new Point(26, 212);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(108, 23);
            pictureBox5.TabIndex = 7;
            pictureBox5.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Transparent;
            pictureBox8.BackgroundImage = (Image)resources.GetObject("pictureBox8.BackgroundImage");
            pictureBox8.Location = new Point(80, 160);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(429, 18);
            pictureBox8.TabIndex = 12;
            pictureBox8.TabStop = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackgroundImageLayout = ImageLayout.None;
            btnSearch.Font = new Font("Pretendard", 15F);
            btnSearch.ForeColor = Color.White;
            btnSearch.Image = (Image)resources.GetObject("btnSearch.Image");
            btnSearch.Location = new Point(535, 111);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.NormalImage = (Image)resources.GetObject("btnSearch.NormalImage");
            btnSearch.PressedImage = (Image)resources.GetObject("btnSearch.PressedImage");
            btnSearch.Size = new Size(88, 40);
            btnSearch.TabIndex = 11;
            btnSearch.TabStop = false;
            btnSearch.Click += btnCancel_Click;
            // 
            // rtxtSearch
            // 
            rtxtSearch.BorderStyle = BorderStyle.None;
            rtxtSearch.Font = new Font("Pretendard", 20F);
            rtxtSearch.Location = new Point(94, 118);
            rtxtSearch.Margin = new Padding(0);
            rtxtSearch.Multiline = false;
            rtxtSearch.Name = "rtxtSearch";
            rtxtSearch.ScrollBars = RichTextBoxScrollBars.None;
            rtxtSearch.Size = new Size(419, 30);
            rtxtSearch.TabIndex = 10;
            rtxtSearch.Text = "";
            rtxtSearch.Click += btnCancel_Click;
            // 
            // pictureBox9
            // 
            pictureBox9.BackgroundImage = (Image)resources.GetObject("pictureBox9.BackgroundImage");
            pictureBox9.Location = new Point(77, 112);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(439, 40);
            pictureBox9.TabIndex = 13;
            pictureBox9.TabStop = false;
            // 
            // AddressInputed
            // 
            AddressInputed.BackColor = Color.FromArgb(89, 112, 158);
            AddressInputed.BackgroundImageLayout = ImageLayout.Stretch;
            AddressInputed.BorderStyle = BorderStyle.None;
            AddressInputed.Font = new Font("Pretendard", 18F);
            AddressInputed.ForeColor = Color.White;
            AddressInputed.Location = new Point(40, 261);
            AddressInputed.Multiline = false;
            AddressInputed.Name = "AddressInputed";
            AddressInputed.ReadOnly = true;
            AddressInputed.Size = new Size(618, 40);
            AddressInputed.TabIndex = 9;
            AddressInputed.TabStop = false;
            AddressInputed.Text = "";
            AddressInputed.SelectionAlignment = HorizontalAlignment.Center; 

            // 
            // DetailedAddressForm
            // 
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(700, 545);
            Controls.Add(rtxtSearch);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox8);
            Controls.Add(btnSearch);
            Controls.Add(AddressInputed);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox2);
            Controls.Add(CancelBtn);
            Controls.Add(txtDetailedAddress);
            Controls.Add(pictureBox3);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(162, 200);
            Name = "DetailedAddressForm";
            StartPosition = FormStartPosition.Manual;
            Text = "상세 주소 입력";
            ((System.ComponentModel.ISupportInitialize)CancelBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSearch).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ResumeLayout(false);
        }

        private PictureBox CancelBtn;
        private PressKeyPictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
        private PictureBox pictureBox8;
        private PressKeyPictureBox btnSearch;
        private RichTextBox rtxtSearch;
        private PictureBox pictureBox9;
        private RichTextBox AddressInputed;
    }
}
