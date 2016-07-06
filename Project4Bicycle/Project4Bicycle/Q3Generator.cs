using Project4Bicycle.Data;
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
        public string selectedNeighbourhood;
        ObservableCollection<BikeContainer> containers;
        ObservableCollection<BikeTheft> thefts;
        BikeContainerViewModel vm;
        BikeTheftViewModel theftVM;
        Gemeentes gemeentes;

        public Q3Generator()
        {
            gemeentes = new Gemeentes();
        }

        public void SetNeighbourhood(String neighbourhood)
        {
            this.selectedNeighbourhood = neighbourhood;
        }

        public async Task LoadData()
        {
            vm = new BikeContainerViewModel();
            theftVM = new BikeTheftViewModel();
            await vm.GetHaltesAsync();
            await theftVM.GetBikeTheftsAsync();
            containers = vm.BikeContainers;
            thefts = theftVM.BikeThefts;
        }

        public List<string> GetNeighbourhoodList()
        {
            var neighbourhoods = gemeentes.deelGemeentes.Select(item => item.Name).ToList();
            return neighbourhoods;
        }

        public Q3Model GenerateGraphModel()
        {
            Q3Model q3model = new Q3Model();
            List<StackedData> sData = new List<StackedData>();
            DeelGemeente deelgemeente = gemeentes.deelGemeentes.Single(item => item.Name == selectedNeighbourhood);

            for (int i = 1; i < 13; i++)
            {
                StackedData data = new StackedData();
                var monthC = containers.Where(item => item.Month == i).Where(item => item.Neighbourhood == deelgemeente.Name);
                var monthT = thefts.Where(item => item.Month == i);
                foreach (var theft in monthT)
                {
                    if (deelgemeente.GetWijken().Any(item => item.StartsWith(theft.Neighbourhood, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        data.AddBikeThefts(theft);
                    }
                }

                foreach (var container in monthC)
                {
                    data.AddContainer(container);
                }                
              
                data.Month = new DateTime(2010, i, 1).ToString("MMM");
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
