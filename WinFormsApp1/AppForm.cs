using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.BackColor = Color.White;
            MeasureCtrl measureCtrl = new MeasureCtrl();
            this.Show();
            // Set HomeForm as the initial MDI child
            HomeForm homeForm = new HomeForm();

            homeForm.Show();
        }

        public void SetFullScreen(Form obj = null)
        {
            if (obj != null)
            {
                obj.WindowState = FormWindowState.Maximized; // 최대화
                obj.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
                obj.Bounds = Screen.PrimaryScreen.Bounds; // 전체 화면 모드
            }
            else
            {
                this.WindowState = FormWindowState.Maximized; // 최대화
                this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
                this.Bounds = Screen.PrimaryScreen.Bounds; // 전체 화면 모드
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // AppForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1024, 1280);
            ControlBox = false;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "AppForm";
            SizeGripStyle = SizeGripStyle.Hide;
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }
    }
}
