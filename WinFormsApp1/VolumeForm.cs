using System;
using System.Diagnostics;
using System.IO; // 파일 경로를 다루기 위해 추가
using System.Text.Json; // JSON 파싱을 위해 추가
using System.Timers;

// 만약 .NET Framework를 사용 중이라면 Newtonsoft.Json을 사용할 수 있습니다.
// using Newtonsoft.Json;
using System.Windows.Forms;
using AxWMPLib; // Windows Media Player 컨트롤을 사용하기 위해 추가

namespace WinFormsApp1
{
    public partial class VolumeForm : Form
    {
        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public VolumeForm()
        {
            //CloseAllFormsExceptHome();
            this.Opacity = 0;
            InitializeComponent();
            InitializeMediaPlayer();
            this.Show();
            this.Shown += async (s, e) =>
            {
                await Task.Delay(600);
                //this.BringToFront(); // 폼을 맨 앞으로 가져옴
                this.Opacity = 1; // 투명도 복원
            };
            InitializeInactivityHandler();
        }

        ////////////////////////////////////////////////////////////60초 뒤 홈 화면으로 이동하는 타이머 초기화//////////////////////////////////////////////////////////////////
        // 
        private void InitializeInactivityHandler()
        {
            InitializeInactivityTimer();
            HookUserActivityEvents();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 초기화
        private void InitializeInactivityTimer()
        {
            inactivityTimer = new System.Timers.Timer(60000); // 60초
            inactivityTimer.Elapsed += InactivityTimer_Elapsed;
            inactivityTimer.AutoReset = false;
            inactivityTimer.Start();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 이벤트 핸들러
        private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Go_Home(this, EventArgs.Empty);
            });
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 재설정
        private void ResetInactivityTimer()
        {
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
                inactivityTimer.Start();
            }
        }

        // 사용자 활동 이벤트 후킹
        private void HookUserActivityEvents()
        {
            this.MouseMove += UserActivity;
            this.KeyPress += UserActivity;
            foreach (Control control in this.Controls)
            {
                control.MouseMove += UserActivity;
                control.KeyPress += UserActivity;
            }
        }

        // 사용자 활동 이벤트 핸들러
        private void UserActivity(object sender, EventArgs e)
        {
            ResetInactivityTimer();
        }
        // 폼 활성화 이벤트 핸들러
        private void Form_Activated(object sender, EventArgs e)
        {
            ResetInactivityTimer();
        }

        // 폼 비활성화 이벤트 핸들러
        private void Form_Deactivate(object sender, EventArgs e)
        {
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
            }
        }
        ////////////////////////////////////////////////////////////////////60초 뒤 홈 화면으로 이동하는 타이머 초기화//////////////////////////////////////////////////////////


        private void InitializeMediaPlayer()
        {
            // appassets/mv 폴더에서 동영상 파일 경로 설정
            string appAssetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "mv");
            string videoFileName = "WeightMeasurement.avi"; // 동영상 파일명
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
            home.Show();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            InvoiceProductForm product = new InvoiceProductForm();
            product.Show();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async void Go_Next(object sender, EventArgs e)
        {
            VolumeLoadingForm loadingForm = new VolumeLoadingForm();

            // Python 스크립트의 경로 설정
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string pythonScriptPath = Path.Combine(executablePath, "py", "hanjin_tof.py");

            // Python 실행 파일 경로 (PATH에 설정되어 있지 않다면 전체 경로를 지정)
            string pythonExePath = "python"; // 또는 @"C:\Python39\python.exe"

            // Python 스크립트에 전달할 인수 (필요에 따라 조정)
            string arguments = $"\"{pythonScriptPath}\" --no-plot"; // --no-plot 인수는 Python 스크립트에서 플롯을 건너뛰게 하기 위함

            // ProcessStartInfo 설정
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonExePath,
                Arguments = arguments,
                UseShellExecute = false, // 표준 출력 리디렉션을 위해 false
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.Combine(executablePath, "py") // 작업 디렉토리를 py 폴더로 설정
            };

            try
            {
                loadingForm.Show();
                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    // 비동기적으로 출력 읽기
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string errors = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync(); // 프로세스가 종료될 때까지 대기

                    if (!string.IsNullOrEmpty(errors))
                    {
                        //MessageBox.Show($"Python 에러:\n{errors}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        new MsgWindow("박스가 감지되지 않았습니다.").Show();
                        loadingForm.Close();
                        return;
                    }

                    if (!string.IsNullOrEmpty(output))
                    {
                        try
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            var result = JsonSerializer.Deserialize<CalculationResult>(output, options);
                            // 만약 Newtonsoft.Json을 사용한다면 아래와 같이 사용합니다.
                            // var result = JsonConvert.DeserializeObject<CalculationResult>(output);

                            if (result != null)
                            {
                                if (!string.IsNullOrEmpty(result.Error))
                                {
                                    MessageBox.Show($"오류: {result.Error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    // Height_cm이 string 또는 double일 수 있으므로 적절히 처리
                                    double height;
                                    if (double.TryParse(result.Height_cm.ToString(), out height))
                                    {
                                        // VolumeResultForm에 값을 전달하여 표시
                                        VolumeResultForm volumeResultForm = new VolumeResultForm(result.Width_cm, result.Length_cm, height);
                                        volumeResultForm.Show();
                                        loadingForm.Close();
                                        this.Close(); // 현재 폼을 닫습니다.
                                        // 마우스 커서 위치 초기화
                                        Cursor.Position = new System.Drawing.Point(0, 300);
                                    }
                                    else
                                    {
                                        // Height_cm이 오류 메시지인 경우
                                        string heightError = result.Height_cm.ToString();
                                        MessageBox.Show($"높이 계산 오류: {heightError}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        loadingForm.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Python 스크립트의 출력 형식을 이해할 수 없습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (JsonException je)
                        {
                            MessageBox.Show($"JSON 파싱 오류:\n{je.Message}\nPython 출력:\n{output}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Python 스크립트에서 출력된 내용이 없습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                await Task.Delay(600).ContinueWith(t => loadingForm.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void VolumeForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void VolumeForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
