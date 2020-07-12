using System;
using System.Collections.Generic;
using System.Text;

namespace RateEngine.Core.Model
{
    public class Duration
    {
        public TimeSpan Startime { get; set; }
        public TimeSpan EndTime { get; set; }
        public IEnumerable<string> Days { get; set;}
    }
}
