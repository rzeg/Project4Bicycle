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
        public MainMenu()
        {
            Title = "Main menu";
            Button mostContainersButton = new Button { Text = "Top 5 amount of bike containers" };
            Button stolenPerMonthButton = new Button { Text = "Amount of stolen bicycles per month" };
            Button groupedChartButton = new Button { Text = "Grouped Bar chart of stolen bicycles and installed bike containers" };
            Button pieChartStolenBikesButton = new Button { Text = "Most stolen bikes and colors" };
            Button saveCurrentLocationButton = new Button { Text = "Save current location of bike" };
            Button createAppointmentButton = new Button { Text = "Create appointment in agenda" };

			saveCurrentLocationButton.Clicked += SaveCurrentLocationButton_Clicked;
            mostContainersButton.Clicked += MostContainersButton_Clicked;
            stolenPerMonthButton.Clicked += StolenPerMonthButton_Clicked;
            groupedChartButton.Clicked += GroupedChartButton_Clicked;
            pieChartStolenBikesButton.Clicked += PieChartStolenBikesButton_Clicked;

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
                }
            };

            this.Content = sl;
        }

        private async void PieChartStolenBikesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PieChartPage());
        }

        private async void GroupedChartButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroupedChartPage());
        }

        private async void StolenPerMonthButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StolenPerMonthPage());
        }

        async void MostContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContainerOverviewPage());
        }

		async void SaveCurrentLocationButton_Clicked(object sender, EventArgs e)
		{
			ICalendar calendar = DependencyService.Get<ICalendar>();
			calendar.SetEvent(10, 20, 30, "Hallo dit is een event", "we gaan schaatsen");
			calendar.SetReminder("Vergeet niet je schaatsen mee te nemen");
			calendar.SetReminder("Vergeet niet de fiets te maken");
		}

    }
}
