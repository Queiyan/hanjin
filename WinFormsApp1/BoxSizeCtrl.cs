using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class BoxSizeCtrl
    {
        public static string startBoxSizeProcess()
        {
            string result = "";
            string sizeComm = "python ../../../../server/api_request_ocr_vol.py";

            result = CmdCtrl.RunCMD(sizeComm);

            string[] splitedSizeData = result.Split("{'flag': True}, {")[1].Split('}')[0].Trim().Split(',');

            string wid = "";
            string hei = "";
            string dep = "";

            for (int i = 0; i < splitedSizeData.Length; i++)
            {
                string[] senderDatas = splitedSizeData[i].Split(':');
                string key = senderDatas[0].Trim().Replace("'", "");
                string data = senderDatas[1].Trim().Replace("'", "");

                if (key == "width")
                {
                    wid = data;
                }
                else if (key == "dep")
                {
                    hei = data;
                }
                else if (key == "hei")
                {
                    dep = data;
                }
                else
                {
                    Console.WriteLine("Nothing detected by key in sender data");
                }
            }

            if((wid + hei + dep).Length > 0)
            {
                result = wid + "," + hei + "," + dep;
            }

            return result;
        }
    }
}
