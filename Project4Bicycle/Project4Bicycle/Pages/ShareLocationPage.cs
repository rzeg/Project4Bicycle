using System;
using Plugin.Geolocator;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Project4Bicycle
{
	public class ShareLocationPage : ContentPage
	{
		Label locationLabel;
		DatePicker datePicker;
		TimePicker timePicker;
		string position;

		public ShareLocationPage()
		{
			
			Button button = new Button
			{
				Text = "Get Location!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			button.Clicked += OnButtonClicked;

			Button calendarButton = new Button
			{
				Text = "Insert into calendar!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			calendarButton.Clicked += OnCalendarButtonClicked;

			Button reminderButton = new Button
			{
				Text = "Insert into calendar!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			reminderButton.Clicked += OnReminderButtonClicked;

			locationLabel = new Label
			{
				Text = "Location",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			datePicker = new DatePicker
			{
				Format = "D",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			timePicker = new TimePicker();

			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" },
					button,
					calendarButton,
					reminderButton,
					locationLabel,
					datePicker,
					timePicker
				}
			};
		}

		async void OnButtonClicked(object sender, EventArgs e)
		{
			
			position = "213156785432";


			//#if __ANDROID__

			//var locator1 = CrossGeolocator.Current;
			//var position1 = await locator1.GetPositionAsync(timeoutMilliseconds: 10000);
			//position = "Position Latitude: "+position1.Latitude+" Position Longitude: "+position1.Latitude;

			//#endif

			locationLabel.Text = position;

		}


		void OnCalendarButtonClicked(object sender, EventArgs e)
		{
			ICalendar calendar = DependencyService.Get<ICalendar>();

			calendar.SetEvent(datePicker.Date + timePicker.Time, "Fiets ophalen", "Locatie van fiets: " + position);
			Debug.WriteLine("set event");
		}

		void OnReminderButtonClicked(object sender, EventArgs e)
		{
			ICalendar calendar = DependencyService.Get<ICalendar>();

			datePicker.Date = datePicker.Date + timePicker.Time;

			calendar.SetReminder(position + " at Time:  " + (datePicker.Date + timePicker.Time).ToString());
			Debug.WriteLine("set reminder");
		}
	}
}


