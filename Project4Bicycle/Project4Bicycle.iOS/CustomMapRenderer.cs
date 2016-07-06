using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreLocation;
using MapKit;
using MapOverlay.iOS;
using Project4Bicycle;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.iOS
{
	public class CustomMapRenderer : MapRenderer
	{
		//MKPolygonRenderer polygonRenderer;

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			//base.OnElementChanged(e);

			//if (e.OldElement != null)
			//{
			//	var nativeMap = Control as MKMapView;
			//	nativeMap.OverlayRenderer = null;
			//}

			//if (e.NewElement != null)
			//{
			//	var formsMap = (CustomMap)e.NewElement;
			//	var nativeMap = Control as MKMapView;
			//	nativeMap.OverlayRenderer = GetOverlayRenderer;

			//	int i = 0;
			//	foreach (var ShapeCoordinates in formsMap.ShapeNeighbourhood)
			//	{

			//		CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[ShapeCoordinates.Count];

			//		int index = 0;
			//		foreach (var position in ShapeCoordinates)
			//		{
			//			coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
			//			index++;
			//		}

			//		var blockOverlay = MKPolygon.FromCoordinates(coords);
			//		nativeMap.AddOverlay(blockOverlay);
			//	}

			//}

			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.OverlayRenderer = null;
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;
				var circle = formsMap.Circle;

				nativeMap.OverlayRenderer = GetOverlayRenderer;

				foreach (var ShapeCoordinates in formsMap.ShapeNeighbourhood)
				{
					foreach (var position in ShapeCoordinates)
					{
						var circleOverlay = MKCircle.Circle(new CoreLocation.CLLocationCoordinate2D(position.Latitude, position.Longitude), 1000);
						nativeMap.AddOverlay(circleOverlay);
					}
				}
			}
		}

		MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			var o = ObjCRuntime.Runtime.GetNSObject(overlay.Handle) as MKCircle;

			MKCircleRenderer circleRenderer = new MKCircleRenderer(o);
			circleRenderer.FillColor = UIColor.Red;
			circleRenderer.Alpha = 0.4f;

			return circleRenderer;
		}
	}
}
	//MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
	//{
	//	var o = ObjCRuntime.Runtime.GetNSObject(overlay.Handle) as MKPolygon;
	//	MKPolygonRenderer polygonRenderer = new MKPolygonRenderer(o);

	//	polygonRenderer.FillColor = UIColor.Red;
	//	polygonRenderer.StrokeColor = UIColor.Blue;
	//	polygonRenderer.Alpha = 0.4f;
	//	polygonRenderer.LineWidth = 9;

	//	return polygonRenderer;
	//}