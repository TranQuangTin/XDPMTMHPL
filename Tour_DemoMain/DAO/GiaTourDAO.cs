using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class GiaTourDAO
    {
        TourDuLichEntities db = new TourDuLichEntities();

        public List<GiaTour> LoadGiaTour(int matour)
        {
            return db.GiaTours.Where(x => x.MaTour == matour).ToList();
        }
        public bool KiemTraGiaKhiSua(int magia, DateTime dt)
        {
            List<DoanDuLich> ll = db.DoanDuLiches.Where(x => x.MaGia == magia).Where(x => x.NgayKhoiHanh < dt).ToList();
            if (ll.Count == 0) return true; return false;
        }
        public bool KiemTraGiaKhiXoa(int magia)
        {
            List<DoanDuLich> ll = db.DoanDuLiches.Where(x => x.MaGia == magia).ToList();
            if (ll.Count == 0) return true; return false;
        }
        public void ThemGiaTour(GiaTour gt)
        {
            db.GiaTours.Add(gt);
            db.SaveChanges();
        }
        public void SuaGia(GiaTour gt)
        {
            GiaTour gg = db.GiaTours.Find(gt.MaGia);
            if (gg != null) gg.Gia = gt.Gia;
            db.SaveChanges();
        }
        public void XoaGia(int magia)
        {
            GiaTour gt = db.GiaTours.Find(magia);
            db.GiaTours.Remove(gt);
            db.SaveChanges();
        }
    }
}
