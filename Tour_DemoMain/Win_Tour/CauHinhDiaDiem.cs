using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using EF;

namespace Win_Tour
{
    public partial class CauHinhDiaDiem : Form
    {
        public CauHinhDiaDiem()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        DiaDiemThamQuanBUS dd = new DiaDiemThamQuanBUS();
        List<DiaDiemHienThi> ddht = new List<DiaDiemHienThi>();
        public int matour;
        private void CauHinhDiaDiem_Load(object sender, EventArgs e)
        {
            ddht = dd.LoadDDHT(matour);
            HienThiDiaDiem();
        }
        private void HienThiDiaDiem()
        {
            gridControl1.DataSource = new BindingList<DiaDiemHienThi>(ddht);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            ddht.Where(x => x.ThuTu == 1).First().ThuTu = tp1.ThuTu;
            ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = 1;
            HienThiDiaDiem();
        }
        int madd;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            madd = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn4).ToString());
            label3.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1).ToString();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            DiaDiemHienThi tp2 = ddht.OrderByDescending(x => x.ThuTu).First();
            int temp = tp2.ThuTu;
            ddht.Where(x => x.ThuTu == tp2.ThuTu).First().ThuTu = tp1.ThuTu;
            ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = temp;
            HienThiDiaDiem();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            if (tp1.ThuTu > 1)
            {
                int temp = tp1.ThuTu;
                ddht.Where(x => x.ThuTu == temp - 1).First().ThuTu = temp;
                ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = temp-1;
                HienThiDiaDiem();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            DiaDiemHienThi tp2 = ddht.OrderByDescending(x => x.ThuTu).First();
            if (tp1.ThuTu <tp2.ThuTu)
            {
                int temp = tp1.ThuTu;
                ddht.Where(x => x.ThuTu == temp + 1).First().ThuTu = temp;
                ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = temp + 1;
                HienThiDiaDiem();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dd.SuaDiaDiemThamQuan(ddht, matour);
            MessageBox.Show("Cập nhật thành công!");
            this.Dispose();
        }
    }
}
