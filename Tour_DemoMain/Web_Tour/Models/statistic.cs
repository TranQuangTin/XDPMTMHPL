﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace Web_Tour.Models
{
    public class statistic
    {
        public int MaDoan { get;  set; }
        public int? MaTour { get;  set; }
        public DateTime NgayKetThuc { get;  set; }
        public DateTime NgayKhoiHanh { get;  set; }
        public ICollection<GiaTour> Price { get; internal set; }
        public string TenDoan { get;  set; }
        public string TenTour { get;  set; }
    }
}