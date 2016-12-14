using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using DAO;

namespace BUS
{
    public class TourBUS
    {
        TourDAO td = new TourDAO();
        public static List<Tour> DSTour = new List<Tour>();
        public static void LoadTour()
        {
            if(DSTour.Count==0)
            DSTour = TourDAO.LoadTour();
        }
        public static void LoadTour1()
        {
                DSTour = TourDAO.LoadTour();
        }
        public void SuaTour(Tour tt)
        {
            td.SuaTour(tt);
        }
        public void XoaTour(int matour)
        {
            td.XoaTour(matour);
        }
        public void ThemTour(Tour tr)
        {
            td.ThemTour(tr);
        }
    }
}
