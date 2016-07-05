using Project4Bicycle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle.Models
{
	public class BikeBrandsGraphModel : GraphModel<Brand>
	{
		public BikeBrandsGraphModel()
		{
			model = new ObservableCollection<Brand>();
		}
	}
}

