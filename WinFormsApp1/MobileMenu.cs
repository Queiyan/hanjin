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
    public partial class MobileMenu : Form
    {
        public MobileMenu()
        {
            InitializeComponent();
            this.Show();
        }

        public void Go_Home(object sender, EventArgs e)
        {
            this.Close();
            HomeForm home = new HomeForm();
        }

        public void Go_Back(object sender, EventArgs e)
        {
            this.Close();
            HomeForm home = new HomeForm();
        }

        public void devTest(object sender, EventArgs e)
        {
            /////////////////////////////////////////////송장 출력 테스트/////////////////////////////////////////////
            //int cost = 5000;

            //DataCtrl.SenderPhoneNo = "010-1234-5678";
            //DataCtrl.SenderName = "김준혁";
            //DataCtrl.SenderAddress = "22228";
            //DataCtrl.SenderAddress2 = "인천광역시 미추홀구 경원대로 753(주안동)";
            //DataCtrl.SenderAddress3 = "1동 101호";

            //DataCtrl.ReceiverPhoneNo = "02-9876-5432";
            //DataCtrl.ReceiverName = "이재용";
            //DataCtrl.ReceiverAddress = "06620";
            //DataCtrl.ReceiverAddress2 = "서울특별시 서초구 서초대로74길 11(서초동)";
            //DataCtrl.ReceiverAddress3 = "33층";

            //DataCtrl.ProductCategory = "소형전자";
            //DataCtrl.ProductPriceInput = "1,000,000";
            //DataCtrl.RequestInputField = "취급주의, 깨짐주의";

            //DataCtrl.BoxVolume = 120;

            //InvoicePrintForm invoiceForm = new InvoicePrintForm(cost);
            //Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            /////////////////////////////////////////////결제 테스트/////////////////////////////////////////////
            DataCtrl.ProductPriceInput = "500";

            int costData = 500;
            double sizeData = 120;

            PaymentForm paymentForm = new PaymentForm(sizeData, costData);

            this.Close();
        }

        private void MobileMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
