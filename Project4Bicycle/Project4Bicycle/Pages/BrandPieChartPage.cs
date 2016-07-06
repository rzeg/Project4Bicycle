using Project4Bicycle.Models;
using System;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace Project4Bicycle
{
	public class BrandPieChartPage : ContentPage
	{
		SfChart brandChart = new SfChart();
		public BrandPieChartPage()
		{
			Title = "BrandChart";

			brandChart.Title = new ChartTitle { Text = "Brands" };

			GenerateGraph();

			Content = brandChart;
		}

		public async void GenerateGraph()
		{
			BrandColorGenerator generator = new BrandColorGenerator();

			BikeBrandsGraphModel bg = await generator.GenerateBrands();

			brandChart.Legend = new ChartLegend();
			brandChart.Legend.Title.Text = "Most stolen brands (Swipe right-to-left to see colours)";


			brandChart.Series.Add(new PieSeries()
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


