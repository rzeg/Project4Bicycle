using Project4Bicycle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Project4Bicycle
{
	class BrandColorGenerator
	{
		List<Color> colors = new List<Color>();

		public BrandColorGenerator()
		{
		}


		public async Task<BikeColorsGraphModel> GenerateColors()
		{
			BikeColorsGraphModel bcgm = new BikeColorsGraphModel();
			BikeTheftViewModel bvm = new BikeTheftViewModel();
			await bvm.GetBikeTheftsAsync();

			foreach (var color in bvm.colors)
			{
				Color colorObject = new Color();
				colorObject.Name = color;
				colors.Add(colorObject);
			}


			foreach (BikeTheft bikeTheft in bvm.BikeThefts)
			{
				Color color = colors.Find(x => x.Name.Contains(bikeTheft.Color));
				color.AddColor(bikeTheft.Color);
			}

			foreach (Color color in colors)
			{
				bcgm.AddData(color);
			}

			return bcgm;
		}
	}
}
