using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF;
using PagedList;
using Web_Tour.Models;
using System.Globalization;
using System.Net;

namespace Web_Tour.Controllers
{
    public class TouristGroupController : Controller
    {
        //TourDuLichModel db = new TourDuLichModel();
        TourDuLichEntities db = new TourDuLichEntities();
        // GET: TouristGroup

        public static DateTime GetNistTime()
        {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            var response = myHttpWebRequest.GetResponse();
            string todaysDates = response.Headers["date"];
            DateTime dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
            return dateTime;
        }

        public void Load(string SearchString = "", int page = 1, int pagesize = 10, Boolean haha = false)
        {
            DateTime gg = GetNistTime();
            IQueryable<DoanDuLich> list = db.DoanDuLiches;
            if (!string.IsNullOrEmpty(SearchString))
            {
                if (haha == false)
                {
                    list = list.Where(x => x.TenDoan.Contains(SearchString) && x.NgayKetThuc > gg);
                }
                else if (haha == true)
                {
                    list = list.Where(x => x.TenDoan.Contains(SearchString));
                }
            }
            else
            {
                if (haha == false)
                {
                    list = list.Where(x => x.NgayKetThuc > gg);
                }

                else if (haha == true)
                {
                    list = list;
                }

            }
            ViewBag.Tour = list.OrderBy(x => x.MaDoan).Select(s => new ReportInfo
            {
                MaTour = s.MaTour,
                TenTour = s.Tour.TenTour,
                MaDoan = s.MaDoan,
                TenDoan = s.TenDoan,
                NgayKhoiHanh = s.NgayKhoiHanh,
                NgayKetThuc = s.NgayKetThuc,
                TinhTrang = s.TinhTrang,
                Gia = s.GiaTour.Gia


            }).ToPagedList(page, pagesize);


        }

        public ActionResult Index(string SearchString = "", int page = 1, int pagesize = 10, Boolean haha = false)
        {
            if (ModelState.IsValid)
            {
                Load(SearchString, page, pagesize, haha);
                ViewBag.SearchString = SearchString;
                ViewBag.d = GetNistTime();
                return View();
                //ViewBag.Group = db.DoanDuLiches.ToList();
                //ViewBag.Group = db.DoanDuLiches.Select(s => new ReportInfo
                //{
                //    MaTour = s.MaTour,
                //    TenTour = s.Tour.TenTour,
                //    MaDoan = s.MaDoan,
                //    TenDoan = s.TenDoan,
                //    NgayKhoiHanh = s.NgayKhoiHanh,
                //    NgayKetThuc = s.NgayKetThuc,
                //    MoTa = s.MoTa,
                //    TinhTrang = s.TinhTrang
                //}).ToList();
            }
            return View();

        }

        [HttpGet]
        public ActionResult LoadTour(int id)
        {
            var model = db.Tours.Find(id);
            if (ModelState.IsValid)
            {
                ViewBag.Mota = model.MoTa;
                return View(model);
            }

            return View(model);
        }

        public void SetViewBag(long? selectedId = null)
        {

            var Name = db.GiaTours.Where(x => x.Tour.TinhTrang == true).Select(y => new loadtourcombobox
            {
                MaTour = y.Tour.MaTour,
                TenTour = y.Tour.TenTour
            }).Distinct().ToList();


            ViewBag.MaTour = new SelectList(Name, "MaTour", "TenTour", selectedId);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            SetViewBag();
            return View();
        }

        public int LayMaGia(DoanDuLich t)
        {
            var gt = db.GiaTours.Where(x => x.MaTour == t.MaTour && x.NgayApDung <= t.NgayKhoiHanh).OrderByDescending(x => x.NgayApDung).First();
            return (int)gt.MaGia;
        }

        [HttpPost]
        public ActionResult Insert(DoanDuLich model, int MaTour)
        {
            DateTime gg = GetNistTime();

            if (model.NgayKhoiHanh > gg)
            {
                var tour = db.Tours.Find(MaTour);
                var dem = tour.SoDem;
                var ngay = tour.SoNgay;


                int gia = LayMaGia(model);

                if (dem > ngay)
                {
                    model.NgayKetThuc = model.NgayKhoiHanh.AddDays(Convert.ToDouble(dem));
                }
                else if (dem < ngay)
                {
                    model.NgayKetThuc = model.NgayKhoiHanh.AddDays(Convert.ToDouble(ngay));
                }

                else if (dem == ngay)
                {
                    model.NgayKetThuc = model.NgayKhoiHanh.AddDays(Convert.ToDouble(ngay));
                }

                model.MaGia = gia;

                model.TinhTrang = 1;
                db.DoanDuLiches.Add(model);
                db.SaveChanges();
                Load();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Insert");
            }

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            SetViewBag();
            var model = db.DoanDuLiches.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(DoanDuLich model, int MaTour/*, Boolean Status*/)
        {
            try
            {

                DateTime gg = GetNistTime();

                if (gg < model.NgayKhoiHanh)
                {
                    var tour = db.Tours.Find(MaTour);
                    int gia = LayMaGia(model);
                    var dem = tour.SoDem;
                    var ngay = tour.SoNgay;
                    model.TinhTrang = model.TinhTrang;
                    model.MaGia = gia;
                    if (dem > ngay)
                    {
                        model.NgayKetThuc = model.NgayKhoiHanh.AddDays(Convert.ToDouble(dem));
                    }
                    else if (dem < ngay)
                    {
                        model.NgayKetThuc = model.NgayKhoiHanh.AddDays(Convert.ToDouble(ngay));
                    }
                    else if (dem == ngay)
                    {
                        model.NgayKetThuc = model.NgayKhoiHanh.AddDays(Convert.ToDouble(ngay));
                    }

                    //if (Status == true)
                    //{
                    //    model.TinhTrang = 3;
                    //    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    //    db.SaveChanges();
                    //    Load();
                    //    return RedirectToAction("Index");
                    //}
                    //else
                    //{


                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    //}
                    //model.MaGia = gia;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Update/" + model.MaDoan, "TouristGroup");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public int ChangeStatus(int id)
        {
           
            var user = db.DoanDuLiches.Find(id);
            if (user.TinhTrang == 1)
            {
                user.TinhTrang = 2;
            }
            else if (user.TinhTrang == 2)
            {
                user.TinhTrang = 1;
            }
            db.SaveChanges();
            return user.TinhTrang;
        }

        public ActionResult UpdateStatus(int id, DateTime rp)
        {
            DateTime gg = GetNistTime();
            if (gg < rp)
            {
                var result = ChangeStatus(id);
                return Json(new
                {
                    status = result
                });
            }
            return RedirectToAction("Index");
        }

        public ActionResult SearchCustomer(int Id, String date1, String date2)
        {

            //var model = db.DoanDuLiches.OrderBy(x=>x.MaDoan).Where(x=>x.MaDoan==Id).Select(s => new CusInfo
            //{
            //    MaKhachhang = s.KhachTheoDoans.Select(g => new CusInfoA {
            //        MaKhachHang=g.KhachHang.MaKhachHang,
            //        TenKhachHang=g.KhachHang.TenKhachHang,
            //        SDT=g.KhachHang.SDT,
            //        GioiTinh=g.KhachHang.GioiTinh,
            //        DiaChi=g.KhachHang.DiaChi,
            //        PassportNumber=g.KhachHang.PassportNumber,
            //        TinhTrang=g.KhachHang.TinhTrang
            //    })
            //}).ToList();
            SetViewBagCus();
            ViewBag.madoan = Id;
            var model = db.KhachTheoDoans.OrderBy(x => x.MaDoan).Where(x => x.MaDoan == Id).Select(g => new CusInfoA
            {
                MaKhachHang = g.KhachHang.MaKhachHang,
                TenKhachHang = g.KhachHang.TenKhachHang,
                SDT = g.KhachHang.SDT,
                GioiTinh = g.KhachHang.GioiTinh,
                DiaChi = g.KhachHang.DiaChi,
                PassportNumber = g.KhachHang.PassportNumber,
                TinhTrang = g.KhachHang.TinhTrang,
                Chitiet = g.Chitiet,
                checktinhtrang = g.DoanDuLich.TinhTrang,
                MaDoan = g.MaDoan
            }).ToList();

            ViewBag.KH = date1;
            ViewBag.KT = date2;
            ViewBag.XET = GetNistTime();

            var check = db.DoanDuLiches.Find(Id);
            ViewBag.checkstatus = check.TinhTrang;

            return View(model);

        }

        public void SetViewBagCus(long? selectedId = null)
        {
            var Name = db.KhachHangs;
            ViewBag.MaKhachHang = new SelectList(Name, "MaKhachHang", "TenKhachHang", selectedId);
        }

        [HttpPost]
        public ActionResult JSonSearch(String Keyword = "")
        {
            int Key = Int32.Parse(Keyword);
            var model = db.KhachHangs
                .Where(p => p.MaKhachHang == Key)
                .Select(p => new { p.MaKhachHang, p.TenKhachHang, p.SDT, p.GioiTinh, p.DiaChi, p.PassportNumber });
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SearchDate(String datasearch = "")
        {
            int Key = Int32.Parse(datasearch);
            var model = db.KhachTheoDoans
                .Where(p => p.KhachHang.MaKhachHang == Key)
                .Select(g => new CheckDate
                {
                    MaKhachHang = g.KhachHang.MaKhachHang,
                    KhoiHanh = g.DoanDuLich.NgayKhoiHanh,
                    KetThuc = g.DoanDuLich.NgayKetThuc
                }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult JSonSearch(String Keyword = "")
        //{
        //    var model = db.KhachTheoDoans
        //        .Where(p => p.KhachHang.TenKhachHang.Contains(Keyword))
        //        .Select(p => new { p.MaKhachHang, p.KhachHang.TenKhachHang, p.KhachHang.SDT, p.KhachHang.GioiTinh, p.KhachHang.DiaChi, p.KhachHang.PassportNumber });
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        public bool ChangeStatusCustom(int id, int dd)
        {
            var user = db.KhachTheoDoans.SingleOrDefault(x => x.MaKhachHang == id && x.MaDoan == dd);
            user.Chitiet = !user.Chitiet;
            db.SaveChanges();
            return user.Chitiet;
        }

        public ActionResult UpdateStatusCustom(int id, int dd)
        {
            var result = ChangeStatusCustom(id, dd);
            return Json(new
            {
                status = result
            });

        }

        public ActionResult Category()
        {
            ViewBag.Cates = new[]{
                "Điện thoại di động",
                "Máy tính xách tay",
                "Máy tính để bàn",
                "Quạt máy",
                "Tivi",
                "Tủ lạnh",
                "Máy ảnh",
                "Nữ trang"
            };

            return PartialView("_Category");
        }

        public ActionResult AddCus(/*KhachTheoDoan model*/)
        {
            var madoan = Convert.ToInt32(Request["madoan"]);
            var makhachhang = Request.Form.GetValues("makhachhang");
            if (makhachhang != null)
            {
                for (int i = 0; i < makhachhang.Length; i++)
                {
                    KhachTheoDoan model = new KhachTheoDoan();
                    model.MaDoan = madoan;
                    model.MaKhachHang = Convert.ToInt32(makhachhang[i]);
                    model.Chitiet = true;
                    db.KhachTheoDoans.Add(model);
                    db.SaveChanges();
                    model = null;
                }
                Load();
                return RedirectToAction("Index");
            }
            return Redirect("http://localhost:53465/TouristGroup/SearchCustomer/" + madoan);
        }
    }
}