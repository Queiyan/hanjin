﻿using System;
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
            // Click 이벤트 추가
            this.Click += MyKeyButton_Click;
        }

        // -------------------
        // 터치/클릭 이벤트
        // -------------------
        private void MyKeyButton_Click(object sender, EventArgs e)
        {
            // 터치/클릭 시 시각적 피드백
            isPressed = true;
            RefreshImage();

            // 짧은 지연 후 원래 상태로 복원
            System.Threading.Tasks.Task.Delay(100).ContinueWith(_ =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        isPressed = false;
                        RefreshImage();
                    }));
                }
                else
                {
                    isPressed = false;
                    RefreshImage();
                }
            });
        }

        // -------------------
        // 이미지 갱신 로직
        // -------------------
        public void RefreshImage()
        {
            Image selected = null;

            if (isKor)
            {
                if (!isShift)
                {
                    selected = isPressed ? KorLowerPressedImage : KorLowerNormalImage;
                }
                else
                {
                    selected = isPressed ? KorUpperPressedImage : KorUpperNormalImage;
                }
            }
            else
            {
                if (!isShift)
                {
                    selected = isPressed ? EngLowerPressedImage : EngLowerNormalImage;
                }
                else
                {
                    selected = isPressed ? EngUpperPressedImage : EngUpperNormalImage;
                }
            }

            if (selected != null)
            {
                this.Image = selected;
            }
            else
            {
                this.Image = null;
                Console.WriteLine("이미지 null 설정됨");
            }
        }

        // 디자이너 상에서 NormalImage 설정 직후
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            RefreshImage();
        }
    }
}



