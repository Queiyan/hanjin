using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinFormsApp1.Services
{
    public partial class PostcodeSearchForm : Form
    {
        private readonly IPostcodeService _postcodeService;
        private readonly Action<string, string> _onAddressSelected;

        public PostcodeSearchForm(IPostcodeService postcodeService, Action<string, string> onAddressSelected)
        {
            InitializeComponent();
            _postcodeService = postcodeService;
            _onAddressSelected = onAddressSelected;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PostcodeSearchForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "PostcodeSearchForm";
            this.Text = "우편번호 검색";
            this.ResumeLayout(false);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string query = rtxtSearch.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("검색어를 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_postcodeService.ValidateSearchQuery(query, out string sanitizedQuery))
            {
                MessageBox.Show("유효하지 않은 검색어입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (zipCode, address) = await _postcodeService.SearchAddressAsync(sanitizedQuery);
                if (zipCode != null && address != null)
                {
                    _onAddressSelected(zipCode, address);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("검색 결과가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 