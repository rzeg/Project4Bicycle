using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project4Bicycle
{
    class ContainerOverviewPage : ContentPage
    {
        public ContainerOverviewPage()
        {
            Title = "Overview";
            SfChart chart = new SfChart();
            CategoryAxis horizontalAxis = new CategoryAxis();
            horizontalAxis.Title = new ChartAxisTitle { Text = "Neighbourhoods" };
            chart.PrimaryAxis = horizontalAxis;
        }
    }
}
