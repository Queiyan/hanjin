using System.ComponentModel;

namespace WinFormsApp1.Controls
{
    public class PressKeyPictureBox : PictureBox
    {
        [Category("Appearance")]
        [Description("기본 상태 이미지")]
        public Image NormalImage { get; set; }

        [Category("Appearance")]
        [Description("눌림 상태 이미지")]
        public Image PressedImage { get; set; }

        public PressKeyPictureBox()
        {
            // Click 이벤트만 사용
            this.Click += PressKeyPictureBox_Click;
        }

        private void PressKeyPictureBox_Click(object sender, EventArgs e)
        {
            if (PressedImage != null)
            {
                this.Image = PressedImage;
                // 짧은 지연 후 원래 이미지로 복원
                System.Threading.Tasks.Task.Delay(100).ContinueWith(_ =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (NormalImage != null)
                            {
                                this.Image = NormalImage;
                            }
                        }));
                    }
                    else
                    {
                        if (NormalImage != null)
                        {
                            this.Image = NormalImage;
                        }
                    }
                });
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (NormalImage != null)
            {
                this.Image = NormalImage;
            }
        }
    }
}