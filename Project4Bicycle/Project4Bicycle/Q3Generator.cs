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
            //Alle gemeentes in Rotterdam genereren voor gebruik in vergelijkingen.
            gemeentes = new Gemeentes();
        }

        public void SetNeighbourhood(String neighbourhood)
        {
            this.selectedNeighbourhood = neighbourhood;
        }

        //Methode om de data van de diefstallen en bikecontainers op te halen.
        public async Task LoadData()
        {
            vm = new BikeContainerViewModel();
            theftVM = new BikeTheftViewModel();
            await vm.GetHaltesAsync();
            await theftVM.GetBikeTheftsAsync();
            containers = vm.BikeContainers;
            thefts = theftVM.BikeThefts;
        }

        //Geeft een lijst terug van alle deelgemeentes
        public List<string> GetNeighbourhoodList()
        {
            var neighbourhoods = gemeentes.deelGemeentes.Select(item => item.Name).ToList();
            return neighbourhoods;
        }

        //Maakt het model dat de source voor de grafiek wordt.
        public Q3Model GenerateGraphModel()
        {
            Q3Model q3model = new Q3Model();
            List<StackedData> sData = new List<StackedData>();

            //Kies de juiste deelgemeente die is geselecteerd is uit de lijst van deelgemeentes.
            DeelGemeente deelgemeente = gemeentes.deelGemeentes.Single(item => item.Name == selectedNeighbourhood);

            for (int i = 1; i < 13; i++)
            {
                //Custom data structuur om de correcte data voor de grafiek te genereren
                StackedData data = new StackedData();

                //Zoek naar containers die in betreffende maand en gelijk is aan de naam van een deelgemeente.
                var monthC = containers.Where(item => item.Month == i).Where(item => item.Neighbourhood == deelgemeente.Name);
                //Selecteer alle diefstallen in de betreffende maand.
                var monthT = thefts.Where(item => item.Month == i);

                //Door alle diefstallen loopen en daarna de diefstallen toevoegen die overeenkomen met de geselecteerde deelgemeente/wijk
                foreach (var theft in monthT)
                {
                    if (deelgemeente.GetWijken().Any(item => item.StartsWith(theft.Neighbourhood, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        data.AddBikeThefts(theft);
                    }
                }

                //Door alle containers loopen en deze toevoegen aan de data.
                foreach (var container in monthC)
                {
                    data.AddContainer(container);
                }                
                
                //Maan genereren
                data.Month = new DateTime(2010, i, 1).ToString("MMM");
                sData.Add(data);
            }
            
            //De uiteindelijke data toevoegen aan het model voor de grafiek
            foreach(StackedData d in sData)
            {
                q3model.AddData(d);
            }

            //Model teruggeven voor gebruik in grafiek
            return q3model;
        }
    }
}
