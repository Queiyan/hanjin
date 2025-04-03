using System.ComponentModel;

namespace WinFormsApp1.Controls
{
    public class PressKeyPictureBox : PictureBox
    {
        private bool isPressed = false;

        [Category("Appearance")]
        [Description("기본 상태 이미지")]
        public Image NormalImage { get; set; }

        [Category("Appearance")]
        [Description("눌림 상태 이미지")]
        public Image PressedImage { get; set; }

        public PressKeyPictureBox()
        {
            this.MouseEnter += MyKeyButton_MouseEnter;
            this.MouseLeave += MyKeyButton_MouseLeave;
        }

        private void MyKeyButton_MouseEnter(object sender, EventArgs e)
        {
            if (PressedImage != null)
            {
                this.Image = PressedImage;
            }
        }

        private void MyKeyButton_MouseLeave(object sender, EventArgs e)
        {
            if (NormalImage != null)
            {
                this.Image = NormalImage;
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