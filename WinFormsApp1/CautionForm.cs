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
    public partial class CautionForm : Form
    {
        public CautionForm()
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

        public void Go_Home(object sender, EventArgs e)
        {
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            HomeForm home = new HomeForm();
        }

        public void Go_Back(object sender, EventArgs e)
        {
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
            HomeForm home = new HomeForm();
        }

        private void MobileMenu_Load(object sender, EventArgs e)
        {

        }

        // 요금안내 페이지
        private void Go_FareInfo(object sender, EventArgs e)
        {
            InfoFareForm infoFareForm = new InfoFareForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void Go_PackagingInfo(object sender, EventArgs e)
        {
            InfoPackagingForm infoPackagingForm = new InfoPackagingForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void Go_RestrictedItems(object sender, EventArgs e)
        {
            InfoRestrictedItemForm infoRestrictedItemForm = new InfoRestrictedItemForm();
            Task.Delay(500).ContinueWith(t => this.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
