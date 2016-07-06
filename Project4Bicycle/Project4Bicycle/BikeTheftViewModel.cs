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
using System.Reflection;

namespace Project4Bicycle
{
	public class BikeTheftViewModel
	{
		public ObservableCollection<BikeTheft> BikeThefts = new ObservableCollection<BikeTheft>();
		public ObservableCollection<string> brands = new ObservableCollection<string>();
		public ObservableCollection<string> colors = new ObservableCollection<string>();
		public ObservableCollection<string> neighbourhoods = new ObservableCollection<string>();

		HashSet<string> brandsHash = new HashSet<string>();
		HashSet<string> colorsHash = new HashSet<string>();
		HashSet<string> neighbourhoodHash = new HashSet<string>();

		public async Task GetBikeTheftsAsync()
		{
            var assembly = typeof(BikeTheftViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Project4Bicycle.Data.b59c159338.csv");
			var reader = new StreamReader(stream);

			BikeTheftFactory factory = new BikeTheftFactory(reader);
			BikeTheft bikeTheft;

			while (factory.HasNext())
			{
				bikeTheft = factory.GetCurrent();
				brandsHash.Add(bikeTheft.Brand);
				colorsHash.Add(bikeTheft.Color);
				neighbourhoodHash.Add(bikeTheft.Neighbourhood);
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

			foreach (var neighbourhood in neighbourhoodHash)
			{
				neighbourhoods.Add(neighbourhood);
			}
		}
	}
}