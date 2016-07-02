using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        public ObservableCollection<ChartDataPoint> HighTemperature { get; set; }
        public List<string> incidentMonthList { get; set; }
        public string[] months { get; set; }
        public int[] monthThefts { get; set; }

        private async Task GetStolenBicyclesAsync()
        {
            string URL = "http://puu.sh/pLgJy/b59c159338.csv";
            var client = new HttpClient();
            var responseStream = await client.GetStreamAsync(URL);
            var reader = new StreamReader(responseStream);
            bool skipRow = true;
            string incidentNeighboorhood = "Unknown";
            string incidentMonth = "Unknown";

            incidentMonthList.AddRange(months);

            int cnt = 0;



            while (!reader.EndOfStream && cnt <= 20500)
            {
                try
                {
                    cnt++;
                    if(cnt == 10351)
                    {

                    }
                    var line = reader.ReadLine();
                    string[] values = new string[] { };
                    string[] tempIncidentDate = { };
                    if (!skipRow && line.Length > 150 && (line.Contains("\t") || line.Contains(",")))
                    {
                        //Some lines are seperated with tabs but somehow also have 1 comma, we look for at least 3 comma's to avoid this problem.
                        if (line.Split(',').Length > 3) 
                        {
                            //Split by looking for ',' and split the dates by using '/'
                            values = line.Split(',');
                            tempIncidentDate = values[11].Split('/');
                        }
                        else
                        {
                            //Split by looking for '\t' and split the dates by using '-'
                            values = line.Split('\t');
                            tempIncidentDate = values[11].Split('-');
                        }
                        incidentNeighboorhood = values[8];
                        //Convert the month number to the short name variant (Eg. 01 = Jan)
                        incidentMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(tempIncidentDate[1]));
                        incidentMonth = DateTime.ParseExact(incidentMonth, "MMMM", CultureInfo.InvariantCulture).ToString("MMM");

                        switch (incidentMonth)
                        {
                            case "mrt":
                                incidentMonth = "mar";
                                break;

                            case "mei":
                                incidentMonth = "may";
                                break;
                        }

                        //Find the corresponding month and add the amount to this month

                        int randomDiefstallen = 10;
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
                    else
                    {
                        //Skip the first row to avoid counting the columnnames.
                        skipRow = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }

        private async Task FillData()
        {
            await GetStolenBicyclesAsync();

            //Add the data to the chart
            foreach (string month in months)
            {
                int index = Array.IndexOf(months, month);
                HighTemperature.Add(new ChartDataPoint(month, monthThefts[index]));
            }

            LineSeries lineSeries = new LineSeries()
            {
                ItemsSource = HighTemperature,
                XBindingPath = "Year",
                YBindingPath = "Value"
            };

            lineSeries.EnableAnimation = true;
            lineSeries.AnimationDuration = 3;
            ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();

            zoomPanBehavior.EnableSelectionZooming = true;
            chart.ChartBehaviors.Add(zoomPanBehavior);
            chart.Series.Add(lineSeries);

            this.Content = chart;
        }

        public StolenPerMonthPage()
        {
            HighTemperature = new ObservableCollection<ChartDataPoint>();
            incidentMonthList = new List<string>();
            months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            monthThefts = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

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



            //string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //incidentMonthList.AddRange(months);
            //foreach (string month in incidentMonthList)
            //{
            //    HighTemperature.Add(new ChartDataPoint(month, 0));
            //}







        }
    }
}
