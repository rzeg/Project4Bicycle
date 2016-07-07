using System;
using System.Globalization;
using System.IO;

namespace Project4Bicycle
{
	class BikeContainerFactory : TraditionalIterator<BikeContainer>
	{
		StreamReader reader;
		bool skipRow;
		string currentLine;
		string[] values;

		public BikeContainerFactory(StreamReader reader)
		{
			this.reader = reader;
			skipRow = true;
		}
		public BikeContainer GetCurrent()
		{
			currentLine = reader.ReadLine();
			values = currentLine.Split(',');
			NumberStyles style = NumberStyles.AllowDecimalPoint;
			BikeContainer bikeContainer = new BikeContainer();

			double latitude = 0.0;
			double longtitude = 0.0;

			if (skipRow && HasNext())
			{
				skipRow = false;
				currentLine = reader.ReadLine();
				values = currentLine.Split(',');
			}

			if (Double.TryParse(values[18], style, CultureInfo.InvariantCulture, out latitude) && Double.TryParse(values[19], style, CultureInfo.InvariantCulture, out longtitude))
			{

			}
			else {
				values = currentLine.Split(',');
			}

			string id = values[0];
			string description = values[5]; //fietstrommel
			string street = values[9];

			string neighbourhood = values[28];
			int month = Int32.Parse(values[32].Split('-')[1]);

			bikeContainer.ID = id;
			bikeContainer.Description = description;
			bikeContainer.Street = street;
			bikeContainer.Neighbourhood = neighbourhood;
			bikeContainer.Latitude = latitude;
			bikeContainer.Longitude = longtitude;
			bikeContainer.Month = month;
				

			return bikeContainer;
		}

		public bool HasNext()
		{
			return !reader.EndOfStream;
		}

		public void MoveNext()
		{
			throw new NotImplementedException();
		}
	}
}