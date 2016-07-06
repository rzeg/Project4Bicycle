using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using Xamarin.Forms.Maps;
using System.Globalization;
using System.Reflection;

namespace Project4Bicycle
{

	public class BikeContainerViewModel
	{
		public ObservableCollection<BikeContainer> BikeContainers = new ObservableCollection<BikeContainer>();

		public BikeContainerViewModel()
		{

		}

		public async Task GetHaltesAsync()
		{
			var assembly = typeof(BikeContainerViewModel).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("Project4Bicycle.Data.pxxc2.csv");
			var reader = new StreamReader(stream);

			BikeContainerFactory factory = new BikeContainerFactory(reader);
			BikeContainer bikeContainer;

			while (factory.HasNext())
			{
				bikeContainer = factory.GetCurrent();
				BikeContainers.Add(bikeContainer);
			}
		}
	}
}

