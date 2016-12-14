using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EF;
using BUS;
namespace Win_Tour
{
    public partial class UCGia : UserControl
    {
        public UCGia()
        {
            InitializeComponent();
        }
        GiaTourBUS gb = new GiaTourBUS();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienThi();
        }
        public void HienThi()
        {
            //try
            //{
            //    gridControl1.DataSource = new BindingList<GiaTour>(gb.LoadDS(int.Parse(comboBox1.SelectedValue.ToString())));
            //}
            //catch { }
        }

        private void UCGia_Load(object sender, EventArgs e)
        {
            if (TourBUS.DSTour.Count <= 0) TourBUS.LoadTour();
            comboBox1.DataSource = new BindingList<Tour>(TourBUS.DSTour);
            comboBox1.ValueMember = "MaTour";
            comboBox1.DisplayMember = "TenTour";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int gia = int.Parse(textBox1.Text);
            //    if (gia > 0)
            //    {
            //        GiaTour gt = new GiaTour();
            //        gt.Gia = gia;
            //        gt.NgayApDung = (DateTime)dateEdit1.EditValue;
            //        gt.MaTour = int.Parse(comboBox1.SelectedValue.ToString());
            //        try
            //        {
            //            GiaTourBUS gtb = new GiaTourBUS();
            //            gtb.ThemGiaTour(gt); MessageBox.Show("Thêm Thành công!");
            //        }
            //        catch { MessageBox.Show("Thêm Thất Bại!"); }
            //    }
            //    else MessageBox.Show("Giá phải lớn hơn 0.");
            //}
            //catch
            //{
            //    MessageBox.Show("Giá phải là số.");
            //}
        }

        private void dateEdit1_DateTimeChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DateTime dt = new DateTime();
            //    dt = (DateTime)dateEdit1.EditValue;
            //    DateTime dtn = DateTime.Now;
            //    if (dt.Year > dtn.Year || (dt.Year == dtn.Year && dt.Month > dtn.Month) || (dt.Year == dtn.Year && dt.Month == dtn.Month && dt.Day >= dtn.Day))
            //    {
            //        int t = LayGia((DateTime)dateEdit1.EditValue);
            //        if (t == 0) { textBox1.Text = "Chưa có giá."; simpleButton1.Enabled = true; }
            //        else
            //        {
            //            label6.Text = t.ToString();
            //            if (KiemTraNgay(dt) == false) simpleButton1.Enabled = false; else simpleButton1.Enabled = true;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không sửa giá của ngày hôm qua ú u ú ú...");
            //        dateEdit1.EditValue = DateTime.Now;
            //    }
            //}
            //catch { }
        }
        //private bool KiemTraNgay(DateTime dt)
        //{
        //    try
        //    {
        //        GiaTour ss = gb.LoadDS(int.Parse(comboBox1.SelectedValue.ToString())).Where(x => x.NgayApDung == dt).First();
        //        if (ss == null) return true; else return false;
        //    }
        //    catch { return true; }
        //}
        //private int LayGia(DateTime ngay)
        //{
        //    try
        //    {
        //        GiaTour ss = gb.LoadDS(int.Parse(comboBox1.SelectedValue.ToString())).Where(x => x.NgayApDung < ngay).OrderByDescending(x => x.NgayApDung).First();
        //        return (int)ss.Gia;
        //    }
        //    catch { return 0; }
        //}
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            label6.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn3).ToString();
            textBox2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn2).ToString();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    int i = int.Parse(textBox2.Text);
            //    if (i > 0)
            //    {
            //        if (gb.KiemTraGia(int.Parse(label6.Text)) == true)
            //        {
            //            GiaTour gt = new GiaTour();
            //            gt.Gia = i;
            //            gt.MaGia = int.Parse(label6.Text);
            //            try
            //            {
            //                gb.SuaGia(gt); MessageBox.Show("Sửa Thành Công!"); HienThi();
            //            }
            //            catch { MessageBox.Show("Sửa Thất Bại!"); }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Không thể sửa giá khi có đoàn khách đã đi giá này!");
            //        }
            //    }
            //    else MessageBox.Show("Giá phải lớn hơn 0!");
            //}
            //catch { MessageBox.Show("Giá phải là số!"); }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //if (gb.KiemTraGia(int.Parse(label6.Text)) == true)
            //{
            //    gb.XoaGia(int.Parse(label6.Text)); MessageBox.Show("Xóa Thành Công!"); HienThi();
            //}
            //else MessageBox.Show("Không thể xóa giá khi tour đã đi!");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
