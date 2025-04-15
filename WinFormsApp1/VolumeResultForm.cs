using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Timers;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class VolumeResultForm : Form
    {
        public double maxSize = 300; // 단위: cm (가로 + 세로 + 높이)

        public double widthData;
        public double depthData;
        public double heightData;
        public double sizeData;
        public int costData;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public VolumeResultForm()
        {
            InitializeComponent();
            this.Show();
        }

        public VolumeResultForm(double wid, double dep, double hei)
        {
            InitializeComponent();

            widthData = wid;
            heightData = hei;
            depthData = dep;
            sizeData = widthData + depthData + heightData;
            costData = CalculateCost(hei, wid, dep);

            // 라벨 업데이트
            widthValue.Text = widthData.ToString();
            depthValue.Text = depthData.ToString();
            heightValue.Text = heightData.ToString();
            SizeValue.Text = sizeData.ToString("F0");

            this.Show();

            InitializeInactivityHandler();
        }

        private void SaveData()
        {
            DataCtrl.BoxWidth = widthData;
            DataCtrl.BoxDepth = depthData;
            DataCtrl.BoxHeight = heightData;
            DataCtrl.BoxVolume = sizeData;
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


        //public int CalculateCost(double hei, double wid, double dep)
        //{
        //    int cos = 0;

        //    int[] delivCostsArray = { 5000, 8000, 10000, 12000 };
        //    double[] isWeight = { 0, 5000, 10000, 20000, 30000 };
        //    double[] isLength = { 0, 80, 100, 120, 160 };

        //    int weiIndex = -1;
        //    int lenIndex = -1;

        //    double boxLength = wid + hei + dep;

        //    // 현재 인덱스 로직이 없으므로 기본값 0 사용
        //    cos = delivCostsArray[Math.Max(0, Math.Max(lenIndex, weiIndex))];

        //    double error = 1.0;
        //    double[] boxSizes = { 50, 60, 70, 80, 100, 120 };
        //    int[] boxCostsArray = { 700, 800, 900, 1100, 1700, 2300 };

        //    for (int i = 0; i < boxSizes.Length; i++)
        //    {
        //        if (boxSizes[i] - error <= boxLength && boxLength <= boxSizes[i] + error)
        //        {
        //            cos += boxCostsArray[i];
        //            break;
        //        }
        //    }

        //    return cos;
        //}
        public int CalculateCost(double hei, double wid, double dep)
        {
            double boxLength = hei + wid + dep; // 세 변수의 합
            int cos = 0;

            // boxLength에 따른 cos 계산
            if (boxLength <= 100)
            {
                cos = 3000;
                DataCtrl.TRN = 3000;
                DataCtrl.BoxType = "B";
            }
            else if (boxLength <= 120)
            {
                cos = 3500;
                DataCtrl.TRN = 3500;
                DataCtrl.BoxType = "C";
            }
            else if (boxLength <= 140)
            {
                cos = 4500;
                DataCtrl.TRN = 4500;
                DataCtrl.BoxType = "D";
            }
            else if (boxLength <= 160)
            {
                cos = 5500;
                DataCtrl.TRN = 5500;
                DataCtrl.BoxType = "E";
            }
            else
            {
                // 160을 초과하는 경우에 대한 로직이 필요하다면 여기에 추가
                // 예) cos = 7000;
            }

            return cos;
        }

        public void Go_Home(object sender, EventArgs e)
        {
            DataCtrl.ClearAll();
            HomeForm home = new HomeForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Back(object sender, EventArgs e)
        {
            VolumeForm weightsForm = new VolumeForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
        public async void Go_Next(object sender, EventArgs e)
        {
            if (sizeData <= 160.0)
            {
                SaveData();
                PaymentForm paymentForm = new PaymentForm(sizeData, costData);
                paymentForm.Show(); // Show 호출 추가
                Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                // 마우스 커서 위치 초기화
                Cursor.Position = new System.Drawing.Point(0, 300);
            }
            else
            {
                // 크기 초과
                MsgWindow msgWindow = new MsgWindow($"기준 크기인 가로 + 세로 + 높이 길이가 {maxSize}cm을 초과하였습니다");
                msgWindow.Show(); // Show 호출 추가
            }
        }

        // ReMeasure 메서드 수정
        public async void ReMeasure(object sender, EventArgs e)
        {
            ResetInactivityTimer(); // 타이머 초기화
            try
            {
                // 로딩 인디케이터 표시 (필요 시 구현)
                VolumeLoadingForm loadingForm = new VolumeLoadingForm();
                loadingForm.Show();

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

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    // 비동기적으로 출력 읽기
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string errors = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync(); // 프로세스가 종료될 때까지 대기

                    if (!string.IsNullOrEmpty(errors))
                    {
                        MessageBox.Show($"Python 에러:\n{errors}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                if (!string.IsNullOrEmpty(errors))
                                {
                                    new MsgWindow("박스가 감지되지 않았습니다.").Show();
                                    loadingForm.Close();
                                    return;
                                }
                                else
                                {
                                    // Height_cm이 string 또는 double일 수 있으므로 적절히 처리
                                    double height;
                                    if (double.TryParse(result.Height_cm.ToString(), out height))
                                    {
                                        // 데이터 업데이트
                                        widthData = result.Width_cm;
                                        depthData = result.Length_cm;
                                        heightData = height;
                                        sizeData = widthData + depthData + heightData;
                                        costData = CalculateCost(height, widthData, depthData);

                                        // UI 업데이트
                                        widthValue.Text = widthData.ToString("F1");
                                        depthValue.Text = depthData.ToString("F1");
                                        heightValue.Text = heightData.ToString("F1");
                                        SizeValue.Text = sizeData.ToString("F0");

                                        loadingForm.Close();
                                        // 필요 시 cost 표시
                                        // costValue.Text = costData.ToString();
                                    }
                                    else
                                    {
                                        // Height_cm이 오류 메시지인 경우
                                        string heightError = result.Height_cm.ToString();
                                        MessageBox.Show($"높이 계산 오류: {heightError}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // 로딩 인디케이터 숨기기 (필요 시 구현)
                // loadingForm.Close();
            }
        }

        private void VolumeResultForm_Load(object sender, EventArgs e)
        {

        }

        private void widthValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void VolumeResultForm_Load_1(object sender, EventArgs e)
        {

        }

        private void SizeValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void depthValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void heightValue_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
