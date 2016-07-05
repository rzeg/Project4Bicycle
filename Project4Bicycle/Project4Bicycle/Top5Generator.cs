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
    public class Top5Generator
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
                if(neighbourhoods.Add(container.Neighbourhood))
                {
                    Neighbourhood neighb = new Neighbourhood();
                    neighb.Name = container.Neighbourhood;
                    realNeighbourhoods.Add(neighb);
                }

                Neighbourhood nb = realNeighbourhoods.Find(x => x.Name.Contains(container.Neighbourhood));
                nb.AddContainer(container);
            }

            var r = realNeighbourhoods.OrderByDescending(x => x.BikeContainerCount).Take(5);

            foreach (Neighbourhood nb in r)
            {
                if(nb.Name.Length > 10)
                    nb.Name = nb.Name.Substring(0, 10);
                bgm.AddData(nb);
            }

            return bgm;
        }
    }
}
