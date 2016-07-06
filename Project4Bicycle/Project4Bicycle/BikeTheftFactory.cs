using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Project4Bicycle
{

	interface TraditionalIterator<T>
	{
		T GetCurrent();
		bool HasNext();
	}


	public class BikeTheftFactory : TraditionalIterator<BikeTheft>
	{
		StreamReader reader;
		bool skipRow;
		string currentLine;
		string[] values;

		public BikeTheftFactory(StreamReader reader)
		{
			this.reader = reader;
			skipRow = true;
		}

		public BikeTheft GetCurrent()
		{
			currentLine = reader.ReadLine();

			values = getValues(currentLine);

			while (values.Length != 25 && HasNext())
			{
				currentLine = reader.ReadLine();
				values = getValues(currentLine);
			}


			if (skipRow && HasNext())
			{
				skipRow = false;
				currentLine = reader.ReadLine();
				values = getValues(currentLine);
			}


			BikeTheft bikeTheft = new BikeTheft();
			bikeTheft.Id = values[0];
			bikeTheft.Description = values[4];
			bikeTheft.City = values[7];
			if (values[8].Length > 3)
				bikeTheft.Neighbourhood = values[8].Substring(3);
			else
				bikeTheft.Neighbourhood = values[8];


			bikeTheft.Street = values[9];

			if (values[11] != "")
			{
				//bikeTheft.Month = Convert.ToDateTime(values[11]).Month;//Int32.Parse(values[11].Split('/')[1]);
				bikeTheft.Month = Int32.Parse(values[11].Split('/')[1]);
				bikeTheft.Year = Int32.Parse(values[11].Split('/')[2]);
			}
			else {
				bikeTheft.Month = 1;
				bikeTheft.Year = 2013;
			}

			bikeTheft.Keyword = values[20];
			bikeTheft.Object = values[21];
			bikeTheft.Brand = values[22];
			bikeTheft.Type = values[23];

			//correct colors
			if (values[24] == "" || values[24] == "0")
				values[24] = "ONBEKEND";
			
			bikeTheft.Color = values[24];
			return bikeTheft;
		}

		public bool HasNext()
		{
			return !reader.EndOfStream;
		}

		public void MoveNext()
		{
			
		}

		public string[] getValues(string line)
		{
			//Some lines are seperated with tabs but somehow also have 1 comma, we look for at least 3 comma's to avoid this problem.
			if (currentLine.Split(',').Length > 3)
			{
				//Split by looking for ',' and split the dates by using '/'
				values = currentLine.Split(',');
			}
			else
			{
				//Split by looking for '\t' and split the dates by using '-'
				values = currentLine.Split('\t');
			}
			return values;
		}
	}
}