using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EF;
using BUS;

namespace Win_Tour
{
    public partial class FormSuaMoTa : Form
    {
        public FormSuaMoTa()
        {
            InitializeComponent();
        }
        public Tour tr;

        private void FormSuaMoTa_Load(object sender, EventArgs e)
        {
            richEditControl1.HtmlText = tr.MoTa;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            tr.MoTa = richEditControl1.HtmlText;
            TourBUS tb = new TourBUS();
            tb.SuaTour(tr); MessageBox.Show("Sửa Thành Công!");
            Tour s1 = TourBUS.DSTour.Where(i => i.MaTour == tr.MaTour).First();
            var index = TourBUS.DSTour.IndexOf(s1);
            if (index != -1)
                TourBUS.DSTour[index] = tr;
            this.Dispose();
        }
    }
}
