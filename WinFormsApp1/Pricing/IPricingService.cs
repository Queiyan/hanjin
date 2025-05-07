namespace WinFormsApp1.Pricing
{
    public interface IPricingService
    {
        int CalculateCost(double height, double width, double depth);
        string GetBoxType(double totalSize);
        bool ValidateSize(double totalSize);
    }
} 