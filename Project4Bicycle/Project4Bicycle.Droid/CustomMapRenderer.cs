using System;
using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Project4Bicycle;
using Project4Bicycle.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace Project4Bicycle.Droid
{
	public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		GoogleMap map;
		List<CustomCircle> circles;

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				// Unsubscribe
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				circles = new List<CustomCircle>();
				foreach (var ShapeCoordinates in formsMap.ShapeNeighbourhood)
				{
					foreach (var position in ShapeCoordinates)
					{
						CustomCircle circleOverlay = new CustomCircle();
						circleOverlay.Position = new Position(position.Latitude, position.Longitude);
						circleOverlay.Radius = 1000;

						circles.Add(circleOverlay);
					}
				}


				((MapView)Control).GetMapAsync(this);
			}
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;

			if (circles != null)
			{
				foreach (var circle in circles)
				{
					var circleOptions = new CircleOptions();
					circleOptions.InvokeCenter(new LatLng(circle.Position.Latitude, circle.Position.Longitude));
					circleOptions.InvokeRadius(circle.Radius);
					circleOptions.InvokeFillColor(0X66FF0000);
					circleOptions.InvokeStrokeColor(0X66FF0000);
					circleOptions.InvokeStrokeWidth(0);
					map.AddCircle(circleOptions);
				}
			}

		}
	}

}

