namespace WinFormsApp1.Pricing
{
    public class PricingService : IPricingService
    {
        private const double MAX_SIZE = 160.0; // 최대 허용 크기 (cm)
        private const double SIZE_B = 100.0;   // B타입 최대 크기
        private const double SIZE_C = 120.0;   // C타입 최대 크기
        private const double SIZE_D = 140.0;   // D타입 최대 크기
        private const double SIZE_E = 160.0;   // E타입 최대 크기

        private const int COST_B = 3000;       // B타입 가격
        private const int COST_C = 3500;       // C타입 가격
        private const int COST_D = 4500;       // D타입 가격
        private const int COST_E = 5500;       // E타입 가격

        public int CalculateCost(double height, double width, double depth)
        {
            double totalSize = height + width + depth;
            return GetCostBySize(totalSize);
        }

        public string GetBoxType(double totalSize)
        {
            if (totalSize <= SIZE_B) return "B";
            if (totalSize <= SIZE_C) return "C";
            if (totalSize <= SIZE_D) return "D";
            if (totalSize <= SIZE_E) return "E";
            return "X"; // 초과 크기
        }

        public bool ValidateSize(double totalSize)
        {
            return totalSize <= MAX_SIZE;
        }

        private int GetCostBySize(double totalSize)
        {
            if (totalSize <= SIZE_B) return COST_B;
            if (totalSize <= SIZE_C) return COST_C;
            if (totalSize <= SIZE_D) return COST_D;
            if (totalSize <= SIZE_E) return COST_E;
            return 0; // 초과 크기
        }
    }
} 