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
            //Vanuit een text file alle Gemeentes selecteren met de daarbij behorende deelgemeentes/wijken.
            //Informatie hierover is deels van Wikipedia en deels op basis van de data die we al hebben.
            var assembly = typeof(Gemeentes).GetTypeInfo().Assembly;
            string[] fields;
            //Xamarin gebruikt dit om cross-platform een file uit te lezen.
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
                            //Eerste veld in bestand is de naam van de deelgemeente
                            deelGemeente.Name = fields[0];
                        }
                        else
                        {
                            //De rest van de velden zijn wijken in de deelgemeente
                            deelGemeente.AddWijk(fields[i]);
                        }
                    }
                    //Voeg deelgemeente toe aan de lijst van deelgemeentes.
                    deelGemeentes.Add(deelGemeente);
                }
            }
        }
    }
}
