using Project4Bicycle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace Project4Bicycle
{
	public class BrandColorGenerator
	{
		List<Color> colors = new List<Color>();
		List<Brand> brands = new List<Brand>();

		public BrandColorGenerator()
		{
		}


		public async Task<BikeBrandsGraphModel> GenerateBrands()
		{
			BikeBrandsGraphModel bbgm = new BikeBrandsGraphModel();

			BikeTheftViewModel bvm = new BikeTheftViewModel();
			await bvm.GetBikeTheftsAsync();

			foreach (var brand in bvm.brands)
			{
				Brand brandObject = new Brand();
				brandObject.Name = brand;
				brands.Add(brandObject);
			}

			foreach (BikeTheft bikeTheft in bvm.BikeThefts)
			{
				Brand brand = brands.Find(x => x.Name.Contains(bikeTheft.Brand));
				brand.AddColor(bikeTheft.Brand);
			}

			var filteredBrands = brands.OrderByDescending(x => x.Count).Take(10);

			foreach (Brand brand in filteredBrands)
			{
				bbgm.AddData(brand);
			}

			return bbgm;
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

			var filteredColors = colors.OrderByDescending(x => x.Count).Take(10);
			foreach (Color color in filteredColors)
			{
				bcgm.AddData(color);
			}

			return bcgm;
		}
	}
}
