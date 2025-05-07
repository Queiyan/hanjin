namespace WinFormsApp1.Services
{
    partial class PostcodeSearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox rtxtSearch;
        private System.Windows.Forms.Button btnSearch;

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
            this.rtxtSearch = new System.Windows.Forms.RichTextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtxtSearch
            // 
            this.rtxtSearch.Location = new System.Drawing.Point(12, 12);
            this.rtxtSearch.Name = "rtxtSearch";
            this.rtxtSearch.Size = new System.Drawing.Size(600, 30);
            this.rtxtSearch.TabIndex = 0;
            this.rtxtSearch.Text = "";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(618, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // PostcodeSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.rtxtSearch);
            this.Name = "PostcodeSearchForm";
            this.Text = "우편번호 검색";
            this.ResumeLayout(false);
        }
    }
} 