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
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Localization;

namespace Win_Tour
{
    public partial class Tour1 : UserControl
    {
        public Tour1()
        {
            InitializeComponent();
        }
        private void HienThiTour()
        {
            try
            {
                if (checkBox1.Checked == true)
                    gridControl1.DataSource = new BindingList<Tour>(TourBUS.DSTour);
                else gridControl1.DataSource = new BindingList<Tour>(TourBUS.DSTour.Where(x => x.TinhTrang == true).ToList());
            }
            catch { }
        }

        private void Tour1_Load(object sender, EventArgs e)
        {
            TourBUS.LoadTour();
            HienThiTour();
            LoadCboLoaiTour();
            LoadcboDiaDiem();
            try
            {
                ddht = dd.LoadDDHT(matuor);
                HienThiDiaDiem();
            }
            catch { }
        }
        
        private void LoadCboLoaiTour()
        {
            LoaiTourBUS lt=new LoaiTourBUS();
            if (lt.DSLoaiTour.Count == 0) lt.LoadLoaiTour();
            comboBox1.DataSource = new BindingList<LoaiTour>(lt.DSLoaiTour);
            comboBox1.DisplayMember = "TenLoaiTour";
            comboBox1.ValueMember = "MaLoaiTour";
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            
        }
        Tour t = new Tour();
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TourBUS tb = new TourBUS();
            int matour = int.Parse(textBox1.Text);
            try
            {
                if (tb.KiemTraXoa(matour) == 0)
                {
                    tb.XoaTour(matour);
                    MessageBox.Show("Xóa Thành Công!");
                    Tour s1 = TourBUS.DSTour.Where(i => i.MaTour == matour).First();
                    var index = TourBUS.DSTour.IndexOf(s1);
                    if (index != -1)
                        TourBUS.DSTour.RemoveAt(index);
                    HienThiTour();
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Tour không thể xóa khi đã có đoàn đi. Bạn có muốn vô hiệu hóa tour này?", "Xác nhận", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        tb.VHHTour(matour);
                            MessageBox.Show("Đã vô hiệu hóa. Sẽ không có đoàn khách mới nào đi tour này!");
                            Tour s1 = TourBUS.DSTour.Where(i => i.MaTour == matour).First();
                            var index = TourBUS.DSTour.IndexOf(s1);
                            if (index != -1)
                                TourBUS.DSTour.RemoveAt(index); HienThiTour();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không thể xóa tour này");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ThemGia tg = new ThemGia();
            tg.tentour = textBox2.Text;
            tg.gt.MaTour = int.Parse(textBox1.Text);
            tg.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            CauHinhDiaDiem ch = new CauHinhDiaDiem();
            ch.matour = int.Parse(textBox1.Text);
            ch.Show();
        }

        private void tabNavigationPage1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void tabNavigationPage2_Paint(object sender, PaintEventArgs e)
        {
            //tabNavigationPage2.Controls.Clear();
            //UCDiaDiem dms = new UCDiaDiem();
            //tabNavigationPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            //tabNavigationPage2.Controls.Add(dms);
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            //tabPage1.Controls.Clear();
            //UCGia dms = new UCGia();
            //tabPage1.Dock = System.Windows.Forms.DockStyle.Top;
            //tabPage1.Controls.Add(dms);
        }
        int matuor;
        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            matuor = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1).ToString());
            t = TourBUS.DSTour.Find(x => x.MaTour == matuor);
            textBox1.Text = t.MaTour.ToString();
            textBox2.Text = t.TenTour;
            comboBox1.SelectedValue = t.LoaiTour;
            textBox3.Text = t.SoNgay.ToString();
            //textBox4.Text = t.SoDem.ToString();
            textBox5.Text = t.Mua;
            //checkBox1.Checked = (bool)t.TinhTrang;
            //richTextBox1.Text = t.MoTa;
            //richEditControl1.HtmlText = t.MoTa;
            GiaTourBUS gtb = new GiaTourBUS();
            DSGT = gtb.LoadDS(matuor);
            HienThi();
            ddht = dd.LoadDDHT(matuor);
            HienThiDiaDiem();
            gridView2_FocusedRowChanged(sender, e);
            gridView3_FocusedRowChanged(sender, e);
            TourBUS tb=new TourBUS();
            if(tb.KiemTraTinhTrang(matuor))
            {
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = true;
                simpleButton5.Enabled = true;
                simpleButton3.Enabled = true;
                simpleButton4.Enabled = true;
                simpleButton6.Enabled = true;
                simpleButton12.Enabled = true;
            }
            else
            {
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;
                simpleButton5.Enabled = false;
                simpleButton3.Enabled = false;
                simpleButton4.Enabled = false;
                simpleButton6.Enabled = false;
                simpleButton12.Enabled = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "")
                {
                    Tour tt = new Tour();
                    tt.SoNgay = int.Parse(textBox3.Text);
                    if (tt.SoNgay > 0)
                    {
                        tt.MaTour = int.Parse(textBox1.Text);
                        tt.TenTour = textBox2.Text;
                        tt.LoaiTour = int.Parse(comboBox1.SelectedValue.ToString());
                        tt.SoDem = 0;
                        tt.Mua = textBox5.Text;
                        tt.MoTa = t.MoTa;
                        tt.TinhTrang = true;
                        if (tt.TenTour != t.TenTour || tt.LoaiTour != t.LoaiTour || tt.SoNgay != t.SoNgay ||
                            tt.Mua != t.Mua || tt.TinhTrang != t.TinhTrang)
                        {
                            TourBUS tb = new TourBUS();
                            tb.SuaTour(tt); MessageBox.Show("Sửa Thành Công!");
                            Tour s1 = TourBUS.DSTour.Where(i => i.MaTour == t.MaTour).First();
                            var index = TourBUS.DSTour.IndexOf(s1);
                            if (index != -1)
                                TourBUS.DSTour[index] = t;
                            HienThiTour();
                        }
                        else MessageBox.Show("Không có thông tin nào thay đổi.");
                    }
                    else MessageBox.Show("Số ngày phải lớn hơn 0.");
                }
                else MessageBox.Show("Chưa nhập đầy đủ thông tin.");
            }
            catch { MessageBox.Show("Số Ngày phải là số!"); }
            //////////////////////////////////////////////////////
        }
        UCGia dms=new UCGia();

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (matuor > 0)
                {
                    int gia = int.Parse(textBox6.Text);
                    if (gia > 0)
                    {
                        GiaTour gt = new GiaTour();
                        gt.Gia = gia;
                        gt.NgayApDung = (DateTime)dateEdit1.EditValue;
                        gt.MaTour = matuor;
                        try
                        {
                            gb.ThemGiaTour(gt); MessageBox.Show("Thêm Thành công!"); DSGT.Add(gt); HienThi();
                        }
                        catch { MessageBox.Show("Thêm Thất Bại!"); }
                    }
                    else MessageBox.Show("Giá phải lớn hơn 0.");
                }
            }
            catch
            {
                MessageBox.Show("Giá phải là số.");
            }
        }
        GiaTourBUS gb = new GiaTourBUS();
        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = new DateTime();
                dt = (DateTime)dateEdit1.EditValue;
                DateTime dtn = GetNistTime();
                if (dt.Year > dtn.Year || (dt.Year == dtn.Year && dt.Month > dtn.Month) || (dt.Year == dtn.Year && dt.Month == dtn.Month && dt.Day >= dtn.Day))
                {
                    int t = LayGia((DateTime)dateEdit1.EditValue);
                    if (t == 0) { textBox6.Text = "Chưa có giá."; simpleButton6.Enabled = true; }
                    else
                    {
                        textBox6.Text = t.ToString();
                        if (KiemTraNgay(dt) == false) simpleButton6.Enabled = false; else simpleButton6.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Không sửa giá trong quá khứ!");
                    dateEdit1.EditValue = dtn;
                }
            }
            catch { }
        }
        public static DateTime GetNistTime()
        {
            try
            {
                DateTime dateTime = DateTime.MinValue;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
                    string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                    double milliseconds = Convert.ToInt64(time) / 1000.0;
                    dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
                }

                return dateTime;
            }
            catch { return DateTime.Now; }
        }
        private bool KiemTraNgay(DateTime dt)
        {
            try
            {
                return gb.KiemTraNgay(dt, matuor);
            }
            catch { return true; }
        }
        private int LayGia(DateTime ngay)
        {
            return gb.LayGia(matuor, ngay);
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {

            try
            {
                int i = int.Parse(textBox4.Text);
                if (i > 0)
                {
                    if (gb.KiemTraGiaKhiSua(magia, GetNistTime()) == true)
                    {
                        GiaTour gt = new GiaTour();
                        gt.Gia = i;
                        gt.MaGia = magia;
                        try
                        {
                            gb.SuaGia(gt); MessageBox.Show("Sửa Thành Công!");
                            GiaTour gtt = DSGT.Where(x => x.MaGia == magia).First();
                            gtt.Gia = i;
                            DSGT.RemoveAll(x => x.MaGia == magia);
                            DSGT.Add(gtt);
                            HienThi();
                            // chưa đi thì có quyền sửa giá
                        }
                        catch { MessageBox.Show("Sửa Thất Bại!"); }
                    }
                    else
                    {
                        MessageBox.Show("Không thể sửa giá khi có đoàn khách đã đi giá này!");
                    }
                }
                else MessageBox.Show("Giá phải lớn hơn 0!");
            }
            catch { MessageBox.Show("Giá phải là số!"); }
        }
        List<GiaTour> DSGT = new List<GiaTour>();
        public void HienThi()
        {
            try
            {
                gridControl2.DataSource = null;
                gridControl2.DataSource = new BindingList<GiaTour>(DSGT);
            }
            catch { }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (gb.KiemTraGiaKhiXoa(matuor) == true)
                {
                    gb.XoaGia(magia); MessageBox.Show("Xóa Thành Công!");
                    DSGT.RemoveAll(x => x.MaGia == magia);
                    HienThi();
                }
                else MessageBox.Show("Không thể xóa giá khi đã có tour đi!");
            }
            catch { MessageBox.Show("Không thể xóa giá khi tour đã có tour đi!"); }
        }
        int magia;
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                magia = int.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridColumn7).ToString());
                textBox4.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridColumn9).ToString();
            }
            catch { }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FormSuaMoTa fs = new FormSuaMoTa();
            fs.tr = t;
            fs.ShowDialog();
        }
        public void LoadcboDiaDiem()
        {
            DiaDiemBUS.LoadDD();
            comboBox2.DataSource = new BindingList<DiaDiem>(DiaDiemBUS.DSDD);
            comboBox2.ValueMember = "MaDiaDiem";
            comboBox2.DisplayMember = "TenDiaDiem";
        }
        DiaDiemThamQuanBUS dd = new DiaDiemThamQuanBUS();
        List<DiaDiemHienThi> ddht = new List<DiaDiemHienThi>();
        private void HienThiDiaDiem()
        {
            try
            {
                gridControl3.DataSource = null;
                gridControl3.DataSource = new BindingList<DiaDiemHienThi>(ddht);
            }
            catch { }
        }
        int madd = 0;

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                madd = int.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, gridColumn10).ToString());
            }
            catch { }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tt = new DiaDiemHienThi();
            int i = int.Parse(comboBox2.SelectedValue.ToString());
            try
            {
                tt = ddht.Where(x => x.MaDiaDiem == i).First();
            }
            catch { }
            DiaDiemThamQuan ddtq = new DiaDiemThamQuan();
            if (tt.MaDiaDiem > 0)
            {
                MessageBox.Show("Địa điểm đã có trong tour!");
            }
            else
            {
                ddtq.MaTour = matuor;
                ddtq.MaDiaDiem = i;
                try
                {
                    ddtq.Chitiet = (ddht.OrderByDescending(x => x.ThuTu).First().ThuTu + 1).ToString();
                }
                catch { ddtq.Chitiet = "1"; }
                dd.ThemDiaDiem(ddtq);
                ddht = dd.LoadDDHT(matuor);
                HienThiDiaDiem();
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            ddht.Where(x => x.ThuTu == 1).First().ThuTu = tp1.ThuTu;
            ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = 1;
            HienThiDiaDiem();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            DiaDiemHienThi tp1 = ddht.Where(x => x.MaDiaDiem == madd).First();
            DiaDiemHienThi tp2 = ddht.OrderByDescending(x => x.ThuTu).First();
            int temp = tp2.ThuTu;
            ddht.Where(x => x.ThuTu == tp2.ThuTu).First().ThuTu = tp1.ThuTu;
            ddht.Where(x => x.MaDiaDiem == madd).First().ThuTu = temp;
            HienThiDiaDiem();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
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

        private void simpleButton11_Click(object sender, EventArgs e)
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

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            try
            {
                dd.SuaDiaDiemThamQuan(ddht, matuor);
                MessageBox.Show("Cập nhật thành công!");
            }
            catch { MessageBox.Show("Đã có lỗi!"); }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn15)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn16)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gridView3_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn17)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    gridControl1.DataSource = new BindingList<Tour>(TourBUS.DSTour);
                    simpleButton13.Enabled = true;
                }
                catch { }
            }
            else
            {
                try
                {
                    gridControl1.DataSource = new BindingList<Tour>(TourBUS.DSTour.Where(x => x.TinhTrang == true).ToList());
                    simpleButton13.Enabled = false;
                }
                catch { }
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kích hoạt thành công!");
            TourBUS tb=new TourBUS();
            TourBUS.DSTour.RemoveAll(x => x.MaTour == matuor);
            TourBUS.DSTour.Add(tb.SuaTinhTrang(matuor));
            HienThiTour();
        }
    }
}
