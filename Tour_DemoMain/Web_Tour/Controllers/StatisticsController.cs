using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF;
using Web_Tour.Models;

namespace Web_Tour.Controllers
{
    public class StatisticsController : Controller
    {

        TourDuLichEntities db = new TourDuLichEntities();
        // GET: Statistics
        public void SetViewBag(long? selectedId = null)
        {
            var Name = db.KhachHangs;
            ViewBag.MaKhachHang = new SelectList(Name, "MaKhachHang", "TenKhachHang", selectedId);
        }

        [HttpGet]
        public ActionResult Index()
        {
            SetViewBag();

            return View();
        }

        [HttpPost]
        public ActionResult Index(int MaKhachHang, DateTime? date1, DateTime? date2)
        {
            try
            {
                SetViewBag();
                var model = db.KhachTheoDoans
                    .OrderBy(x => x.DoanDuLich.NgayKhoiHanh)
                    //.Where(x=>x.KhachHang.MaKhachHang==MaKhachHang&&x.DoanDuLich.NgayKhoiHanh>=date1&&x.DoanDuLich.NgayKetThuc<=date2)
                    .Where(x => (x.KhachHang.MaKhachHang == MaKhachHang && x.DoanDuLich.NgayKhoiHanh >= date1 && x.DoanDuLich.NgayKhoiHanh <= date2) || (x.KhachHang.MaKhachHang == MaKhachHang && x.DoanDuLich.NgayKetThuc <= date2 && x.DoanDuLich.NgayKetThuc >= date1))
                    .Select(q => new statistic
                    {
                        MaDoan = q.DoanDuLich.MaDoan,
                        TenDoan = q.DoanDuLich.TenDoan,
                        MaTour = q.DoanDuLich.MaTour,
                        GiaDoan=q.DoanDuLich.GiaTour.Gia,
                        NgayKhoiHanh = q.DoanDuLich.NgayKhoiHanh,
                        NgayKetThuc = q.DoanDuLich.NgayKetThuc,
                        TenTour = q.DoanDuLich.Tour.TenTour
                    }).ToList();

               

                //ViewBag.Countdata = db.KhachTheoDoans
                //    .OrderBy(x => x.DoanDuLich.NgayKhoiHanh)
                //    //.Where(x=>x.KhachHang.MaKhachHang==MaKhachHang&&x.DoanDuLich.NgayKhoiHanh>=date1&&x.DoanDuLich.NgayKetThuc<=date2)
                //    .Where(x => ((x.KhachHang.MaKhachHang == MaKhachHang && x.DoanDuLich.NgayKhoiHanh >= date1 && x.DoanDuLich.NgayKhoiHanh <= date2) || (x.KhachHang.MaKhachHang == MaKhachHang && x.DoanDuLich.NgayKetThuc <= date2 && x.DoanDuLich.NgayKetThuc >= date1)))
                //    .GroupBy(x => x.MaKhachHang)
                //   .Select(g => new StatisticOrderCount
                //   {                      
                //       Sum = g.Sum(p => p.DoanDuLich.GiaTour.Gia),
                //       Min = g.Min(p => p.DoanDuLich.GiaTour.Gia),
                //       Max = g.Max(p => p.DoanDuLich.GiaTour.Gia),
                //       Avg = g.Average(p => p.DoanDuLich.GiaTour.Gia)
                //   }).ToList();

                //ViewBag.Gia = db.GiaTours.Where(x=>x.MaTour==(statistic)model.)
                return View("Index", model);
            }
            catch
            {              
                return View("Index");
            }
        }



    }
}