using System;
using System.Collections.Generic;
using System.Diagnostics;
using Project4Bicycle.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Project4Bicycle
{
	public class MapPage : ContentPage
	{
		CustomMap map;

		public MapPage()
		{
			map = new CustomMap(
				MapSpan.FromCenterAndRadius(
					new Position(51.9202975795699, 4.49352622032165), Distance.FromMiles(5)))
			{
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};


			GenerateMap();

			var stack = new StackLayout { Spacing = 0 };
			stack.Children.Add(map);
			Content = stack;

		}

		public async void GenerateMap()
		{
			NeighbourhoodTheftsGenerator generator = new NeighbourhoodTheftsGenerator();
			List<NeighbourhoodTheft> bt = await generator.GenerateNeighbourhoods();

			foreach (var neighbourhood in bt)
			{
				List<Position> ShapeCoordinates = new List<Position>();

				//var _NElat = neighbourhood.NE.Latitude;
				//var _NElon = neighbourhood.NE.Longitude;

				//var _ZWlat = neighbourhood.ZW.Latitude;
				//var _ZWlon = neighbourhood.ZW.Longitude;

				//if(_NElat > 0)
				//	ShapeCoordinates.Add(new Position(_NElat, _NElon));
				//if(_ZWlat > 0)
				//	ShapeCoordinates.Add(new Position(_ZWlat, _ZWlon));


				//Position _posNE = new Position(_NElat + ((_NElat - _ZWlat)), _NElon);
				//Position _posZW = new Position(_ZWlat, _ZWlon - (_ZWlon - _NElon));

				//if (_posNE.Latitude > 0)
				//	ShapeCoordinates.Add(_posNE);

				//if (_posZW.Latitude > 0)
				//	ShapeCoordinates.Add(_posZW);

				//if(ShapeCoordinates.Count > 0)
				map.ShapeNeighbourhood.Add(neighbourhood.Positions);
			}

			var stack = new StackLayout { Spacing = 0 };
			stack.Children.Add(map);
			Content = stack;

			//bt.

		}
	}

	public class CustomCircle
	{
		public Position Position { get; set; }
		public double Radius { get; set; }
	}

	public class CustomMap : Map
	{
		public List<List<Position>> ShapeNeighbourhood { get; set; }
		public CustomCircle Circle { get; set; }


		public CustomMap(MapSpan value) : base (value)
		{
			ShapeNeighbourhood = new List<List<Position>>();
		}
	}
}


