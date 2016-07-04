using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xamarin.Forms.Maps;
using System.Globalization;

namespace Project4Bicycle
{

	public class BikeContainerViewModel
	{
		public ObservableCollection<BikeContainer> BikeContainers { get; } = new ObservableCollection<BikeContainer>();

		public BikeContainerViewModel()
		{

		}

		public async Task GetHaltesAsync()
		{
			//RetDatabase database = new RetDatabase();

			//			Reset database
			//			database.Drop();


			//if (database.Count())
			//{
				string requestUri = "http://puu.sh/pxxc2.csv";
				var client = new HttpClient();
				var responseStream = await client.GetStreamAsync(requestUri);
				var reader = new StreamReader(responseStream);
				bool first = true;

				//FindDuplicates dublicator = new FindDuplicates();

				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					if (!first)
					{
						string id = values[0];
						string description = values[5]; //fietstrommel
						string street = values[9];
                        double latitude = 0.0;
                        double longtitude = 0.0;
						string neighbourhood = values[28];
					//Debug.WriteLine(values[32]);
                        //DateTime dt = Convert.ToDateTime(values[32]);
					    int month = Int32.Parse(values[32].Split('-')[1]);
                        NumberStyles style = NumberStyles.AllowDecimalPoint;

                        if (Double.TryParse(values[18], style, CultureInfo.InvariantCulture, out latitude) && Double.TryParse(values[19], style, CultureInfo.InvariantCulture, out longtitude))
						{


							//var position = new Position(latitude, longtitude);
							//var pin = new Pin
							//{
							//	Type = PinType.Place,
							//	Position = position,
							//	Label = description,
							//	Address = street
							//};


							BikeContainer bikeContainer = new BikeContainer();
							bikeContainer.ID = id;
							bikeContainer.Description = description;
							bikeContainer.Street = street;
							bikeContainer.Neighbourhood = neighbourhood;
							bikeContainer.Latitude = latitude;
							bikeContainer.Longitude = longtitude;
                            bikeContainer.Month = month;
							//database.SaveItem(retItem);
							BikeContainers.Add(bikeContainer);
						}
					}
					else {
						first = false;
					}
				}
			//}
			//else {
			//	foreach (var item in database.GetItems())
			//	{
			//		var position = new Position(item.Latitude, item.Longitude);
			//		var pin = new Pin
			//		{
			//			Type = PinType.Place,
			//			Position = position,
			//			Label = item.Description,
			//			Address = item.Name
			//		};
			//	}
				
			//}
		}
	}
}

