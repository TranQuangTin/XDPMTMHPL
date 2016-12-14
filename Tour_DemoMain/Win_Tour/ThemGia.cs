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
    public partial class ThemGia : Form
    {
        public ThemGia()
        {
            InitializeComponent();
        }
        public GiaTour gt = new GiaTour();
        public string tentour;
        private GiaTourBUS gtb = new GiaTourBUS();
        private List<GiaTour> DSGiaTuor;
        public void LoadGiaTour(int matour)
        {
            DSGiaTuor= gtb.LoadGiaTour(matour);
        }

        private void ThemGia_Load(object sender, EventArgs e)
        {
            label5.Text = tentour;
            LoadGiaTour((int)gt.MaTour);
            dateEdit1.Properties.Mask.EditMask = "dd-MM-yyyy";
        }
        private int LayGia(DateTime ngay)
        {
            try
            {
                GiaTour ss = DSGiaTuor.Where(x => x.NgayApDung < ngay).OrderByDescending(x => x.NgayApDung).First();
                return (int)ss.Gia;
            }
            catch { return 0; }
        }
        private bool KiemTraNgay(DateTime dt)
        {
            try
            {
                GiaTour ss = DSGiaTuor.Where(x => x.NgayApDung == dt).First();
                if (ss == null) return true; else return false;
            }
            catch { return true; }
        }
        private void dateEdit1_DateTimeChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = new DateTime();
                dt = (DateTime)dateEdit1.EditValue;
                DateTime dtn = DateTime.Now;
                if (dt.Year > dtn.Year || (dt.Year == dtn.Year && dt.Month > dtn.Month) || (dt.Year == dtn.Year && dt.Month == dtn.Month && dt.Day >= dtn.Day))
                {
                    int t = LayGia((DateTime)dateEdit1.EditValue);
                    if (t == 0) { label6.Text = "Chưa có giá."; simpleButton1.Enabled = true; }
                    else
                    {
                        label6.Text = t.ToString();
                        if (KiemTraNgay(dt) == false) simpleButton1.Enabled = false; else simpleButton1.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Không sửa giá của ngày hôm qua ú u ú ú...");
                    dateEdit1.EditValue = DateTime.Now;
                }
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int gia = int.Parse(textBox4.Text);
                if (gia > 0)
                {
                    gt.Gia = gia;
                    gt.NgayApDung = (DateTime)dateEdit1.EditValue;
                    try
                    {
                        gtb.ThemGiaTour(gt); MessageBox.Show("Thêm Thành công!"); this.Dispose();
                    }
                    catch { MessageBox.Show("Thêm Thất Bại!"); }
                }
                else MessageBox.Show("Giá phải lớn hơn 0.");
            }
            catch
            {
                MessageBox.Show("Giá phải là số.");
            }
        }
    }
}
