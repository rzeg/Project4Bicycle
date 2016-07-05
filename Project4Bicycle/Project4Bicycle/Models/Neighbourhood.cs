using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle.Models
{
    public class Neighbourhood
    {
        public List<BikeContainer> BikeContainers = new List<BikeContainer>();
        public string Name { get; set; }
        
        public int BikeContainerCount { get { return BikeContainers.Count; } }

        public void AddContainer(BikeContainer container)
        {
            BikeContainers.Add(container);
        }
    }
}
