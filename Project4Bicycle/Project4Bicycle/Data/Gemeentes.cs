using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project4Bicycle.Data
{
    public class Gemeentes
    {
        public List<DeelGemeente> deelGemeentes = new List<DeelGemeente>();

        public Gemeentes()
        {
            var assembly = typeof(Gemeentes).GetTypeInfo().Assembly;
            string[] fields;
            Stream stream = assembly.GetManifestResourceStream("Project4Bicycle.Data.Stadsdelen.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                while ((text = reader.ReadLine()) != null)
                {
                    fields = text.Split('\t');
                    DeelGemeente deelGemeente = new DeelGemeente();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if(i == 0)
                        {
                            deelGemeente.Name = fields[0];
                        }
                        else
                        {
                            deelGemeente.AddWijk(fields[i]);
                        }
                    }
                    deelGemeentes.Add(deelGemeente);
                }
            }
        }
    }
}
