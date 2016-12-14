using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class ThongKeDAO
    {
        TourDuLichEntities db = new TourDuLichEntities();
        public List<DoiTuongThongKe> ThongKe(DateTime dt1, DateTime dt2)
        {
            List<DoiTuongThongKe> kq = new List<DoiTuongThongKe>();
            List<DoanDuLich> DSDDL = db.DoanDuLiches.Where(x => x.NgayKhoiHanh >= dt1 && x.NgayKhoiHanh <= dt2).ToList();
            foreach (DoanDuLich ddl in DSDDL)
            {
                DoiTuongThongKe dt = new DoiTuongThongKe();
                dt.TenDoan = ddl.TenDoan;
                dt.TenTour = ddl.Tour.TenTour;
                dt.NgayKhoiHanh = ddl.NgayKhoiHanh;
                dt.Gia = (int)ddl.GiaTour.Gia;
                dt.SoLuongKhachHang = ddl.KhachTheoDoans.Count;
                dt.TongTien = dt.Gia * dt.SoLuongKhachHang;
                kq.Add(dt);
            }
            return kq;
        }
        public List<DoiTuongThongKe> BieuDo(DateTime dt1, DateTime dt2)
        {
            List<DoiTuongThongKe> kq = new List<DoiTuongThongKe>();
            List<DoanDuLich> DSDDL = db.DoanDuLiches.Where(x => x.NgayKhoiHanh >= dt1 && x.NgayKhoiHanh <= dt2).ToList();

            foreach (DoanDuLich ddl in DSDDL)
            {
                DoiTuongThongKe dt = new DoiTuongThongKe();
                dt.TenTour = ddl.Tour.TenTour;

                dt.Gia = (int)ddl.GiaTour.Gia;
                dt.SoLuongKhachHang = ddl.KhachTheoDoans.Count;
                dt.TongTien = dt.Gia * dt.SoLuongKhachHang;

                kq.Add(dt);
            }
            List<DoiTuongThongKe> kq1 = new List<DoiTuongThongKe>();
            foreach (DoiTuongThongKe dtt in kq)
            {
                DoiTuongThongKe dr=null;
                try
                {
                    dr = kq1.Where(x => x.TenTour == dtt.TenTour).First();
                }
                catch { }
                if (dr == null)
                {
                    DoiTuongThongKe de = new DoiTuongThongKe();
                    de.TenTour = dtt.TenTour;
                    de.TongTien = kq.Where(x => x.TenTour == dtt.TenTour).Sum(x => x.TongTien);
                    kq1.Add(de);
                }
            }

            return kq1;
        }
    }
}
