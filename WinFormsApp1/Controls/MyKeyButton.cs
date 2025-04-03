using System;
using System.ComponentModel;  // Category, Browsable 등
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WinFormsApp1.Controls
{
    public class MyKeyButton : PictureBox
    {
        // -------------------
        // 이미지 프로퍼티들 (8종)
        // -------------------

        [Category("Appearance")]
        [Description("한글(Shift Off) - 기본 상태 이미지")]
        public Image KorLowerNormalImage { get; set; }

        [Category("Appearance")]
        [Description("한글(Shift Off) - 눌림 상태 이미지")]
        public Image KorLowerPressedImage { get; set; }

        [Category("Appearance")]
        [Description("한글(Shift On) - 기본 상태 이미지")]
        public Image KorUpperNormalImage { get; set; }

        [Category("Appearance")]
        [Description("한글(Shift On) - 눌림 상태 이미지")]
        public Image KorUpperPressedImage { get; set; }

        [Category("Appearance")]
        [Description("영어(Shift Off) - 기본 상태 이미지")]
        public Image EngLowerNormalImage { get; set; }

        [Category("Appearance")]
        [Description("영어(Shift Off) - 눌림 상태 이미지")]
        public Image EngLowerPressedImage { get; set; }

        [Category("Appearance")]
        [Description("영어(Shift On) - 기본 상태 이미지")]
        public Image EngUpperNormalImage { get; set; }

        [Category("Appearance")]
        [Description("영어(Shift On) - 눌림 상태 이미지")]
        public Image EngUpperPressedImage { get; set; }

        // -------------------
        // 내부 상태
        // -------------------
        private bool isPressed = false;  // 마우스 눌림 여부
        private bool isKor = true;   // 한글/영문
        private bool isShift = false;  // Shift On/Off

        // -------------------
        // 외부에서 읽고 쓸 수 있는 한글/영문, Shift 속성
        // -------------------
        [Browsable(false)]
        public bool IsKor
        {
            get => isKor;
            set
            {
                if (isKor != value)
                {
                    isKor = value;
                    RefreshImage();
                }
            }
        }

        [Browsable(false)]
        public bool IsShift
        {
            get => isShift;
            set
            {
                if (isShift != value)
                {
                    isShift = value;
                    RefreshImage();
                }
            }
        }

        public MyKeyButton()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // -------------------
        // 마우스 이벤트
        // -------------------
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isPressed = true;
            RefreshImage();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isPressed = false;
            RefreshImage();
        }

        // -------------------
        // 이미지 갱신 로직
        // -------------------
        public void RefreshImage()
        {
            //Console.WriteLine($"RefreshImage 진입, 값 : {isKor}, {isShift}");
            Image selected = null;

            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new Action(RefreshImage));
            //    return;
            //}

            if (isKor)
            {
                if (!isShift)
                {
                    selected = isPressed ? KorLowerPressedImage : KorLowerNormalImage;
                    //Console.WriteLine(selected != null ? "KorLower 이미지 설정됨" : "KorLower 이미지 null");
                }
                else
                {
                    selected = isPressed ? KorUpperPressedImage : KorUpperNormalImage;
                    //Console.WriteLine(selected != null ? "KorUpper 이미지 설정됨" : "KorUpper 이미지 null");
                }
            }
            else
            {
                if (!isShift)
                {
                    selected = isPressed ? EngLowerPressedImage : EngLowerNormalImage;
                    //Console.WriteLine(selected != null ? "EngLower 이미지 설정됨" : "EngLower 이미지 null");
                }
                else
                {
                    selected = isPressed ? EngUpperPressedImage : EngUpperNormalImage;
                    //Console.WriteLine(selected != null ? "EngUpper 이미지 설정됨" : "EngUpper 이미지 null");
                }
            }

            if (selected != null)
            {
                this.Image = selected; // 새로운 Bitmap 대신 기존 이미지를 사용
                //Console.WriteLine("이미지 적용됨");
            }
            else
            {
                this.Image = null;
                Console.WriteLine("이미지 null 설정됨");
            }

            //this.Invalidate(); // 컨트롤 다시 그리기
        }

        // 모든 MyKeyButton 인스턴스의 이미지를 갱신
        //private void RefreshAllButtons()
        //{
        //    var parentForm = this.FindForm();
        //    if (parentForm != null)
        //    {
        //        Console.WriteLine("RefreshAllButtons 호출됨");
        //        foreach (var btn in parentForm.Controls.OfType<MyKeyButton>())
        //        {
        //            Console.WriteLine($"RefreshImage 호출됨 for {btn.Name}");
        //            btn.RefreshImage();
        //        }
        //    }
        //}

        // 디자이너 상에서 NormalImage 설정 직후
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            // 처음 표시 시점에, 한글 Lower Normal을 표시할 수도 있음
            RefreshImage();
        }
    }
}



