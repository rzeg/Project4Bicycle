using Project4Bicycle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle
{
    class Top5Generator
    {
        HashSet<string> neighbourhoods = new HashSet<string>();
        List<Neighbourhood> realNeighbourhoods = new List<Neighbourhood>();
        public HashSet<string> Neighbourhoods { get { return neighbourhoods; } }

        public Top5Generator()
        {
        
        }

        public async Task<BikeGraphModel> GenerateNeighbourhoods()
        {
            BikeGraphModel bgm = new BikeGraphModel();
            BikeContainerViewModel vm = new BikeContainerViewModel();
            await vm.GetHaltesAsync();
            ObservableCollection<BikeContainer> containers = vm.BikeContainers;
            foreach (BikeContainer container in containers)
            {
                neighbourhoods.Add(container.Neighbourhood);
            }

            foreach(string nb in neighbourhoods)
            {
                Neighbourhood neighb = new Neighbourhood();
                neighb.Name = nb;
                realNeighbourhoods.Add(neighb);
            }

            
            foreach(BikeContainer container in containers)
            {
               Neighbourhood nb = realNeighbourhoods.Find(x => x.Name.Contains(container.Neighbourhood));
               nb.AddContainer(container);
            }

            var r = realNeighbourhoods.OrderByDescending(x => x.BikeContainerCount).Take(5);

            foreach (Neighbourhood nb in r)
                bgm.AddData(nb);

            return bgm;
        }
    }
}
