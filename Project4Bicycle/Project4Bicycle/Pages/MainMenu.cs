using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project4Bicycle
{
    class MainMenu : ContentPage
    {
        StolenPerMonthPage SPMP;
        ContainerOverviewPage COP;
        PieChartPage PCP;
        GroupedChartPage GCP;
        public MainMenu()
        {
            Title = "Main menu";
            Button mostContainersButton = new Button { Text = "Top 5 amount of bike containers" };
            Button stolenPerMonthButton = new Button { Text = "Amount of stolen bicycles per month" };
            Button groupedChartButton = new Button { Text = "Grouped Bar chart of stolen bicycles and installed bike containers" };
            Button pieChartStolenBikesButton = new Button { Text = "Most stolen bikes and colors" };
            Button saveCurrentLocationButton = new Button { Text = "Save current location of bike" };
            Button createAppointmentButton = new Button { Text = "Create appointment in agenda" };
            Button mapsButton = new Button { Text = "Maps" };
            //SPMP = new StolenPerMonthPage();
            //COP = new ContainerOverviewPage();
            //PCP = new PieChartPage();
            //GCP = new GroupedChartPage();
            saveCurrentLocationButton.Clicked += SaveCurrentLocationButton_Clicked;
            mostContainersButton.Clicked += MostContainersButton_Clicked;
            stolenPerMonthButton.Clicked += StolenPerMonthButton_Clicked;
            groupedChartButton.Clicked += GroupedChartButton_Clicked;
            pieChartStolenBikesButton.Clicked += PieChartStolenBikesButton_Clicked;
			mapsButton.Clicked += MapsButton_clicked;

            StackLayout sl = new StackLayout
            {
                Children =
                {
                    mostContainersButton,
                    stolenPerMonthButton,
                    groupedChartButton,
                    pieChartStolenBikesButton,
                    saveCurrentLocationButton,
                    createAppointmentButton,
					mapsButton,
                }
            };

            this.Content = sl;
        }

        private async void PieChartStolenBikesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(PCP);
        }

        private async void GroupedChartButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(GCP);
        }

        private async void StolenPerMonthButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(SPMP);
        }

        async void MostContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(COP);
        }


		async void SaveCurrentLocationButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ShareLocationPage());
		}

		async void MapsButton_clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MapPage());
		}

    }
}
