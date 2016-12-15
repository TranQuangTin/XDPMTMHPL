using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Tour.Models
{
    public class StatisticOrderCount
    {
        public double? Avg { get;  set; }

        public int? Count { get;  set; }
        public int? Max { get; set; }
        public int? Min { get;  set; }
        public int? Sum { get;  set; }
    }
}