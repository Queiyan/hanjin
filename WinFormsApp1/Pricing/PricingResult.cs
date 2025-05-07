namespace WinFormsApp1.Pricing
{
    public class PricingResult
    {
        public double TotalSize { get; set; }
        public int Cost { get; set; }
        public string BoxType { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }

        public PricingResult(double totalSize, int cost, string boxType, bool isValid, string errorMessage = null)
        {
            TotalSize = totalSize;
            Cost = cost;
            BoxType = boxType;
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }
} 