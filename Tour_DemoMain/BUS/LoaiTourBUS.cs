using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using EF;

namespace BUS
{
    public class LoaiTourBUS
    {
        public List<LoaiTour> DSLoaiTour = new List<LoaiTour>();
        LoaiTourDAO ltd = new LoaiTourDAO();
        public void LoadLoaiTour()
        {
            if (DSLoaiTour.Count == 0)
                DSLoaiTour= ltd.LoadCbo();
        }
    }
}
