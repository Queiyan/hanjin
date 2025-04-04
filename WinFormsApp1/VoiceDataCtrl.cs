using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class VoiceDataCtrl
    {

        public static string ResultCode { get; set; } = string.Empty;
        public static string ResultMessage { get; set; } = string.Empty;
        public static string MsgKey { get; set; } = string.Empty;
        public static string STmlNam { get; set; } = string.Empty;
        public static string STmlCod { get; set; } = string.Empty;
        public static string ZipCod { get; set; } = string.Empty;
        public static string TmlNam { get; set; } = string.Empty;
        public static string TmlCod { get; set; } = string.Empty;
        public static string CenNam { get; set; } = string.Empty;
        public static string CenCod { get; set; } = string.Empty;
        public static string PdTim { get; set; } = string.Empty;
        public static string DomRgn { get; set; } = string.Empty;
        public static string HubCod { get; set; } = string.Empty;
        public static string DomMid { get; set; } = string.Empty;
        public static string GrpRnk { get; set; } = string.Empty;
        public static string EsNam { get; set; } = string.Empty;
        public static string EsCod { get; set; } = string.Empty;
        public static string PrtAdd { get; set; } = string.Empty;
        public static string DlvTyp { get; set; } = string.Empty;
        public static string SvcNam { get; set; } = string.Empty;
        public static string PtnSrt { get; set; } = string.Empty;
        public static string SrtNam { get; set; } = string.Empty;
        public static string WblNum { get; set; } = string.Empty;

        public static void SaveHanjinApiResponse(
            string resultCode, string resultMessage, string msgKey, string sTmlNam, string sTmlCod,
            string zipCod, string tmlNam, string tmlCod, string cenNam, string cenCod,
            string pdTim, string domRgn, string hubCod, string domMid, string grpRnk,
            string esNam, string esCod, string prtAdd, string dlvTyp, string svcNam,
            string ptnSrt, string srtNam, string wblNum)
        {
            ResultCode = resultCode;
            ResultMessage = resultMessage;
            MsgKey = msgKey;
            STmlNam = sTmlNam;
            STmlCod = sTmlCod;
            ZipCod = zipCod;
            TmlNam = tmlNam;
            TmlCod = tmlCod;
            CenNam = cenNam;
            CenCod = cenCod;
            PdTim = pdTim;
            DomRgn = domRgn;
            HubCod = hubCod;
            DomMid = domMid;
            GrpRnk = grpRnk;
            EsNam = esNam;
            EsCod = esCod;
            PrtAdd = prtAdd;
            DlvTyp = dlvTyp;
            SvcNam = svcNam;
            PtnSrt = ptnSrt;
            SrtNam = srtNam;
            WblNum = wblNum;
        }

        // 전체 초기화 메서드
        public static void ClearAll()
        {
            ResultCode = string.Empty;
            ResultMessage = string.Empty;
            MsgKey = string.Empty;
            STmlNam = string.Empty;
            STmlCod = string.Empty;
            ZipCod = string.Empty;
            TmlNam = string.Empty;
            TmlCod = string.Empty;
            CenNam = string.Empty;
            CenCod = string.Empty;
            PdTim = string.Empty;
            DomRgn = string.Empty;
            HubCod = string.Empty;
            DomMid = string.Empty;
            GrpRnk = string.Empty;
            EsNam = string.Empty;
            EsCod = string.Empty;
            PrtAdd = string.Empty;
            DlvTyp = string.Empty;
            SvcNam = string.Empty;
            PtnSrt = string.Empty;
            SrtNam = string.Empty;
            WblNum = string.Empty;
        }


    }

}
