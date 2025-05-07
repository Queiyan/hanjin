// PostcodeSearchForm.cs

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WinFormsApp1
{
    [ComVisible(true)]
    public partial class PostcodeSearchForm : Form
    {
        public string ZipCode { get; private set; }
        public string Address { get; private set; }
        public string DetailedAddress { get; private set; }

        private CustomVirtualKeyboard keyboard;

        // 60초 뒤 홈 화면으로 이동하는 타이머
        private System.Timers.Timer inactivityTimer;

        public PostcodeSearchForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            InitializeComponent();
            this.Load += VoiceForm_Load;
            rtxtSearch.GotFocus += RichTextBox_GotFocus;
            //txtDetailedAddress.GotFocus += RichTextBox_GotFocus;
            this.FormClosed += PostcodeSearchForm_FormClosed;
            this.Shown += async (s, e) =>
            {
                await Task.Delay(300);
                this.Opacity = 1; // 투명도 복원
            };

            InitializeInactivityHandler();
        }

        private void VoiceForm_Load(object sender, EventArgs e)

        {
            // 가상 키보드 생성 및 표시
            ShowCustomVirtualKeyboard();

            // 폼 로드 시 첫 번째 입력 필드에 포커스 설정
            rtxtSearch.Focus();
            keyboard.SetTargetInputField(rtxtSearch); // 키보드의 대상 필드도 설정

            //Console.WriteLine($"ActiveControl after load: {this.ActiveControl?.Name ?? "null"}");

            // 모든 RichTextBox의 TextChanged 이벤트에 핸들러 추가
            foreach (Control control in this.Controls)
            {
                if (control is RichTextBox richTextBox)
                {
                    //richTextBox.TextChanged += RichTextBox_TextChanged;
                    richTextBox.GotFocus += RichTextBox_GotFocus; // 포커스 이벤트 핸들러 추가
                    // MouseUp 이벤트 핸들러 등록
                    richTextBox.MouseUp += RichTextBox_MouseUp;
                    richTextBox.MouseDown += RichTextBox_MouseDown;
                    richTextBox.Click += RichTextBox_Click;
                }
            }
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
            inactivityTimer = new System.Timers.Timer(60000); // 60초로 통일
            inactivityTimer.Elapsed += InactivityTimer_Elapsed;
            inactivityTimer.AutoReset = false;
            inactivityTimer.Start();
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 이벤트 핸들러
        private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    if (keyboard != null && !keyboard.IsDisposed)
                    {
                        keyboard.Close();
                        keyboard.Dispose();
                    }
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            });
        }

        // 60초 뒤 홈 화면으로 이동하는 타이머 재설정
        public void ResetInactivityTimer()
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


        // MouseUp 이벤트 핸들러 구현
        private void RichTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.ResetComposingBuffer();
            }
        }
        private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.ResetComposingBuffer();
            }
        }
        private void RichTextBox_Click(object sender, EventArgs e)
        {
            if (keyboard != null)
            {
                keyboard.FinalizeBlock();
            }
        }

        private void RichTextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                keyboard.SetTargetInputField(richTextBox);
                richTextBox.Focus(); // 포커스 설정 (이미 포커스를 받았겠지만 명시적으로 설정)
            }
        }
        private void ShowCustomVirtualKeyboard()
        {
            keyboard = new CustomVirtualKeyboard(this.Handle);
            keyboard.Location = new Point(0, 764);
            keyboard.NextKeyPressed += Keyboard_NextKeyPressed;
            keyboard.Show();
        }

        private void Keyboard_NextKeyPressed(object sender, EventArgs e)
        {
            //Go_Next(sender, e);
        }

        private async void SearchAddress(string query)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiKey = "U01TX0FVVEgyMDI1MDIwNzE1Mzc1NTExNTQ1NTA="; // API 키 사용
                    string requestUrl = $"https://www.juso.go.kr/addrlink/addrLinkApi.do?confmKey={apiKey}&currentPage=1&countPerPage=10&keyword={query}&resultType=json";

                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseBody);

                    if (json["results"]["common"]["errorCode"].ToString() == "0")
                    {
                        lvResults.Items.Clear();
                        var jusoList = json["results"]["juso"];
                        foreach (var juso in jusoList)
                        {
                            string zipNo = juso["zipNo"].ToString();
                            string roadAddr = juso["roadAddr"].ToString();
                            ListViewItem item = new ListViewItem(new[] { zipNo, roadAddr });
                            lvResults.Items.Add(item);
                        }

                        if (lvResults.Items.Count > 0)
                        {
                            lvResults.Items[0].Selected = true;
                        }
                    }
                    else
                    {
                        string errorMessage = json["results"]["common"]["errorMessage"].ToString();
                        new MiniMsgWindow(errorMessage).Show();
                        //MessageBox.Show($"API 오류: {errorMessage}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                new MiniMsgWindow(ex.Message).Show();
                //MessageBox.Show($"주소 검색 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = rtxtSearch.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                //MessageBox.Show("검색어를 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                new MiniMsgWindow("검색어를 입력하세요").Show();
                return;
            }

            if (!ValidateSearchQuery(query, out string sanitizedQuery))
            {
                // 유효하지 않은 검색어이므로 검색을 중단합니다.
                return;
            }

            SearchAddress(sanitizedQuery);
        }

        /// <summary>
        /// 검색어의 유효성을 검사하고 필요한 필터링을 적용합니다.
        /// </summary>
        /// <param name="query">입력된 검색어</param>
        /// <param name="sanitizedQuery">필터링된 검색어</param>
        /// <returns>유효한 검색어인지 여부</returns>
        private bool ValidateSearchQuery(string query, out string sanitizedQuery)
        {
            sanitizedQuery = query;

            // 특수문자 제거
            string specialCharsPattern = @"[%=><]";
            if (Regex.IsMatch(sanitizedQuery, specialCharsPattern))
            {
                new MiniMsgWindow("특수문자를 입력할 수 없습니다").Show();
                //MessageBox.Show("특수문자를 입력할 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sanitizedQuery = Regex.Replace(sanitizedQuery, specialCharsPattern, "");
                return false;
            }

            // SQL 예약어 배열
            string[] sqlKeywords = new string[]
            {
                "OR", "SELECT", "INSERT", "DELETE", "UPDATE", "CREATE", "DROP", "EXEC",
                "UNION", "FETCH", "DECLARE", "TRUNCATE"
            };

            foreach (string keyword in sqlKeywords)
            {
                // 단어 경계(\b)를 사용하여 정확한 단어만 매칭
                string pattern = $@"\b{keyword}\b";
                if (Regex.IsMatch(sanitizedQuery, pattern, RegexOptions.IgnoreCase))
                {
                    MessageBox.Show($"\"{keyword}\"와(과) 같은 특정문자로 검색할 수 없습니다", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sanitizedQuery = Regex.Replace(sanitizedQuery, pattern, "", RegexOptions.IgnoreCase);
                    return false;
                }
            }

            return true;
        }

        private void lvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvResults.SelectedItems.Count > 0)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            rtxtSearch.Clear();
            if (lvResults.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvResults.SelectedItems[0];
                ZipCode = selectedItem.SubItems[0].Text;
                Address = selectedItem.SubItems[1].Text;

                using (var detailedAddressForm = new DetailedAddressForm(Address, this))
                {
                    if (detailedAddressForm.ShowDialog() == DialogResult.OK)
                    {
                        DetailedAddress = detailedAddressForm.DetailedAddress;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
                        keyboard.Dispose();
                    }
                }
            }
            else
            {
                new MiniMsgWindow("주소를 검색 하세요").Show();
            }
        }


        // 제목 표시줄 드래그 이동
        private void TitleBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // 구현 필요 시 추가
        }

        // 최소화 버튼 클릭 이벤트
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // 최대화/복원 버튼 클릭 이벤트
        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            // 구현 필요 시 추가
        }

        // 닫기 버튼 클릭 이벤트
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => keyboard.Dispose(), TaskScheduler.FromCurrentSynchronizationContext());
        }


        private void PostcodeSearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Task.Delay(500).ContinueWith(t => keyboard.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Delay(500).ContinueWith(t => keyboard.Dispose(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void PostcodeSearchForm_Load_1(object sender, EventArgs e)
        {
            // 추가 초기화가 필요하면 구현
        }

        private void rtxtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
                inactivityTimer.Dispose();
            }
            if (keyboard != null && !keyboard.IsDisposed)
            {
                keyboard.Close();
                keyboard.Dispose();
            }
        }
    }
}
