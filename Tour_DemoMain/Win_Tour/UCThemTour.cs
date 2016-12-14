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
    public partial class UCThemTour : UserControl
    {
        public UCThemTour()
        {
            InitializeComponent();
        }

        private void UCThemTour_Load(object sender, EventArgs e)
        {
            LoadcboLoaiTour();
        }
        private void LoadcboLoaiTour()
        {
            LoaiTourBUS lt = new LoaiTourBUS();
            if (lt.DSLoaiTour.Count == 0) lt.LoadLoaiTour();
            comboBox1.DataSource = new BindingList<LoaiTour>(lt.DSLoaiTour);
            comboBox1.DisplayMember = "TenLoaiTour";
            comboBox1.ValueMember = "MaLoaiTour";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text="";
            //textBox3.Text="";
            textBox4.Text = "";
            richTextBox1.Text = "";
            //checkBox1.Checked = false;
            LoadcboLoaiTour();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && richTextBox1.Text!="")
            {
                Tour tr = new Tour();
                tr.TenTour = textBox1.Text;
                tr.LoaiTour = int.Parse(comboBox1.SelectedValue.ToString());
                tr.MoTa = richTextBox1.Text;
                try
                {
                    tr.SoNgay = int.Parse(textBox2.Text);
                    if (tr.SoNgay > 0)
                    {
                        tr.SoDem = 0;
                        tr.Mua = textBox4.Text;
                        tr.TinhTrang = true;
                        try
                        {
                            TourBUS tb = new TourBUS();
                            tb.ThemTour(tr);
                            MessageBox.Show("Thêm thành công!");
                            TourBUS.LoadTour1();
                            simpleButton2_Click(sender, e);
                        }
                        catch { MessageBox.Show("Thêm Thất Bại!"); }
                    }
                    else MessageBox.Show("Số ngày phải lớn hơn 0!");
                }
                catch { MessageBox.Show("Số ngày phải là số!"); }
            }
            else MessageBox.Show("Chưa nhập đầy đủ thông tin!");
        }
    }
}
