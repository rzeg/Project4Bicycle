using System.Collections.Generic;

namespace Project4Bicycle
{
	public class Brand
	{
		public List<string> BikeBrands = new List<string>();
		public string Name { get; set; }
		public int Count { get { return BikeBrands.Count; } }

		public void AddColor(string color)
		{
			BikeBrands.Add(color);
		}
	}
}