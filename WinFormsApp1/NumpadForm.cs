using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// 필요 없는 using 문은 제거하셔도 됩니다.
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class NumpadForm : Form
    {
        public RichTextBox targetInputField;
        public event EventHandler NextKeyPressed;
        public string inputStr = "";

        public NumpadForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            this.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            InitializeComponent();
            this.Show();
            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                //this.BringToFront(); // 폼을 맨 앞으로 가져옴
                this.Opacity = 1; // 투명도 복원
            };
            this.TopMost = true;
            //this.Activate();
        }

        public async void EnterNumber(object sender, EventArgs e)
        {
            PictureBox numBox = (PictureBox)sender;
            string name = numBox.Name;

            if (targetInputField == null)
            {
                Console.WriteLine("NumPad targetInputField is null.");
                return;
            }

            if (!targetInputField.Focused)
            {
                targetInputField.Focus();
            }

            int selectionStart = targetInputField.SelectionStart;
            int selectionLength = targetInputField.SelectionLength;

            // 하이픈을 제거한 순수 숫자 문자열과 커서 이전의 숫자 개수 계산
            string unformattedText = targetInputField.Text.Replace("-", "");
            int digitsBeforeCursor = targetInputField.Text.Substring(0, selectionStart).Count(char.IsDigit);

            if (name == "BackSpace")
            {
                if (selectionLength > 0)
                {
                    // 선택된 텍스트 삭제
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart, selectionLength);
                }
                else if (selectionStart > 0)
                {
                    // 커서 앞의 한 문자 삭제
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart - 1, 1);
                    selectionStart--;
                    digitsBeforeCursor--;
                }
            }
            else if (name == "Delete")
            {
                if (selectionLength > 0)
                {
                    // 선택된 텍스트 삭제
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart, selectionLength);
                }
                else if (selectionStart < targetInputField.Text.Length)
                {
                    // 커서 뒤의 한 문자 삭제
                    targetInputField.Text = targetInputField.Text.Remove(selectionStart, 1);
                }
            }
            else if (name == "Enter")
            {
                //SendKeys.Send("{TAB}");
                OnNextKeyPressed(EventArgs.Empty);
                return;
            }
            else if (name.Contains("num"))
            {
                if (unformattedText.Length < 11) // 최대 11자리 숫자 제한
                {
                    string num = name.Replace("num", "");
                    if (selectionLength > 0)
                    {
                        // 선택된 텍스트 대체
                        targetInputField.Text = targetInputField.Text.Remove(selectionStart, selectionLength).Insert(selectionStart, num);
                    }
                    else
                    {
                        // 커서 위치에 숫자 삽입
                        targetInputField.Text = targetInputField.Text.Insert(selectionStart, num);
                    }
                    selectionStart += num.Length;
                    digitsBeforeCursor += num.Length;
                }
            }

            // 하이픈 제거된 숫자만 추출하여 포맷팅
            string digits = targetInputField.Text.Replace("-", "");
            //targetInputField.Text = FormatPhoneNumber(digits);

            // 포맷팅 후 커서 위치 조정
            targetInputField.SelectionStart = GetCursorPositionFromDigitIndex(targetInputField.Text, digitsBeforeCursor);
            targetInputField.SelectionLength = 0;
            targetInputField.Focus();

            // 0.2초 후 마우스 커서 위치 초기화
            await Task.Delay(200);
            Cursor.Position = new System.Drawing.Point(0, 764);
        }

        private int GetCursorPositionFromDigitIndex(string formattedText, int digitIndex)
        {
            int digitsCount = 0;
            for (int i = 0; i < formattedText.Length; i++)
            {
                if (char.IsDigit(formattedText[i]))
                {
                    if (digitsCount == digitIndex)
                    {
                        return i;
                    }
                    digitsCount++;
                }
            }
            return formattedText.Length;
        }

        private string FormatPhoneNumber(string digits)
        {
            if (digits.StartsWith("02"))
            {
                // 02-XXXX-XXXX 형식
                if (digits.Length > 2)
                {
                    string part1 = digits.Substring(0, 2);
                    string part2 = digits.Length >= 6 ? digits.Substring(2, 4) : digits.Substring(2);
                    string part3 = digits.Length > 6 ? digits.Substring(6) : "";

                    return string.Join("-", new[] { part1, part2, part3 }.Where(s => !string.IsNullOrEmpty(s)));
                }
                else
                {
                    return digits;
                }
            }
            else if (digits.Length == 10)
            {
                // XXX-XXX-XXXX 형식
                string part1 = digits.Substring(0, 3);
                string part2 = digits.Substring(3, 3);
                string part3 = digits.Substring(6, 4);

                return $"{part1}-{part2}-{part3}";
            }
            else
            {
                // 휴대폰 번호 또는 기타 지역 번호
                if (digits.Length > 3)
                {
                    string part1 = digits.Substring(0, 3);
                    string part2 = digits.Length >= 7 ? digits.Substring(3, 4) : digits.Substring(3);
                    string part3 = digits.Length > 7 ? digits.Substring(7) : "";

                    return string.Join("-", new[] { part1, part2, part3 }.Where(s => !string.IsNullOrEmpty(s)));
                }
                else
                {
                    return digits;
                }
            }
        }


        public void SetTargetInputField(RichTextBox fieldBox)
        {
            if (targetInputField != fieldBox)
            {
                targetInputField = fieldBox;
            }
        }

        protected virtual void OnNextKeyPressed(EventArgs e)
        {
            NextKeyPressed?.Invoke(this, e);
        }

        // 필요하지 않은 메서드는 제거하거나 비워 둘 수 있습니다.
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void keyLayout1_Paint(object sender, PaintEventArgs e) { }
        private void NumpadForm_Load(object sender, EventArgs e) { }
    }
}
