using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class MeasureCtrl
    {
        public static MeasureCtrl instance;
        private SerialPort serialPort;

        public string weight = "";
        public string width = "";
        public string depth = "";
        public string height = "";

        public MeasureCtrl()
        {
            if(instance == null)
            {
                instance = this;
            }

            serialPort = new SerialPort("COM6", 9600);
        }

        public void MeasureWeight()
        {
            try
            {
                // 시리얼 포트가 열려 있지 않으면 열기
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                // 시리얼 포트로부터 하나의 데이터만 읽기
                string data = serialPort.ReadLine();
                Console.WriteLine("수신된 데이터: " + data); // 디버깅을 위한 출력

                // 수신된 데이터에서 숫자 부분만 Extract
                weight = new string(data.Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());
                Console.WriteLine("Extract된 숫자 부분: " + weight);

                // 값을 읽은 후, 포트를 닫아서 다음에 다시 클릭할 때까지 기다리도록 할 수 있음
                serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("시리얼 포트 처리 중 오류 발생: " + ex.Message);
            }
        }

        public void MeasureSize()
        {
            string boxSize = BoxSizeCtrl.startBoxSizeProcess();
            string[] sizeData = boxSize.Split(',');
            width = sizeData[0];
            height = sizeData[1];
            depth = sizeData[2];
        }
    }
}
