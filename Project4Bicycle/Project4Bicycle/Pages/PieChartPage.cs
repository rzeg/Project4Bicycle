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
	class PieChartPage : CarouselPage
    {
        public PieChartPage()
        {
			var navigationPageBrands = new BrandPieChartPage();
			navigationPageBrands.Title = "Brands";

			var navigationPageColors = new ColorPieChartPage();
			navigationPageColors.Title = "Colours";
			
			Children.Add(navigationPageBrands);
			Children.Add(navigationPageColors);
        }
    }
}
