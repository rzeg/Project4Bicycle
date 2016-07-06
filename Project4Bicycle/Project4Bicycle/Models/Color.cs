
using System;
using System.Collections.Generic;

namespace Project4Bicycle
{
	public class Color
	{
		public List<string> BikeColors = new List<string>();
		public string Name { get; set; }
		public int Count { get { return BikeColors.Count; } }


		public void AddColor(string color)
		{
			BikeColors.Add(color);
		}
	}
}

