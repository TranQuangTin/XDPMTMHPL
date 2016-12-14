using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;

namespace DAO
{
    public class DiaDiemDAO
    {
        TourDuLichEntities db = new TourDuLichEntities();
        public List<DiaDiem> LoadDD()
        {
            return db.DiaDiems.Where(x => x.TinhTrang == true).ToList();
        }
        public Tinh_QuocGia LayTenTinh(int matinh)
        {
            return db.Tinh_QuocGia.Where(x => x.MaTinh_QuocGia == matinh).First();
        }
        public Tour LoadTour(int matour)
        {
            return db.Tours.Where(x => x.MaTour == matour).First();
        }
        public List<DiaDiemThamQuan> LoadDDTQ(int madd)
        {
            return db.DiaDiemThamQuans.Where(x => x.MaDiaDiem == madd).ToList();
        }
        
    }
}
