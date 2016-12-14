using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class DiaDiemThamQuanDAO
    {
        TourDuLichEntities db = new TourDuLichEntities();
        public List<DiaDiemThamQuan> LoadDDTQ(int matour)
        {
            return db.DiaDiemThamQuans.Where(x => x.MaTour == matour).OrderBy(x=>x.Chitiet).ToList();
        }
        public DiaDiem LayDiaDiem(int madiadiem)
        {
            return db.DiaDiems.Where(x => x.MaDiaDiem == madiadiem).First();
        }
        public Tinh_QuocGia LayTenTinh(int matinh)
        {
            return db.Tinh_QuocGia.Where(x => x.MaTinh_QuocGia == matinh).First();
        }
        public void SuaDiaDiem(DiaDiemThamQuan dd)
        {
            DiaDiemThamQuan da = db.DiaDiemThamQuans.Where(x => x.MaTour == dd.MaTour).Where(x => x.MaDiaDiem == dd.MaDiaDiem).First();
            if (da != null)
            {
                da.Chitiet = dd.Chitiet;
            }
            db.SaveChanges();
        }
        public void ThemDiaDiem(DiaDiemThamQuan dd)
        {
            db.DiaDiemThamQuans.Add(dd);
            db.SaveChanges();
        }
    }
}
