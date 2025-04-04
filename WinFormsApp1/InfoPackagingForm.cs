using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace WinFormsApp1
{
    public partial class InfoPackagingForm : Form
    {
        private WebView2 webView;
        private bool isDragging = false;
        private Point dragStartPoint;

        public InfoPackagingForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            InitializeComponent();
            InitializeWebView();
            this.Show();
            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                this.Opacity = 1; // 투명도 복원
            };
        }

        private void InitializeWebView()
        {
            webView = new WebView2
            {
                Location = new Point(34, 214),
                Size = new Size(956, 1032),
                Dock = DockStyle.None // Dock 속성을 None으로 설정
            };
            this.Controls.Add(webView);
            webView.BringToFront();
            LoadHtmlContent();
        }

        private void LoadHtmlContent()
        {
            string htmlFilePath = Path.Combine(Application.StartupPath, "Resources/packaging", "anchor2_full.html");
            if (File.Exists(htmlFilePath))
            {
                webView.Source = new Uri(htmlFilePath);
            }
            else
            {
                MessageBox.Show("HTML 파일을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScrollHandle_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public void Go_Home(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            CautionForm caution = new CautionForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
