using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class DataCtrl
    {
        // Sender
        public static string SenderPhoneNo { get; set; } = string.Empty;
        public static string SenderName { get; set; } = string.Empty;
        //public static string SenderTelePhoneNo { get; set; } = string.Empty;
        public static string SenderAddress { get; set; } = string.Empty;
        public static string SenderAddress2 { get; set; } = string.Empty;
        public static string SenderAddress3 { get; set; } = string.Empty;

        // Receiver
        public static string ReceiverPhoneNo { get; set; } = string.Empty;
        public static string ReceiverName { get; set; } = string.Empty;
        //public static string ReceiverTelePhoneNo { get; set; } = string.Empty;
        public static string ReceiverAddress { get; set; } = string.Empty;
        public static string ReceiverAddress2 { get; set; } = string.Empty;
        public static string ReceiverAddress3 { get; set; } = string.Empty;

        // Product
        public static string ProductCategory { get; set; } = string.Empty;
        public static string ProductPriceInput { get; set; } = string.Empty;
        public static string RequestInputField { get; set; } = string.Empty;

        // Box
        public static double BoxWidth { get; set; } = 0.0;
        public static double BoxDepth { get; set; } = 0.0;
        public static double BoxHeight { get; set; } = 0.0;
        public static double BoxVolume { get; set; } = 0.0;

        public static int Cost { get; set; } = 0;

        public static void ClearAll()
        {
            SenderPhoneNo = string.Empty;
            SenderName = string.Empty;
            //SenderTelePhoneNo = string.Empty;
            SenderAddress = string.Empty;
            SenderAddress2 = string.Empty;
            SenderAddress3 = string.Empty;

            ReceiverPhoneNo = string.Empty;
            ReceiverName = string.Empty;
            //ReceiverTelePhoneNo = string.Empty;
            ReceiverAddress = string.Empty;
            ReceiverAddress2 = string.Empty;
            ReceiverAddress3 = string.Empty;

            ProductCategory = string.Empty;
            ProductPriceInput = string.Empty;
            RequestInputField = string.Empty;

            BoxWidth = 0.0;
            BoxDepth = 0.0;
            BoxHeight = 0.0;
            BoxVolume = 0.0;
        }
    }
}
