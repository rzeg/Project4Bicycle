using Project4Bicycle.Models;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project4Bicycle
{
    class GroupedChartPage : ContentPage
    {
        SfChart chart = new SfChart();
        public GroupedChartPage()
        {
            Title = "Overview";
            NumericalAxis horizontalAxis = new NumericalAxis();
            NumericalAxis verticalAxis = new NumericalAxis();
            chart.PrimaryAxis = horizontalAxis;
            chart.SecondaryAxis = verticalAxis;
            chart.Title = new ChartTitle { Text = "Grouped chart" };
            GenerateGraph();
            this.Content = chart;
        }

        public async void GenerateGraph()
        {
            Q3Generator gen = new Q3Generator("hal");
            Q3Model bg = await gen.GenerateGraphModel();
            chart.Series.Add(new StackingBarSeries()
            {
                ItemsSource = bg.model,
                GroupingLabel = "grp1",
                XBindingPath = "Month",
                YBindingPath = "BikeContainerCount",
                EnableAnimation = true,
                AnimationDuration = 3.0,
                EnableTooltip = true,
            });
            chart.Series.Add(new StackingBarSeries()
            {
                ItemsSource = bg.model,
                GroupingLabel = "grp1",
                XBindingPath = "Month",
                YBindingPath = "BikeTheftCount",
                EnableAnimation = true,
                AnimationDuration = 3.0,
                EnableTooltip = true,
            });

        }
    }
}
