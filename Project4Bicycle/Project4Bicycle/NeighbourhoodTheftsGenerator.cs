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
		Geocoder geoCoder = new Geocoder();


		public NeighbourhoodTheftsGenerator()
		{
		}


		public async Task<List<NeighbourhoodTheft>> GenerateNeighbourhoods()
		{
			//BikeTheftNeighbourhoodsModel btnm = new BikeTheftNeighbourhoodsModel();
			BikeTheftViewModel bvm = new BikeTheftViewModel();
			//List<Position> positions = new List<Position>();

			await bvm.GetBikeTheftsAsync();

			foreach (var neighbourhood in bvm.neighbourhoods)
			{
				NeighbourhoodTheft neighbourhoodTheft = new NeighbourhoodTheft();
				neighbourhoodTheft.Name = neighbourhood;
	

				neighbourhoods.Add(neighbourhoodTheft);
			}

			foreach (BikeTheft bikeTheft in bvm.BikeThefts)
			{

				NeighbourhoodTheft neighbourhoodTheft = neighbourhoods.Find(x => x.Name.Contains(bikeTheft.Neighbourhood));


				if (neighbourhoodTheft.Positions.Count == 0)
				{
					var possibleAddresses = await geoCoder.GetPositionsForAddressAsync(bikeTheft.City);

					foreach (var address in possibleAddresses)
					{
						neighbourhoodTheft.Positions.Add(address);
					}
				}



				if (neighbourhoodTheft.Positions.Count < 10)
				{
					//var possibleAddresses2 = await geoCoder.GetPositionsForAddressAsync(neighbourhoodTheft.Name + ", " + bikeTheft.Street);
					//foreach (var address in possibleAddresses2)
					//{
					//	neighbourhoodTheft.Positions.Add(address);
					//}
				}

				neighbourhoodTheft.AddNeighbourhood(bikeTheft.Neighbourhood);
			}

			return neighbourhoods;
		}

	}
}

