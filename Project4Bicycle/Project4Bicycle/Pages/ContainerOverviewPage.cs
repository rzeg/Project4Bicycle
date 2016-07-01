using Project4Bicycle.Models;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project4Bicycle
{
    class ContainerOverviewPage : ContentPage
    {
        SfChart chart = new SfChart();
        public ContainerOverviewPage()
        {
            Title = "Overview";
            CategoryAxis horizontalAxis = new CategoryAxis();
            NumericalAxis verticalAxis = new NumericalAxis();
            verticalAxis.Title = new ChartAxisTitle { Text = "Bike containers" };
            horizontalAxis.Title = new ChartAxisTitle { Text = "Neighbourhoods" };
            chart.PrimaryAxis = horizontalAxis;
            chart.SecondaryAxis = verticalAxis;
            chart.Title = new ChartTitle { Text = "Top 5 neighbourhoods with bike containers" };
            GenerateGraph();
            this.Content = chart;
        }

        public async void GenerateGraph()
        {
            Top5Generator gen = new Top5Generator();
            BikeGraphModel bg = await gen.GenerateNeighbourhoods();
            chart.Series.Add(new ColumnSeries()
            {
                ItemsSource = bg.model,
                XBindingPath = "Name",
                YBindingPath = "BikeContainerCount",
                EnableAnimation = true,
                AnimationDuration = 3.0,
                EnableTooltip = true,
            });
        }

    }
}
