using System;

using Xamarin.Forms;

namespace Project4Bicycle
{
	public class ShareLocationPage : ContentPage
	{
		Label locationLabel;

		public ShareLocationPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }

				}
			};
			Button button = new Button
			{
				Text = "Click Me!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			button.Clicked += OnButtonClicked;

			locationLabel = new Label
			{
				Text = "Location",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
		}

		void OnButtonClicked(object sender, EventArgs e)
		{
			locationLabel.Text = "location 12312";
		}
	}


}


