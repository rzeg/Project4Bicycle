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
			Title = "Colors / Brands";


			this.Content = chart;
        }


		public async void GenerateGraph()
		{
			BrandColorGenerator generator = new BrandColorGenerator();
		}
    }
}
