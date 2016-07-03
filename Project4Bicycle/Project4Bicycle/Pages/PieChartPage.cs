using Project4Bicycle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace Project4Bicycle
{
    class PieChartPage : ContentPage
	{
		SfChart chart = new SfChart();

        public PieChartPage()
        {
			Title = "Overview";

			chart.Title = new ChartTitle { Text = "Top 5 neighbourhoods with bike containers" };

			GenerateGraph();

			this.Content = chart;
        }

		public async void GenerateGraph()
		{
			BrandColorGenerator generator = new BrandColorGenerator();
			BikeColorsGraphModel bg = await generator.GenerateColors();

			chart.Legend = new ChartLegend();

			chart.Legend.Title.Text = "Colors";

			chart.Series.Add(new PieSeries()
			{
				ItemsSource = bg.model,
				XBindingPath = "Name",
				YBindingPath = "BikeContainerCount",
				EnableTooltip = true,
				EnableSmartLabels = true,
				DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
				ConnectorLineType = ConnectorLineType.Bezier,
				StartAngle = 75,
				EndAngle = 435,
				DataMarker = new ChartDataMarker(),
			});
		}
    }
}
