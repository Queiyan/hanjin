using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class CalculationResult
    {
        public double Width_cm { get; set; }
        public double Length_cm { get; set; }
        public object Height_cm { get; set; } // string 또는 double일 수 있음
        public string Error { get; set; }
    }
}
