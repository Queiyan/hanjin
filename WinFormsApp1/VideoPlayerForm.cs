using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AxWMPLib;
using WMPLib;

namespace WinFormsApp1
{
    public partial class VideoPlayerForm : Form
    {
        private string videoFile;
        private bool isTransitioning = false;
        private bool isDisposing = false;

        public VideoPlayerForm()
        {
            InitializeComponent();

            // 기본 폼 설정
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.Size = new Size(1024, 1280);
            this.TopMost = true;

            // 비디오 파일 경로 설정
            videoFile = Path.Combine(Application.StartupPath, "Resources", "video.mp4");

            try
            {
                // 미디어 플레이어 기본 설정
                axWindowsMediaPlayer1.Location = new Point(0, 0);
                axWindowsMediaPlayer1.Size = new Size(1024, 1280);
                axWindowsMediaPlayer1.stretchToFit = true;
                axWindowsMediaPlayer1.enableContextMenu = false;
                axWindowsMediaPlayer1.uiMode = "none";

                // 투명 패널을 AxWindowsMediaPlayer 위에 추가
                var touchPanel = new TransparentPanel();
                touchPanel.Dock = DockStyle.Fill;
                touchPanel.BackColor = Color.Transparent;
                touchPanel.MouseClick += (s, e) => CloseVideo();
                this.Controls.Add(touchPanel);
                touchPanel.BringToFront();

                // 폼이 표시된 후 비디오 재생
                this.Shown += async (s, e) => 
                {
                    await Task.Delay(500);
                    try 
                    {
                        if (!isDisposing && File.Exists(videoFile))
                        {
                            axWindowsMediaPlayer1.URL = videoFile;
                            axWindowsMediaPlayer1.settings.setMode("loop", true);
                            axWindowsMediaPlayer1.settings.volume = 100;
                            axWindowsMediaPlayer1.settings.autoStart = true;
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                        }
                        else if (!isDisposing)
                        {
                            MessageBox.Show("비디오 파일을 찾을 수 없습니다.");
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!isDisposing)
                        {
                            MessageBox.Show($"비디오 재생 중 오류 발생: {ex.Message}");
                            this.Close();
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"초기화 중 오류 발생: {ex.Message}");
                isDisposing = true;
                this.Close();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = new Point(0, 0);
            this.Size = new Size(1024, 1280);
        }

        private void CloseVideo()
        {
            if (isDisposing) return;
            
            isDisposing = true;
            try
            {
                if (axWindowsMediaPlayer1 != null)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    axWindowsMediaPlayer1.close();
                    axWindowsMediaPlayer1.URL = "";
                }
            }
            catch { }
            finally
            {
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isDisposing = true;
            base.OnFormClosing(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~0x40000; // WS_CHILD 스타일 제거
                cp.ExStyle |= 0x8; // WS_EX_TOPMOST 설정
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_POINTERDOWN = 0x0246;
            const int WM_POINTERUP = 0x0247;
            const int WM_POINTERUPDATE = 0x0245;

            switch (m.Msg)
            {
                case WM_POINTERDOWN:
                case WM_POINTERUP:
                case WM_POINTERUPDATE:
                    if (!isDisposing)
                    {
                        CloseVideo();
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (isDisposing) return;

            try
            {
                if ((WMPPlayState)e.newState == WMPPlayState.wmppsPlaying)
                {
                    isTransitioning = false;
                }
                else if ((WMPPlayState)e.newState == WMPPlayState.wmppsReady)
                {
                    if (!isTransitioning && !isDisposing)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!isDisposing)
                {
                    MessageBox.Show($"상태 변경 중 오류 발생: {ex.Message}");
                }
            }
        }
    }
} 