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
    class Q3Generator
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
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            foreach(int month in months)
            {
                StackedData data = new StackedData();
                var monthC = containers.Where(item => item.Month == month);
                var monthT = thefts.Where(item => item.Month == month);
                foreach(var container in monthC)
                {
                    data.AddContainer(container);
                    data.Month = container.Month;
                }

                foreach (var theft in monthT)
                {
                    data.AddBikeThefts(theft);
                }
                sData.Add(data);
            }

            foreach(StackedData d in sData)
            {
                q3model.AddData(d);
            }

            return q3model;
        }
    }
}
