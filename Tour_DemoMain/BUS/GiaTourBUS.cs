using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using EF;

namespace BUS
{
    public class GiaTourBUS
    {
        GiaTourDAO gd = new GiaTourDAO();
        public List<GiaTour> LoadGiaTour(int matour)
        {
            return gd.LoadGiaTour(matour);
        }
        public void ThemGiaTour(GiaTour gt)
        {
            gd.ThemGiaTour(gt);
        }
        public List<GiaTour> LoadDS(int matour)
        {
            return gd.LoadGiaTour(matour);
        }

        public bool KiemTraNgay(DateTime dt, int matour)
        {
            List<GiaTour> ss = LoadDS(matour) ;
            foreach (GiaTour gt in ss)
            {
                DateTime dt1 = (DateTime)gt.NgayApDung;
                if (dt.Year == dt1.Year && dt.Month == dt1.Month && dt.Date == dt1.Date) return false;

            } return true;
        }
        public int LayGia(int matour, DateTime ngay)
        {
            try
            {
                GiaTour ss = LoadDS(matour).Where(x => x.NgayApDung < ngay).OrderByDescending(x => x.NgayApDung).First();
                return (int)ss.Gia;
            }
            catch { return 0; }
        }
        public bool KiemTraGiaKhiSua(int magia, DateTime dt)
        {
            return gd.KiemTraGiaKhiSua(magia,dt);
        }
        public bool KiemTraGiaKhiXoa(int magia)
        {
            return gd.KiemTraGiaKhiXoa(magia);
        }
        public void SuaGia(GiaTour gt)
        {
            gd.SuaGia(gt);
        }
        public void XoaGia(int magia)
        {
            gd.XoaGia(magia);
        }
    }
}
