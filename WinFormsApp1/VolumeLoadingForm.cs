using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class VolumeLoadingForm : Form
    {
        public VolumeLoadingForm()
        {
            InitializeComponent();
            InitializeMediaPlayer();
            this.Show();
        }

        private void InitializeMediaPlayer()
        {
            // appassets/mv 폴더에서 동영상 파일 경로 설정
            string appAssetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "mv");
            string videoFileName = "MeasuringWeight.avi"; // 동영상 파일명
            string videoPath = Path.Combine(appAssetsPath, videoFileName);

            if (File.Exists(videoPath))
            {
                this.axWindowsMediaPlayer1.URL = videoPath;
                this.axWindowsMediaPlayer1.settings.setMode("loop", true); // 무한 반복 설정
                this.axWindowsMediaPlayer1.uiMode = "none"; // UI 숨김
                this.axWindowsMediaPlayer1.Ctlcontrols.play(); // 재생 시작
            }
            else
            {
                MessageBox.Show($"동영상 파일을 찾을 수 없습니다: {videoPath}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Go_Home(object sender, EventArgs e)
        {
            DataCtrl.ClearAll();
            HomeForm home = new HomeForm();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
