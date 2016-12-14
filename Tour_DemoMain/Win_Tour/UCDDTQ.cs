using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using EF;

namespace Win_Tour
{
    public partial class UCDDTQ : UserControl
    {
        public UCDDTQ()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UCDDTQ_Load(object sender, EventArgs e)
        {
            //LoadcboDiaDiem();
            //LoadcboTour();
            //try
            //{
            //    ddht = dd.LoadDDHT(int.Parse(comboBox1.SelectedValue.ToString()));
            //    HienThiDiaDiem();
            //}
            //catch { }
        }
        public void LoadcboTour()
        {
            //if (TourBUS.DSTour.Count <= 0) TourBUS.LoadTour();
            //comboBox1.DataSource = new BindingList<Tour>(TourBUS.DSTour);
            //comboBox1.ValueMember = "MaTour";
            //comboBox1.DisplayMember = "TenTour";
        }
        public void LoadcboDiaDiem()
        {
        //    DiaDiemBUS.LoadDD();
        //    comboBox2.DataSource = new BindingList<DiaDiem>(DiaDiemBUS.DSDD);
        //    comboBox2.ValueMember = "MaDiaDiem";
        //    comboBox2.DisplayMember = "TenDiaDiem";
        }

        DiaDiemThamQuanBUS dd = new DiaDiemThamQuanBUS();
        List<DiaDiemHienThi> ddht = new List<DiaDiemHienThi>();
        private void HienThiDiaDiem()
        {
            //try
            //{
            //    gridControl1.DataSource = new BindingList<DiaDiemHienThi>(ddht);
            //}
            //catch { }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ddht = dd.LoadDDHT(int.Parse(comboBox1.SelectedValue.ToString()));
                //HienThiDiaDiem();
            }
            catch { }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            ddht.Where(x => x.ThuTu == 1).First().ThuTu = tp1.ThuTu;
            ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = 1;
            HienThiDiaDiem();
        }
        int madd = 0;

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            madd = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1).ToString());
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
                ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = temp - 1;
                HienThiDiaDiem();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            DiaDiemHienThi tp2 = ddht.OrderByDescending(x => x.ThuTu).First();
            if (tp1.ThuTu < tp2.ThuTu)
            {
                int temp = tp1.ThuTu;
                ddht.Where(x => x.ThuTu == temp + 1).First().ThuTu = temp;
                ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = temp + 1;
                HienThiDiaDiem();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                dd.SuaDiaDiemThamQuan(ddht, int.Parse(comboBox1.SelectedValue.ToString()));
                MessageBox.Show("Cập nhật thành công!");
            }
            catch { MessageBox.Show("Đã có lỗi!"); }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
                DiaDiemHienThi tt=new DiaDiemHienThi();
                int i = int.Parse(comboBox2.SelectedValue.ToString());
                try
                {
                    tt = ddht.Where(x => x.MaDiaDiem == i).First();
                }
                catch { }
                    DiaDiemThamQuan ddtq = new DiaDiemThamQuan();
                    if (tt.MaDiaDiem>0)
                    {
                        MessageBox.Show("Địa điểm đã có trong tour!");
                    }
                    else
                    {
                        ddtq.MaTour = int.Parse(comboBox1.SelectedValue.ToString());
                        ddtq.MaDiaDiem = i;
                        try
                        {
                            ddtq.Chitiet = (ddht.OrderByDescending(x => x.ThuTu).First().ThuTu + 1).ToString();
                        }
                        catch { ddtq.Chitiet = "1"; }
                        dd.ThemDiaDiem(ddtq);
                        comboBox1_SelectedIndexChanged(sender, e);
                    }
            }
    }
}
