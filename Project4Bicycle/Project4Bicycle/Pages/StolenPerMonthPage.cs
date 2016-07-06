using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Project4Bicycle
{
    class StolenPerMonthPage : ContentPage
    {
        //This method could be in a different class
        private string UpperFirst(string text)
        {
            return char.ToUpper(text[0]) +
                ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
        }

        SfChart chart = new SfChart();
        public ObservableCollection<ChartDataPoint> ChartData { get; set; }
        public List<string> incidentMonthList { get; set; }
        public string[] months { get; set; }
        public int[] monthThefts { get; set; }

        private async Task GetStolenBicyclesAsync()
        {
            var assembly = typeof(BikeTheftViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Project4Bicycle.Data.b59c159338.csv");
            var reader = new StreamReader(stream);

			BikeTheftFactory factory = new BikeTheftFactory(reader);
			BikeTheft bikeTheft;
            string incidentNeighboorhood = "Unknown";
            string incidentMonth = "Unknown";
            incidentMonthList.AddRange(months);


			while (factory.HasNext())
			{
				bikeTheft = factory.GetCurrent();

				incidentNeighboorhood = bikeTheft.Neighbourhood;
				//Convert the month number to the short name variant (Eg. 01 = Jan)
				incidentMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(bikeTheft.Month);
				incidentMonth = DateTime.ParseExact(incidentMonth, "MMMM", CultureInfo.InvariantCulture).ToString("MMM");

				switch (incidentMonth)
				{
					case "mrt":
						incidentMonth = "mar";
						break;

					case "mei":
						incidentMonth = "may";
						break;
					case "okt":
						incidentMonth = "oct";
						break;
				}

				//Find the corresponding month and add the amount to this month
				foreach (string month in incidentMonthList)
				{
					if (month == UpperFirst(incidentMonth))
					{
						//HighTemperature.Add(new ChartDataPoint(month, randomDiefstallen++));
						int index = Array.IndexOf(months, month);
						monthThefts[index]++; //Add 1 incident to the corresponding month
						break;
					}
				}
			}
        }

        private async Task FillData()
        {
            await GetStolenBicyclesAsync();
			Debug.WriteLine("afterasync");

			//Add the data to the chart
			foreach (string month in months)
            {
                int index = Array.IndexOf(months, month);
                ChartData.Add(new ChartDataPoint(month, monthThefts[index]));
            }

            LineSeries lineSeries = new LineSeries()
            {
                ItemsSource = ChartData,
                XBindingPath = "Year",
                YBindingPath = "Value"
            };

            lineSeries.EnableAnimation = true;
            lineSeries.AnimationDuration = 3;
            lineSeries.EnableTooltip = true;
            ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();

            zoomPanBehavior.EnableSelectionZooming = true;
            chart.ChartBehaviors.Add(zoomPanBehavior);
            chart.Series.Add(lineSeries);

            this.Content = chart;
        }

        public StolenPerMonthPage()
        {
            ChartData = new ObservableCollection<ChartDataPoint>();
            incidentMonthList = new List<string>();
            months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "  "};
            monthThefts = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            FillData();
            Title = "Stolen bicycles per month";

            //Initializing Primary Axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title = new ChartAxisTitle()
            {
                Text = "Month"
            };
            chart.PrimaryAxis = primaryAxis;

            //Initializing Secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title = new ChartAxisTitle()
            {
                Text = "Stolen bicycles"
            };
            chart.SecondaryAxis = secondaryAxis;
        }
    }
}
