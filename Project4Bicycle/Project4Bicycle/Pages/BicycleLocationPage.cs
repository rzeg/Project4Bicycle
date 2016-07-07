using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Project4Bicycle
{
    public class BicycleLocationPage : ContentPage
    {
        CustomMap map;
        List<NeighbourhoodTheft> filteredList;
        List<NeighbourhoodTheft> NeighbourhoodsThefts;
        ListView listView;
        Position myPosition;
        Geocoder geoCoder;


        public BicycleLocationPage()
        {
            GenerateList();
        }

        public async void GenerateList()
        {
            try
            {
                BikeContainerViewModel bikeContainerViewModel = new BikeContainerViewModel();
                await bikeContainerViewModel.GetHaltesAsync();
                var containers = bikeContainerViewModel.BikeContainers;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var tempPos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

                myPosition = new Position(tempPos.Latitude, tempPos.Longitude);


                var fcontainers = ClosestTo(containers, myPosition);


                // Create the ListView.
                listView = new ListView
                {
                    // Source of data items.
                    ItemsSource = fcontainers,

                    // Define template for displaying each item.
                    // (Argument of DataTemplate constructor is called for 
                    //      each item; it must return a Cell derivative.)
                    ItemTemplate = new DataTemplate(() =>
                    {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                        nameLabel.SetBinding(Label.TextProperty, "Street");

                        Label birthdayLabel = new Label();
                        Binding lat = new Binding();

                        birthdayLabel.SetBinding(Label.TextProperty, "Distance");
                        birthdayLabel.SetBinding(Label.TextProperty,
                                                 new Binding("Distance", BindingMode.OneWay,
                                                             null, null, "{0} Meter"));
                    //BoxView boxView = new BoxView();
                    //boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

                    // Return an assembled ViewCell.
                    return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                    {
									//boxView,
									new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            nameLabel,
                                            birthdayLabel
                                        }
                                        }
                                    }
                            }
                        };
                    })
                };

                map = new CustomMap(
                    MapSpan.FromCenterAndRadius(
                        new Position(51.9202975795699, 4.49352622032165), Distance.FromMiles(5)))
                {
                    IsShowingUser = true,
                    HeightRequest = 200,
                    WidthRequest = 320,
                    MapType = MapType.Hybrid,
                };


                foreach (var container in containers)
                {
                    var position = new Position(container.Latitude, container.Longitude);
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = container.Description,
                        Address = container.Street
                    };
                    map.Pins.Add(pin);
                }


                var pin2 = new Pin
                {
                    Type = PinType.Place,
                    Position = myPosition,
                    Label = "me"
                };
                map.Pins.Add(pin2);

                DoubleClick doubleClick = new DoubleClick();

                listView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
                {
                    BikeContainer container = e.Item as BikeContainer;
                //var selectedContainer = container;
                var containerPosition = new Position(container.Latitude, container.Longitude);


                    if (doubleClick.Check(container))
                    {
                        geoCoder = new Geocoder();
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(containerPosition);
                        string containeraddress = "";
                        foreach (var address in possibleAddresses)
                        {
                            containeraddress = address;
                        }

                        switch (Device.OS)
                        {
                            case TargetPlatform.iOS:
                                Device.OpenUri(
                                  new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(containeraddress))));
                                break;
                            case TargetPlatform.Android:
                                Device.OpenUri(
                                  new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(containeraddress))));
                                break;
                        }

                    }
                    else {
                    //var containerPosition = new Position(container.Latitude, container.Longitude);
                    map.MoveToRegion(
                            MapSpan.FromCenterAndRadius(
                                containerPosition, Distance.FromMiles(.1f)));

                    }
                };

                var stack = new StackLayout { Spacing = 0 };
                var innerStack = new StackLayout { Spacing = 0 };
                Button gotoMyLocation = new Button { Text = "My location" };
                gotoMyLocation.Clicked += gotoMyLocation_clicked;
                innerStack.Children.Add(map);
                stack.Children.Add(new Label { Text = "Map" });

                // Accomodate iPhone status bar.
                this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

                stack.Children.Add(innerStack);
                stack.Children.Add(gotoMyLocation);
                stack.Children.Add(listView);
                Content = stack;
            }
            catch
            {
                Label locationLabel;
                locationLabel = new Label
                {
                    Text = "No GPS available, please restart the app and try again with GPS enabled.",
                    Font = Font.SystemFontOfSize(NamedSize.Large),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                Content = new StackLayout
                {
                    Children = {
                        locationLabel
                    }
                };
                await DisplayAlert("No GPS available", "We could not retrieve your location on time, please try again.", "OK");
            }

        }

        public static IEnumerable<BikeContainer> ClosestTo(ObservableCollection<BikeContainer> collection, Position location)
        {

            var newCollection = collection.Select(x => { x.Distance = Convert.ToInt32(getDistanceFromLatLonInKm(location, new Position(x.Latitude, x.Longitude)) * 1000); return x; });
            return newCollection.OrderBy(x => x.Distance);
            //return newCollection.OrderBy(x => Math.Abs(location.Latitude - x.Latitude) + Math.Abs(location.Longitude - x.Longitude));
        }

        private async void gotoMyLocation_clicked(object sender, EventArgs e)
        {
            map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                    myPosition, Distance.FromMiles(.1f)));
        }


        public static double getDistanceFromLatLonInKm(Position pos1, Position pos2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = deg2rad(pos1.Latitude - pos2.Latitude);  // deg2rad below
            var dLon = deg2rad(pos2.Longitude - pos1.Longitude);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(deg2rad(pos1.Latitude)) * Math.Cos(deg2rad(pos2.Latitude)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        public static double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public class DoubleClick
        {
            BikeContainer prevContainer;
            public bool Check(BikeContainer container)
            {
                if (prevContainer == container)
                {
                    return true;
                }
                else {
                    prevContainer = container;
                    return false;
                }
            }
        }



    }
}


