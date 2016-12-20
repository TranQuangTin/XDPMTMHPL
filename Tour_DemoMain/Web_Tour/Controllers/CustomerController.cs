using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Tour.Controllers
{
    public class CustomerController : Controller
    {
        TourDuLichEntities db = new TourDuLichEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var model = db.KhachHangs.OrderByDescending(x=>x.MaKhachHang).ToList();
            return View(model);
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(KhachHang khachhang, String caree)
        {
            khachhang.GioiTinh = caree;
            khachhang.TinhTrang = true;
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
            var model = db.KhachHangs.OrderByDescending(x => x.MaKhachHang).ToList();
            return RedirectToAction("Index",model);
        }
    }
}