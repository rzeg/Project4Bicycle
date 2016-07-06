using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Project4Bicycle
{
	class NeighbourhoodTheft
	{
		public List<string> Neighbourhoods = new List<string>();
		public string Name { get; set; }
		public Position NE { get; set;}
		public Position ZW { get; set;}
		public List<Position> Positions = new List<Position>();
		public int Count { get { return Neighbourhoods.Count; } }


		public void AddNeighbourhood(string color)
		{
			Neighbourhoods.Add(color);
		}
	}
}