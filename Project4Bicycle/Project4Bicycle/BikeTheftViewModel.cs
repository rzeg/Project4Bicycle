using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xamarin.Forms.Maps;

namespace Project4Bicycle
{
	public class BikeTheftViewModel
	{
		public ObservableCollection<BikeTheft> BikeThefts { get; } = new ObservableCollection<BikeTheft>();

		public async Task GetBikeTheftsAsync()
		{
			string requestUri = "http://www.rotterdamopendata.nl/storage/f/2014-01-30T11:49:23.696Z/fietsdiefstal-rotterdam-2011-2013.csv";
			var client = new HttpClient();
			var responseStream = await client.GetStreamAsync(requestUri);
			var reader = new StreamReader(responseStream);
			bool first = true;

			double latitude = 0f;
			double longtitude = 0f;

			BikeTheftFactory factory = new BikeTheftFactory(reader);




			//while (!reader.EndOfStream)
			//{
			//	var line = reader.ReadLine();
			//	var values = line.Split(',');

			//	if (!first)
			//	{

			//	}
			//	else {
			//		first = false;
			//	}
			//}

		}
	}
}