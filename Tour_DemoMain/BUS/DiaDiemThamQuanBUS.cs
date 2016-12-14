using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using EF;

namespace BUS
{
    public class DiaDiemThamQuanBUS
    {
        DiaDiemThamQuanDAO dd = new DiaDiemThamQuanDAO();
        public List<DiaDiem> LoadDD(int matour)
        {
            List<DiaDiemThamQuan> DSDD = dd.LoadDDTQ(matour);
            List<DiaDiem> ddd = new List<DiaDiem>();
            foreach (DiaDiemThamQuan d in DSDD)
            {
                ddd.Add(dd.LayDiaDiem(d.MaDiaDiem));
            }
            return ddd;
        }
        public List<DiaDiemHienThi> LoadDDHT(int matour)
        {
            List<DiaDiemHienThi> ddht = new List<DiaDiemHienThi>();
            List<DiaDiem> ddd = LoadDD(matour);
            int h = 0;
            foreach (DiaDiem s in ddd)
            {
                DiaDiemHienThi dht = new DiaDiemHienThi();
                dht.MaDiaDiem = s.MaDiaDiem;
                dht.TenDiaDiem = s.TenDiaDiem;
                dht.TinhQuocGia = (dd.LayTenTinh((int)s.Tinh_QuocGia)).TenTinh_QuocGia;
                dht.MoTa = s.MoTa;
                h++;
                dht.ThuTu =h;
                ddht.Add(dht);
            }
            return ddht;
        }
        public void SuaDiaDiemThamQuan(List<DiaDiemHienThi> da, int matour)
        {
            foreach (DiaDiemHienThi a in da)
            {
                DiaDiemThamQuan ds = new DiaDiemThamQuan();
                ds.MaDiaDiem = a.MaDiaDiem;
                ds.MaTour = matour;
                ds.Chitiet = a.ThuTu.ToString();
                dd.SuaDiaDiem(ds);
            }
        }
        public void ThemDiaDiem(DiaDiemThamQuan ddd)
        {
            dd.ThemDiaDiem(ddd);
        }
    }
}
