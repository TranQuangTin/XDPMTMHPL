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
            try
            {
                int i = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn1).ToString());
                gridControl2.DataSource = new BindingList<Tour>(ddb.LoadTour(i));
            }
            catch { }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn9)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == gridColumn10)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
    }
}
