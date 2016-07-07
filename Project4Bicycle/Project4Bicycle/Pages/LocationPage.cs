using System;

using Xamarin.Forms;

namespace Project4Bicycle
{
	public class LocationPage : CarouselPage
	{
		public LocationPage()
		{
			var SLP = new ShareLocationPage();
			SLP.Title = "Share my location";

			var bicycleLocationPage = new BicycleLocationPage();
			bicycleLocationPage.Title = "Location of your bike";

			Children.Add(SLP);
			Children.Add(bicycleLocationPage);
		}
	}
}


