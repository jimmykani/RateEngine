using System;
using System.Collections.Generic;
using System.Text;

namespace RateEngine.Core.Model
{
    public class SpecialRate
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double TotalPrice { get; set; }
        public Duration Entry { get; set; }
        public Duration Exit { get; set; }
        public int MaxDays { get; set; }
    }
}
