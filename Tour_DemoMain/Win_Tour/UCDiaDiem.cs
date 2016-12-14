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
    public partial class UCDiaDiem : UserControl
    {
        public UCDiaDiem()
        {
            InitializeComponent();
        }
        DiaDiemBUS ddb = new DiaDiemBUS();
        private void UCDiaDiem_Load(object sender, EventArgs e)
        {
            HienThi();
        }
        void HienThi()
        {
            DiaDiemBUS.LoadDD();
            gridControl1.DataSource = new BindingList<DiaDiemTemp>(ddb.LoadDDHT());
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int i = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1).ToString());
            gridControl2.DataSource= new BindingList<Tour>(ddb.LoadTour(i));
        }
    }
}
