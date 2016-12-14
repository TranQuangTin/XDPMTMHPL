using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using EF;

namespace BUS
{
    public class DiaDiemBUS
    {
        static DiaDiemDAO dda = new DiaDiemDAO();
        public static List<DiaDiem> DSDD = new List<DiaDiem>();
        
        public static void LoadDD()
        {
            if(DSDD.Count==0)
            DSDD = dda.LoadDD();
        }
        public List<DiaDiemTemp> LoadDDHT()
        {
            List<DiaDiemTemp> ddt = new List<DiaDiemTemp>();
            foreach (DiaDiem a in DSDD)
            {
                DiaDiemTemp dt = new DiaDiemTemp();
                dt.dd = a;
                try
                {
                    dt.tentp = dda.LayTenTinh((int)a.Tinh_QuocGia).TenTinh_QuocGia;
                }
                catch { }
                ddt.Add(dt);
            }
            return ddt;
        }
        public List<Tour> LoadTour(int madd)
        {
            List<DiaDiemThamQuan> DSDD = dda.LoadDDTQ(madd);
            List<Tour> LT = new List<Tour>();

            foreach (DiaDiemThamQuan a in DSDD)
            {
                LT.Add(dda.LoadTour(a.MaTour));
            }
            return LT;
        }
    }
}
