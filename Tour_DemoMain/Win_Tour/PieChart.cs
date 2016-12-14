using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using EF;

namespace Win_Tour
{
    public partial class PieChart : Form
    {
        public PieChart()
        {
            InitializeComponent();
        }
        public List<DoiTuongThongKe> dtt;
        private void PieChart_Load(object sender, EventArgs e)
        {
            ChartControl pieChart = new ChartControl();

            // Create a pie series.
            Series series1 = new Series("Tour - Doanh Thu", ViewType.Pie);

            foreach (DoiTuongThongKe a in dtt)
            {
                series1.Points.Add(new SeriesPoint(a.TenTour, a.TongTien));
            }
            // Add the series to the chart.
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
            this.Controls.Add(pieChart);
        }
    }
}
