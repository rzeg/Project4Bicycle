using System.Collections.Generic;

namespace Project4Bicycle.Models
{
    public class StackedData
    {
        public List<BikeContainer> BikeContainers = new List<BikeContainer>();
        public List<BikeTheft> BikeThefts = new List<BikeTheft>();
        public string Month { get; set; }
        public int BikeContainerCount { get { return BikeContainers.Count; } }
        public int BikeTheftCount { get { return BikeThefts.Count; } }


        public StackedData()
        {
            
        }

        public void AddContainer(BikeContainer container)
        {
            BikeContainers.Add(container);
        }

        public void AddBikeThefts(BikeTheft theft)
        {
            BikeThefts.Add(theft);
        }
    }
}