using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle.Data
{
    public class DeelGemeente
    {
        public string Name { get; set; }
        private List<string> Wijken = new List<string>();

        public void AddWijk(string wijk)
        {
            Wijken.Add(wijk);
        }

        public List<string> GetWijken()
        {
            return Wijken;
        }
    }
}
