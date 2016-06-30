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

            //BikeContainer bk = new BikeContainer();
            //bk.Description = "Lel";
            //bk.ID = "1";
            //bk.Neighbourhood = "Hookah";
            //BikeContainer bk3 = new BikeContainer();
            //bk3.Description = "Lel";
            //bk3.ID = "2";
            //bk3.Neighbourhood = "Hookah";
            //BikeContainer bk2 = new BikeContainer();
            //bk2.Description = "Lel";
            //bk2.ID = "2";
            //bk2.Neighbourhood = "Hook";
            //BikeGraphModel bg = new BikeGraphModel();
            //Neighbourhood nb = new Neighbourhood();
            //nb.Name = bk.Neighbourhood;
            //nb.AddContainer(bk);
            //nb.AddContainer(bk3);
            //Neighbourhood nb2 = new Neighbourhood();
            //nb2.Name = bk2.Neighbourhood;
            //nb2.AddContainer(bk2);
            //Neighbourhood nb3 = new Neighbourhood();

            //List<Neighbourhood> Neighbourhoods = new List<Neighbourhood>();
            //Neighbourhoods.Add(nb);
            //Neighbourhoods.Add(nb2);
            //bg.AddData(nb);
            //bg.AddData(nb2);
            GetShit();
            //foreach (Neighbourhood nb in gen.Neighbourhoods)
            //{
            //    Debug.WriteLine(nb.Name);
            //    bg.AddData(nb);
            //}
            //var t = gen.GenerateNeighbourhoods();
            //var result = t.Result;
     
            this.Content = chart;
        }

        public async void GetShit()
        {
            Top5Generator gen = new Top5Generator();
            BikeGraphModel bg = await gen.GenerateNeighbourhoods();
            chart.Series.Add(new ColumnSeries()
            {
                ItemsSource = bg.model,
                XBindingPath = "Name",
                YBindingPath = "BikeContainerCount",
            });
        }

    }
}
