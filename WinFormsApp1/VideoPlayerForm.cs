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
using AxWMPLib;
using WMPLib;

namespace WinFormsApp1
{
    public partial class VideoPlayerForm : Form
    {
        private string videoFile;
        private bool isTransitioning = false;

        public VideoPlayerForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            
            // 터치 이벤트 활성화
            this.IsMdiContainer = false;
            
            // 미디어 플레이어 초기화
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.enableContextMenu = false;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);  // 반복 재생 설정
            axWindowsMediaPlayer1.settings.volume = 100;  // 볼륨 최대로 설정
            axWindowsMediaPlayer1.settings.mute = false;  // 음소거 해제
            axWindowsMediaPlayer1.settings.playCount = 999999;  // 매우 큰 숫자로 설정하여 계속 반복
            axWindowsMediaPlayer1.settings.rate = 1.0;  // 재생 속도를 기본값으로 설정
            axWindowsMediaPlayer1.settings.defaultFrame = "0";  // 프레임 설정 초기화

            // 비디오 파일 경로 설정
            videoFile = Path.Combine(Application.StartupPath, "Resources", "video.mp4");

            // 마우스 및 터치 이벤트 핸들러 등록
            this.MouseClick += VideoPlayerForm_MouseClick;
            this.MouseDown += VideoPlayerForm_MouseDown;
            this.FormClosing += VideoPlayerForm_FormClosing;

            // playStateChange 이벤트 등록
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;

            // 생성자에서 바로 재생하지 않고, Shown 이벤트에서 재생
            this.Shown += (s, e) => 
            {
                // 약간의 지연 후 비디오 재생
                System.Threading.Thread.Sleep(500);
                PlayVideo();
            };
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_TOUCH = 0x0240;
            const int WM_POINTERDOWN = 0x0246;
            
            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_TOUCH || m.Msg == WM_POINTERDOWN)
            {
                // 비디오 재생 중지
                if (axWindowsMediaPlayer1 != null)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                }
                
                // 폼 종료
                this.Close();
            }
            base.WndProc(ref m);
        }

        private void PlayVideo()
        {
            try
            {
                if (File.Exists(videoFile))
                {
                    // 현재 재생 중인 미디어를 완전히 중지하고 정리
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    axWindowsMediaPlayer1.URL = "";
                    
                    // URL 설정
                    axWindowsMediaPlayer1.URL = videoFile;
                    
                    // 비디오가 로드될 때까지 대기
                    int retryCount = 0;
                    bool isLoaded = false;
                    
                    while (!isLoaded && retryCount < 50)
                    {
                        if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsReady)
                        {
                            isLoaded = true;
                            break;
                        }
                        System.Threading.Thread.Sleep(100);
                        Application.DoEvents();
                        retryCount++;
                    }
                    
                    if (isLoaded)
                    {
                        // 재생 시작
                        if (!isTransitioning)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("재생할 파일이 없습니다!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비디오 재생 중 오류 발생: {ex.Message}");
            }
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                if ((WMPPlayState)e.newState == WMPPlayState.wmppsPlaying)
                {
                    isTransitioning = false;
                }
                else if ((WMPPlayState)e.newState == WMPPlayState.wmppsReady)
                {
                    // Ready 상태에서 바로 재생 시작
                    if (!isTransitioning)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"상태 변경 중 오류 발생: {ex.Message}");
            }
        }

        private void VideoPlayerForm_MouseClick(object sender, MouseEventArgs e)
        {
            // 비디오 재생 중지
            if (axWindowsMediaPlayer1 != null)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
            this.Close();
        }

        private void VideoPlayerForm_MouseDown(object sender, MouseEventArgs e)
        {
            // 비디오 재생 중지
            if (axWindowsMediaPlayer1 != null)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
            this.Close();
        }

        private void VideoPlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.WindowState = FormWindowState.Maximized;
        }
    }
} 