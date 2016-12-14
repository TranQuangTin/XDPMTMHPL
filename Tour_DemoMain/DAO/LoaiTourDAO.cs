using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class LoaiTourDAO
    {
        TourDuLichEntities tdl = new TourDuLichEntities();
        public List<LoaiTour> LoadCbo()
        {
            return tdl.LoaiTours.ToList();
        }
    }
}
