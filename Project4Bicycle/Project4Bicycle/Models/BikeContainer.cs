using System;


namespace Project4Bicycle
{
	public class BikeContainer
	{
		public int Id { get; set; }
		public string ID { get; set; }
		public string Description { get; set; }
		public string Street { get; set; }
		public string Neighbourhood { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }
        public int Month { get; internal set; }
		public int Distance { get; set; }
    }
}

