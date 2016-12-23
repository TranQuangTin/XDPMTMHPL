using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class TourDAO
    {
        public static List<Tour> LoadTour()
        {
            return tdl.Tours.Where(x => x.TinhTrang == true).ToList();
            //câu này làm chậm
            
        }
        static TourDuLichEntities tdl = new TourDuLichEntities();
        public int LayMaGia(DoanDuLich t)
        {
            GiaTour gt = tdl.GiaTours.Where(x => x.MaTour == t.MaTour && x.NgayApDung<=t.NgayKhoiHanh).OrderByDescending(x => x.NgayApDung).First();
            return (int)gt.MaGia;
        }
        public void SuaTour(Tour tt)
        {
            //Tour ss = tdl.Tours.Where(s => s.MaTour == tt.MaTour).FirstOrDefault<Tour>();
            Tour ss = tdl.Tours.Find(tt.MaTour);
                if (ss != null)
                {
                    ss.TenTour = tt.TenTour;
                    ss.TinhTrang = tt.TinhTrang;
                    ss.SoNgay = tt.SoNgay;
                    ss.SoDem = tt.SoDem;
                    ss.Mua = tt.Mua; ss.MoTa = tt.MoTa; ss.LoaiTour = tt.LoaiTour;
                }
                tdl.SaveChanges();
        }
        public void XoaTour(int matour)
        {
            List<GiaTour> gt = tdl.GiaTours.Where(x => x.MaTour == matour).ToList();
            foreach (GiaTour g in gt)
            {
                tdl.GiaTours.Remove(g);
                tdl.SaveChanges();
            }
            Tour tt= tdl.Tours.Find(matour);
            tdl.Tours.Remove(tt);
            tdl.SaveChanges();
        }
        public void VHHTour(int matour)
        {
            Tour tt = tdl.Tours.Find(matour);
            tt.TinhTrang = false;
            tdl.SaveChanges();
        }
        public int KiemTraXoa(int matour)
        {
           List<DoanDuLich> dd = tdl.DoanDuLiches.Where(x => x.MaTour == matour).ToList();
           return dd.Count;
        }
        public void ThemTour(Tour tr)
        {
            tdl.Tours.Add(tr);
            tdl.SaveChanges();
        }
    }
}
