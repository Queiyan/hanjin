using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    partial class InvoiceProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceProductForm));
            TitleLayout = new Panel();
            BackBtn = new PressKeyPictureBox();
            HomeBtn = new PressKeyPictureBox();
            panel1 = new Panel();
            ProductCategory = new ComboBox();
            productComboArea = new PictureBox();
            RequestInputField = new RichTextBox();
            NextBtn = new PictureBox();
            UnabledInformation = new RichTextBox();
            InformationBox = new RichTextBox();
            product = new Panel();
            pictureBox2 = new PictureBox();
            panel3 = new Panel();
            inputMemoArea = new PictureBox();
            ProductPriceInput = new RichTextBox();
            panel2 = new Panel();
            inputPriceArea = new PictureBox();
            pictureBox1 = new PictureBox();
            TitleLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productComboArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).BeginInit();
            product.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)inputMemoArea).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)inputPriceArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TitleLayout
            // 
            TitleLayout.BackColor = Color.Transparent;
            TitleLayout.BackgroundImage = (Image)resources.GetObject("TitleLayout.BackgroundImage");
            TitleLayout.Controls.Add(BackBtn);
            TitleLayout.Location = new Point(0, 0);
            TitleLayout.Name = "TitleLayout";
            TitleLayout.Size = new Size(1024, 180);
            TitleLayout.TabIndex = 0;
            // 
            // BackBtn
            // 
            BackBtn.BackColor = Color.Transparent;
            BackBtn.Image = (Image)resources.GetObject("BackBtn.Image");
            BackBtn.Location = new Point(845, 0);
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
            HomeBtn.BackColor = Color.Transparent;
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
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(ProductCategory);
            panel1.Controls.Add(productComboArea);
            panel1.Location = new Point(242, 343);
            panel1.Name = "panel1";
            panel1.Size = new Size(665, 40);
            panel1.TabIndex = 2;
            // 
            // ProductCategory
            // 
            ProductCategory.BackColor = Color.White;
            ProductCategory.DropDownHeight = 600;
            ProductCategory.FlatStyle = FlatStyle.Flat;
            ProductCategory.Font = new Font("Pretendard", 20.25F);
            ProductCategory.IntegralHeight = false;
            ProductCategory.ItemHeight = 32;
            ProductCategory.Location = new Point(338, 0);
            ProductCategory.Name = "ProductCategory";
            ProductCategory.Size = new Size(327, 40);
            ProductCategory.TabIndex = 0;
            ProductCategory.TabStop = false;
            // 
            // productComboArea
            // 
            productComboArea.Location = new Point(-132, -17);
            productComboArea.Name = "productComboArea";
            productComboArea.Size = new Size(797, 66);
            productComboArea.TabIndex = 1;
            productComboArea.TabStop = false;
            productComboArea.Click += pictureBox_Focus;
            // 
            // RequestInputField
            // 
            RequestInputField.BackColor = Color.White;
            RequestInputField.BorderStyle = BorderStyle.None;
            RequestInputField.Font = new Font("Pretendard", 20.25F);
            RequestInputField.Location = new Point(24, 0);
            RequestInputField.Name = "RequestInputField";
            RequestInputField.ScrollBars = RichTextBoxScrollBars.None;
            RequestInputField.Size = new Size(517, 40);
            RequestInputField.TabIndex = 2;
            RequestInputField.Text = "";
            // 
            // NextBtn
            // 
            NextBtn.Image = (Image)resources.GetObject("NextBtn.Image");
            NextBtn.Location = new Point(0, 1770);
            NextBtn.Name = "NextBtn";
            NextBtn.Size = new Size(1080, 170);
            NextBtn.SizeMode = PictureBoxSizeMode.AutoSize;
            NextBtn.TabIndex = 11;
            NextBtn.TabStop = false;
            NextBtn.Click += Go_Next;
            // 
            // UnabledInformation
            // 
            UnabledInformation.BackColor = Color.White;
            UnabledInformation.BorderStyle = BorderStyle.None;
            UnabledInformation.Font = new Font("굴림", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            UnabledInformation.Location = new Point(113, 873);
            UnabledInformation.Name = "UnabledInformation";
            UnabledInformation.ReadOnly = true;
            UnabledInformation.ScrollBars = RichTextBoxScrollBars.None;
            UnabledInformation.Size = new Size(869, 0);
            UnabledInformation.TabIndex = 12;
            UnabledInformation.Text = resources.GetString("UnabledInformation.Text");
            // 
            // InformationBox
            // 
            InformationBox.BackColor = Color.White;
            InformationBox.BorderStyle = BorderStyle.None;
            InformationBox.Font = new Font("굴림", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            InformationBox.Location = new Point(113, 873);
            InformationBox.Name = "InformationBox";
            InformationBox.ReadOnly = true;
            InformationBox.ScrollBars = RichTextBoxScrollBars.None;
            InformationBox.Size = new Size(869, 0);
            InformationBox.TabIndex = 13;
            InformationBox.Text = resources.GetString("InformationBox.Text");
            // 
            // product
            // 
            product.BackgroundImage = (Image)resources.GetObject("product.BackgroundImage");
            product.Controls.Add(pictureBox2);
            product.Location = new Point(110, 235);
            product.Name = "product";
            product.Size = new Size(797, 384);
            product.TabIndex = 1;
            product.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(0, 316);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(19, 21);
            pictureBox2.TabIndex = 15;
            pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackgroundImageLayout = ImageLayout.Center;
            panel3.Controls.Add(RequestInputField);
            panel3.Controls.Add(inputMemoArea);
            panel3.Location = new Point(242, 569);
            panel3.Name = "panel3";
            panel3.Size = new Size(606, 40);
            panel3.TabIndex = 4;
            // 
            // inputMemoArea
            // 
            inputMemoArea.BackColor = Color.Transparent;
            inputMemoArea.Location = new Point(-129, -9);
            inputMemoArea.Name = "inputMemoArea";
            inputMemoArea.Size = new Size(794, 56);
            inputMemoArea.TabIndex = 9;
            inputMemoArea.TabStop = false;
            inputMemoArea.Click += pictureBox_Focus;
            // 
            // ProductPriceInput
            // 
            ProductPriceInput.BackColor = Color.White;
            ProductPriceInput.BorderStyle = BorderStyle.None;
            ProductPriceInput.Font = new Font("Pretendard", 20.25F);
            ProductPriceInput.Location = new Point(338, 16);
            ProductPriceInput.MaxLength = 10;
            ProductPriceInput.Multiline = false;
            ProductPriceInput.Name = "ProductPriceInput";
            ProductPriceInput.RightToLeft = RightToLeft.Yes;
            ProductPriceInput.ScrollBars = RichTextBoxScrollBars.None;
            ProductPriceInput.ShortcutsEnabled = false;
            ProductPriceInput.Size = new Size(300, 36);
            ProductPriceInput.TabIndex = 1;
            ProductPriceInput.Text = "";
            ProductPriceInput.KeyUp += GoodsNameEntered;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImageLayout = ImageLayout.None;
            panel2.Controls.Add(ProductPriceInput);
            panel2.Controls.Add(inputPriceArea);
            panel2.Location = new Point(242, 445);
            panel2.Name = "panel2";
            panel2.Size = new Size(638, 58);
            panel2.TabIndex = 3;
            // 
            // inputPriceArea
            // 
            inputPriceArea.BackColor = Color.Transparent;
            inputPriceArea.Location = new Point(-129, -2);
            inputPriceArea.Name = "inputPriceArea";
            inputPriceArea.Size = new Size(791, 60);
            inputPriceArea.TabIndex = 0;
            inputPriceArea.TabStop = false;
            inputPriceArea.Click += pictureBox_Focus;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(920, 200);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 75);
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            pictureBox1.Click += sampleinput;
            // 
            // InvoiceProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1024, 1280);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(NextBtn);
            Controls.Add(InformationBox);
            Controls.Add(UnabledInformation);
            Controls.Add(panel2);
            Controls.Add(HomeBtn);
            Controls.Add(TitleLayout);
            Controls.Add(product);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "InvoiceProductForm";
            ShowInTaskbar = false;
            Text = "GoodsForm";
            WindowState = FormWindowState.Maximized;
            Load += VoiceForm_Load;
            TitleLayout.ResumeLayout(false);
            TitleLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BackBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)HomeBtn).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)productComboArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)NextBtn).EndInit();
            product.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)inputMemoArea).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)inputPriceArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Panel TitleLayout;
        private PressKeyPictureBox BackBtn;
        private PressKeyPictureBox HomeBtn;
        private Panel panel1;
        private ComboBox ProductCategory;
        private PictureBox NextBtn;
        private RichTextBox UnabledInformation;
        private RichTextBox InformationBox;
        private RichTextBox RequestInputField;
        private Panel product;
        private Panel panel3;
        private RichTextBox ProductPriceInput;
        private Panel panel2;
        private PictureBox pictureBox2;
        private PictureBox inputPriceArea;
        private PictureBox inputMemoArea;
        private PictureBox productComboArea;
        private PictureBox pictureBox1;
    }
}