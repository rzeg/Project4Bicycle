using Project4Bicycle.Models;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project4Bicycle
{
    class GroupedChartPage : ContentPage
    {
        SfChart chart = new SfChart
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Title = new ChartTitle { Text = "Amount of stolen bicycles and installed bicycle containers per month" },
            Legend = new ChartLegend(),
        };

        Picker picker;
        Q3Generator generator;
        Q3Model bg;
        StackingBarSeries thefts, bikecontainers;
        

        public GroupedChartPage()
        {
            Title = "Overview";
            NumericalAxis horizontalAxis = new NumericalAxis();
            CategoryAxis verticalAxis = new CategoryAxis();
            chart.PrimaryAxis = verticalAxis;
            chart.SecondaryAxis = horizontalAxis;

            ActivityIndicator indicator = new ActivityIndicator();
            indicator.IsRunning = true;
            indicator.IsEnabled = true;
            indicator.BindingContext = this;
            indicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");


            generator = new Q3Generator();

            picker = new Picker()
            {
                Title = "Neighbourhoods",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 100,
            };

            this.Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    indicator,
                    chart,
                    picker,
                }
            };

            picker.SelectedIndexChanged += (sender, args) =>
            {
                UpdateGraph();
            };

            this.IsBusy = true;
            chart.IsVisible = false;
            picker.IsVisible = false;
            Task.Run(() => GenerateGraph());
        }

        public async void GenerateGraph()
        {
            await generator.LoadData();
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (string neighbourhood in generator.GetNeighbourhoodList())
                    picker.Items.Add(neighbourhood);
                generator.SetNeighbourhood(picker.Items[0]);

                bg = generator.GenerateGraphModel();
                chart.Series.Add(bikecontainers = new StackingBarSeries()
                {
                    Label = "Bike containers",
                    ItemsSource = bg.model,
                    XBindingPath = "Month",
                    YBindingPath = "BikeContainerCount",
                    EnableAnimation = true,
                    AnimationDuration = 3.0,
                    EnableTooltip = true,
                });
                chart.Series.Add(thefts = new StackingBarSeries()
                {
                    Label = "Bike thefts",
                    ItemsSource = bg.model,
                    XBindingPath = "Month",
                    YBindingPath = "BikeTheftCount",
                    EnableAnimation = true,
                    AnimationDuration = 3.0,
                    EnableTooltip = true,
                });


                chart.IsVisible = true;
                picker.IsVisible = true;
                this.IsBusy = false;
            });
            

        }

        public void UpdateGraph()
        {
            if(generator != null)
            {
                generator.SetNeighbourhood(picker.Items[picker.SelectedIndex]);
                bg = generator.GenerateGraphModel();
                bikecontainers.ItemsSource = bg.model;
                thefts.ItemsSource = bg.model;
            }        
        }
    }
}
