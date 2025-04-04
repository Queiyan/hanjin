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
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            this.Opacity = 0; // 초기 투명도 설정
            InitializeComponent();
            this.Show();
            this.Shown += async (s, e) =>
            {
                await Task.Delay(500);
                this.Opacity = 1; // 투명도 복원
            };
        }

        public void Go_Kiosk(object sender, EventArgs e)
        {
            TermsForm kioskForm = new TermsForm();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Easy(object sender, EventArgs e)
        {
            ReservationMenu easyMenu = new ReservationMenu();
            Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Go_Mobile(object sender, EventArgs e)
        {
            // dev 테스트용
            //MobileMenu mobileMenu = new MobileMenu();
            //Task.Delay(600).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());

            CautionForm cautionForm = new CautionForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            Cursor.Position = new System.Drawing.Point(0, 200);

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void HomeForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
