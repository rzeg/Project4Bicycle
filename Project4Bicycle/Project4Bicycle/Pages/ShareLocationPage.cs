﻿using System;
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
		Position position;
		//string position;
		Geocoder geoCoder;

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
				Text = "Insert into reminder!",
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

			Picker picker = new Picker
			{
				Title = "Calendar",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" },
					button,
					calendarButton,
					reminderButton,
					locationLabel,
					picker,
					datePicker,
					timePicker
				}
			};
		}

		async void OnButtonClicked(object sender, EventArgs e)
		{
			geoCoder = new Geocoder();

			var locator1 = CrossGeolocator.Current;
            if(!locator1.IsGeolocationEnabled)
            {
                //GPS is unavailable
                await DisplayAlert("No GPS", "We could not retrieve your location, please make sure you have GPS enabled.", "OK");
            }
            else
            {
                //Retrieve GPS coordinates
                var pos = await locator1.GetPositionAsync(timeoutMilliseconds: 15000);
                if(pos.Longitude != 0.0D || pos.Latitude != 0.0D)//0.0D to check if empty, double can't be 'null'.
                {
                    position = new Position(pos.Latitude, pos.Longitude);

                  //Get addresses makes the app crash, needs to be fixed.
                  var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                  
                  //foreach (var address in possibleAddresses)
                  //{
                  //  Debug.WriteLine(address);
                  //}
                    
                }
                else
                {
                    //GPS is unavailable
                    await DisplayAlert("Time-out", "We could not retrieve your location on time, please try again.", "OK");
                }
                
            }
		}

		void OnCalendarButtonClicked(object sender, EventArgs e)
		{
			ICalendar calendar = DependencyService.Get<ICalendar>();

			calendar.SetEvent(datePicker.Date + timePicker.Time, "Fiets ophalen", "Locatie van fiets: " + position);
			Debug.WriteLine("Fiets ophalen", "Locatie van fiets: " + position);
			Debug.WriteLine("set event");
		}

		void OnReminderButtonClicked(object sender, EventArgs e)
		{
			ICalendar calendar = DependencyService.Get<ICalendar>();

			datePicker.Date = datePicker.Date + timePicker.Time;

			calendar.SetReminder(position + " at Time:  " + (datePicker.Date + timePicker.Time).ToString());
			Debug.WriteLine(position + " at Time:  " + (datePicker.Date + timePicker.Time).ToString());
			//Debug.WriteLine("set reminder");
		}
	}
}


