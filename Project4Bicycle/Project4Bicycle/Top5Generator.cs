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

        //async methode om neighbourhoods met containers te genereren.
        public async Task<BikeGraphModel> GenerateNeighbourhoods()
        {
            //Een nieuwe bikegraphmodel aanmaken die gebruikt wordt om data in de grafiek te zetten
            BikeGraphModel bgm = new BikeGraphModel();
            BikeContainerViewModel vm = new BikeContainerViewModel();
            await vm.GetHaltesAsync();
            ObservableCollection<BikeContainer> containers = vm.BikeContainers;
            foreach (BikeContainer container in containers)
            {
                //Als er een unieke neighbourhood naam is gevonden wordt deze aan de hashset toegevoegd
                if(neighbourhoods.Add(container.Neighbourhood))
                {
                    Neighbourhood neighb = new Neighbourhood();
                    neighb.Name = container.Neighbourhood;
                    realNeighbourhoods.Add(neighb);
                }

                //Hier wordt de bikecontainer toegevoegd aan de bijbehorende neighbourhood
                Neighbourhood nb = realNeighbourhoods.Find(x => x.Name.Contains(container.Neighbourhood));
                nb.AddContainer(container);
            }

            //De top 5 selecteren van de neighbourhoods.
            var r = realNeighbourhoods.OrderByDescending(x => x.BikeContainerCount).Take(5);

            //Door de top 5 loopen en toevoegen aan het model.
            foreach (Neighbourhood nb in r)
            {
                //Naam afkorten anders past deze niet op de grafiek.
                if(nb.Name.Length > 10)
                    nb.Name = nb.Name.Substring(0, 10);
                bgm.AddData(nb);
            }

            return bgm;
        }
    }
}
