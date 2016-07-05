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
    public class Q3Generator
    {
        List<StackedData> sData = new List<StackedData>();
        public string selectedNeighbourhood;

        public Q3Generator(string Neighbourhood)
        {
            this.selectedNeighbourhood = Neighbourhood;
        }

        public async Task<Q3Model> GenerateGraphModel()
        {
            Q3Model q3model = new Q3Model();
            BikeContainerViewModel vm = new BikeContainerViewModel();
            BikeTheftViewModel theftVM = new BikeTheftViewModel();
            await vm.GetHaltesAsync();
            await theftVM.GetBikeTheftsAsync();
            ObservableCollection<BikeContainer> containers = vm.BikeContainers;
            ObservableCollection<BikeTheft> thefts = theftVM.BikeThefts;

            for (int i = 1; i < 11; i++)
            {
                if(containers.Any(item => item.Neighbourhood.Contains(selectedNeighbourhood) && item.Neighbourhood.Contains(selectedNeighbourhood)))
                {
                    StackedData data = new StackedData();
                    var monthC = containers.Where(item => item.Month == i);
                    var monthT = thefts.Where(item => item.Month == i);
                    foreach (var container in monthC)
                    {
                        data.AddContainer(container);
                    }

                    foreach (var theft in monthT)
                    {
                        data.AddBikeThefts(theft);
                    }
                    data.Month = i;
                    sData.Add(data);
                }
                
            }

            foreach(StackedData d in sData)
            {
                q3model.AddData(d);
            }

            return q3model;
        }
    }
}
