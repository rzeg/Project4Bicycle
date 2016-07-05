using Project4Bicycle.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace Project4Bicycle
{
	public class BikeTheftViewModel
	{
		public ObservableCollection<BikeTheft> BikeThefts = new ObservableCollection<BikeTheft>();
		public ObservableCollection<string> brands = new ObservableCollection<string>();
		public ObservableCollection<string> colors = new ObservableCollection<string>();

		HashSet<string> brandsHash = new HashSet<string>();
		HashSet<string> colorsHash = new HashSet<string>();

		public async Task GetBikeTheftsAsync()
		{
			string requestUri = "http://www.rotterdamopendata.nl/storage/f/2014-01-30T11:49:23.696Z/fietsdiefstal-rotterdam-2011-2013.csv";
			var client = new HttpClient();
			var responseStream = await client.GetStreamAsync(requestUri);
			var reader = new StreamReader(responseStream);

			BikeTheftFactory factory = new BikeTheftFactory(reader);
			BikeTheft bikeTheft;

			while (factory.HasNext())
			{
				bikeTheft = factory.GetCurrent();
				brandsHash.Add(bikeTheft.Brand);
				colorsHash.Add(bikeTheft.Color);
				BikeThefts.Add(bikeTheft);
			}

			foreach (var color in colorsHash)
			{
				colors.Add(color);
			}			

			foreach (var brand in brandsHash)
			{
				brands.Add(brand);
			}
		}
	}
}