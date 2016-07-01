using System;
using System.IO;

namespace Project4Bicycle
{

	interface TraditionalIterator<T>
	{
		T GetCurrent();
		bool HasNext();
		void MoveNext();
	}


	class BikeTheftFactory : TraditionalIterator<BikeTheft>
	{
		StreamReader reader;

		public BikeTheftFactory(StreamReader reader)
		{
			this.reader = reader;
		}

		public BikeTheft GetCurrent()
		{
			reader.ReadLine();

			BikeTheft bikeTheft = new BikeTheft();
			//bikeTheft.
			return bikeTheft;
		}

		public bool HasNext()
		{
			return !reader.EndOfStream;
		}

		public void MoveNext()
		{
			
		}
	}
}