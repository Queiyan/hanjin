// PostcodeSearchForm.Designer.cs
using System.Windows.Forms;
using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class PostcodeSearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private RichTextBox rtxtSearch;
        private PressKeyPictureBox btnSearch;
        private ListView lvResults;
        private ColumnHeader columnZipCode;
        private ColumnHeader columnAddress;
        private PressKeyPictureBox btnOK;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostcodeSearchForm));
            rtxtSearch = new RichTextBox();
            btnSearch = new PressKeyPictureBox();
            lvResults = new ListView();
            columnZipCode = new ColumnHeader();
            columnAddress = new ColumnHeader();
            btnOK = new PressKeyPictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox9 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)btnSearch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnOK).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            SuspendLayout();
            // 
            // rtxtSearch
            // 
            rtxtSearch.BorderStyle = BorderStyle.None;
            rtxtSearch.Font = new Font("Pretendard", 20F);
            rtxtSearch.Location = new Point(94, 117);
            rtxtSearch.Margin = new Padding(0);
            rtxtSearch.Multiline = false;
            rtxtSearch.Name = "rtxtSearch";
            rtxtSearch.ScrollBars = RichTextBoxScrollBars.None;
            rtxtSearch.Size = new Size(419, 30);
            rtxtSearch.TabIndex = 0;
            rtxtSearch.Text = "";
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
            btnSearch.TabIndex = 1;
            btnSearch.TabStop = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // lvResults
            // 
            lvResults.Columns.AddRange(new ColumnHeader[] { columnZipCode, columnAddress });
            lvResults.Font = new Font("Pretendard", 14F);
            lvResults.FullRowSelect = true;
            lvResults.Location = new Point(77, 188);
            lvResults.Margin = new Padding(4, 3, 4, 3);
            lvResults.MultiSelect = false;
            lvResults.Name = "lvResults";
            lvResults.Size = new Size(546, 291);
            lvResults.TabIndex = 2;
            lvResults.UseCompatibleStateImageBehavior = false;
            lvResults.View = View.Details;
            lvResults.SelectedIndexChanged += lvResults_SelectedIndexChanged;
            // 
            // columnZipCode
            // 
            columnZipCode.Text = "우편번호";
            columnZipCode.Width = 100;
            // 
            // columnAddress
            // 
            columnAddress.Text = "주소";
            columnAddress.Width = 400;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.Transparent;
            btnOK.Font = new Font("Pretendard", 16F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btnOK.ForeColor = Color.White;
            btnOK.Image = (Image)resources.GetObject("btnOK.Image");
            btnOK.Location = new Point(223, 488);
            btnOK.Margin = new Padding(0);
            btnOK.Name = "btnOK";
            btnOK.NormalImage = (Image)resources.GetObject("btnOK.NormalImage");
            btnOK.PressedImage = (Image)resources.GetObject("btnOK.PressedImage");
            btnOK.Size = new Size(255, 40);
            btnOK.TabIndex = 5;
            btnOK.TabStop = false;
            btnOK.Click += btnOK_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(643, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 48);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            pictureBox1.Click += CloseButton_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.Location = new Point(80, 160);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(429, 18);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.BackgroundImage = (Image)resources.GetObject("pictureBox9.BackgroundImage");
            pictureBox9.Location = new Point(77, 112);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(439, 40);
            pictureBox9.TabIndex = 14;
            pictureBox9.TabStop = false;
            // 
            // PostcodeSearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(700, 545);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnOK);
            Controls.Add(lvResults);
            Controls.Add(btnSearch);
            Controls.Add(rtxtSearch);
            Controls.Add(pictureBox9);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(162, 200);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "PostcodeSearchForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "주소 검색";
            ((System.ComponentModel.ISupportInitialize)btnSearch).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnOK).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ResumeLayout(false);
        }

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox9;
    }
}
