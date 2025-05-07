using System;
using System.Windows.Forms;

namespace WinFormsApp1.Services
{
    public class PostcodeSearchExample
    {
        private readonly IPostcodeService _postcodeService;

        public PostcodeSearchExample()
        {
            // API 키는 실제 사용 시 환경 변수나 설정 파일에서 가져오는 것이 좋습니다.
            string apiKey = "U01TX0FVVEgyMDI1MDIwNzE1Mzc1NTExNTQ1NTA=";
            _postcodeService = new PostcodeService(apiKey);
        }

        public void ShowPostcodeSearchForm()
        {
            var form = new PostcodeSearchForm(_postcodeService, (zipCode, address) =>
            {
                // 주소가 선택되었을 때의 처리
                MessageBox.Show($"우편번호: {zipCode}\n주소: {address}", "주소 선택 완료");
            });

            form.ShowDialog();
        }
    }
} 