using System;
using System.Collections.Generic;
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

            mostContainersButton.Clicked += MostContainersButton_Clicked;
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

        async void MostContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContainerOverviewPage());
        }
    }
}
