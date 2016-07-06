using Project4Bicycle.Models;
using System;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace Project4Bicycle
{
	public class ColorPieChartPage : ContentPage
	{
		SfChart colorChart = new SfChart();

		public ColorPieChartPage()
		{

			Title = "ColorChart";

			colorChart.Title = new ChartTitle { Text = "Colours" };

			GenerateGraph();

			Content = colorChart;
		}

		public async void GenerateGraph()
		{
			BrandColorGenerator generator = new BrandColorGenerator();

			BikeColorsGraphModel bg = await generator.GenerateColors();

			colorChart.Legend = new ChartLegend();
			colorChart.Legend.Title.Text = "Most stolen colours (Swipe left-to-right for brands)";


            colorChart.Series.Add(new PieSeries()
            {
                ItemsSource = bg.model,
                XBindingPath = "Name",
                YBindingPath = "Count",
                EnableTooltip = true,
                EnableAnimation = true,
                AnimationDuration = 3,
				EnableSmartLabels = true
			});
		}
	}
}


