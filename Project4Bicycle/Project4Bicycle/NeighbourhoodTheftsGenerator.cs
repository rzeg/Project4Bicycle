using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Project4Bicycle.Models;
using Xamarin.Forms.Maps;

namespace Project4Bicycle
{
	class NeighbourhoodTheftsGenerator
	{

		List<NeighbourhoodTheft> neighbourhoods = new List<NeighbourhoodTheft>();

		public NeighbourhoodTheftsGenerator()
		{
		}


		public async Task<List<NeighbourhoodTheft>> GenerateNeighbourhoods()
		{
			BikeTheftNeighbourhoodsModel btnm = new BikeTheftNeighbourhoodsModel();
			BikeTheftViewModel bvm = new BikeTheftViewModel();
			List<Position> positions = new List<Position>();
			Geocoder geoCoder = new Geocoder();

			await bvm.GetBikeTheftsAsync();

			foreach (var neighbourhood in bvm.neighbourhoods)
			{

				NeighbourhoodTheft neighbourhoodTheft = new NeighbourhoodTheft();
				neighbourhoodTheft.Name = neighbourhood;
				var possibleAddresses = await geoCoder.GetPositionsForAddressAsync("Rotterdam " + neighbourhood);

				int i = 0;
				foreach (var address in possibleAddresses)
				{
					if (i == 0)
						neighbourhoodTheft.NE = address;
					else
						neighbourhoodTheft.ZW = address;
					i++;
				}

				neighbourhoods.Add(neighbourhoodTheft);
			}

			foreach (BikeTheft bikeTheft in bvm.BikeThefts)
			{
				NeighbourhoodTheft neighbourhoodTheft = neighbourhoods.Find(x => x.Name.Contains(bikeTheft.Neighbourhood));
				if (neighbourhoodTheft.ZW.Latitude == 0)
				{
					var possibleAddresses = await geoCoder.GetPositionsForAddressAsync(bikeTheft.City + " " + bikeTheft.Street);
					foreach (var address in possibleAddresses)
					{
						neighbourhoodTheft.ZW = address;
					}
				}
				neighbourhoodTheft.AddNeighbourhood(bikeTheft.Neighbourhood);
			}

			return neighbourhoods;
		}

	}
}

