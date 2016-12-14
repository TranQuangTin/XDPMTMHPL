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
using DevExpress.XtraCharts;

namespace Win_Tour
{
    public partial class UCThongKe : UserControl
    {
        public UCThongKe()
        {
            InitializeComponent();
        }

        ThongKeBUS tkb = new ThongKeBUS();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DoiNgay();
                LoadDL();
                gridControl1.DataSource = new BindingList<DoiTuongThongKe>(dtk);
                VePieChart();
                VeLineChart();
            }
            catch { MessageBox.Show("Ngày không phù hợp!"); }
        }
        void VePieChart()
        {
            ChartControl pieChart = new ChartControl();
            List<DoiTuongThongKe> dtt = tkb.BieuDo(dt1, dt2);
            Series series1 = new Series("Tour - Doanh Thu", ViewType.Pie);
            foreach (DoiTuongThongKe a in dtt)
            {
                series1.Points.Add(new SeriesPoint(a.TenTour, a.TongTien));
            }
            pieChart.Series.Add(series1);
            // Format the the series labels.
            series1.Label.TextPattern = "{A}: {VP:p0}";
            // Adjust the position of series labels. 
            ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
            // Detect overlapping of series labels.
            ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            // Access the view-type-specific options of the series.
            PieSeriesView myView = (PieSeriesView)series1.View;
            // Show a title for the series.
            myView.Titles.Add(new SeriesTitle());
            myView.Titles[0].Text = series1.Name;
            // Specify a data filter to explode points.
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                DataFilterCondition.GreaterThanOrEqual, 9));
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.NotEqual, "Others"));
            //myView.ExplodeMode = PieExplodeMode.UseFilters;
            //myView.ExplodedDistancePercentage = 30;
            //myView.RuntimeExploding = true;
            //myView.HeightToWidthRatio = 0.75;
            // Hide the legend (if necessary).
            pieChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            // Add the chart to the form.
            pieChart.Dock = DockStyle.Fill;
            tabPage1.Controls.Clear();
            tabPage1.Controls.Add(pieChart);
        }
        void VeLineChart()
        {
            // Create a new chart.
            ChartControl lineChart = new ChartControl();

            // Create a line series.
            Series series1 = new Series("Series 1", ViewType.Line);

            // Add points to it.
            DateTime dt=dt1;
            int i = 1;
            while (dt <= dt2)
            {
                TimeSpan ts1 = new TimeSpan(23, 59, 0);
                DateTime dt3 = dt.Date + ts1;
                TimeSpan ts = new TimeSpan(0, 0, 0);
                dt = dt.Date + ts;
                series1.Points.Add(new SeriesPoint(dt, dtk.Where(x => x.NgayKhoiHanh >= dt && x.NgayKhoiHanh <= dt3).Sum(x => x.TongTien)));
                i++;
                dt = dt.AddDays(1);
            }
            lineChart.Series.Add(series1);

            // Set the numerical argument scale types for the series,
            // as it is qualitative, by default.
            series1.ArgumentScaleType = ScaleType.DateTime;

            // Access the view-type-specific options of the series.
            ((LineSeriesView)series1.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)series1.View).LineStyle.DashStyle = DashStyle.Solid;

            // Access the type-specific options of the diagram.
            ((XYDiagram)lineChart.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary).
            lineChart.Legend.Visible = false;

            // Add a title to the chart (if necessary).
            lineChart.Titles.Add(new ChartTitle());
            lineChart.Titles[0].Text = "Doanh Thu - Thời Gian";

            // Add the chart to the form.
            lineChart.Dock = DockStyle.Fill;
            tabPage2.Controls.Clear();
            tabPage2.Controls.Add(lineChart);
        }
        public List<DoiTuongThongKe> dtk;
        public void LoadDL()
        {
            dtk = tkb.ThongKe(dt1, dt2);
        }
        DateTime dt1;
        DateTime dt2;
        void DoiNgay()
        {
            dt1 = (DateTime)dateEdit1.EditValue;
            dt2 = (DateTime)dateEdit2.EditValue;
            if (dt1 == dt2) MessageBox.Show("Phải nhập 2 ngày khác nhau!");
            else if (dt1 > dt2)
            {
                DateTime temp = dt1;
                dt1 = dt2;
                dt2 = temp;
            }
            TimeSpan ts = new TimeSpan(23, 59, 0);
            dt2 = dt2.Date + ts;
            ts = new TimeSpan(0, 0, 0);
            dt1 = dt1.Date + ts;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //LoadDL();
            //DateTime dt1 = (DateTime)dateEdit1.EditValue;
            //DateTime dt2 = (DateTime)dateEdit2.EditValue;
            //if (dt1 == dt2) MessageBox.Show("Phải nhập 2 ngày khác nhau!");
            //else if (dt1 > dt2)
            //{
            //    DateTime temp = dt1;
            //    dt1 = dt2;
            //    dt2 = temp;
            //}
            //PieChart pc = new PieChart();
            //pc.dtt = tkb.BieuDo(dt1, dt2);
            //pc.ShowDialog();
        }
    }
}
