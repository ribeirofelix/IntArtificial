using MachineLearning.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MachineLearning
{
    class Program
    {

        int a;

        private const string pokefolder = @"..\..\PokeFiles";
        static Regex regFloat = new Regex(@"[0-9]+\.[0-9]+");
        static Regex regInt = new Regex(@"[0-9][0-9][0-9]");
        static PokeContext ct = new PokeContext();
           


        static void Main(string[] args)
        {        
            
            ParseHeight();          
        }
        
        static void ParseHeight()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeheight.txt"))).ReadToEnd();
            var heightGroups = file.Replace('\r',' ').Split( new string[]{ "footer" },StringSplitOptions.None);

            

            foreach (var group in heightGroups) // iterate over height groups
            {
                var lines = group.Split('\n');
                float height = 0 ;
                int skip = 0;
                foreach (var item in lines)
                {
                    skip++;
                    if (regFloat.IsMatch(item))
                    {
                        height = float.Parse(regFloat.Match(item).Value, new CultureInfo("en-US"));
                        break;
                    }
                }

                foreach (var line in lines.Skip(skip) ) // iterate over each line of this group
                {
                    var pokes = regInt.Matches(line);
                    foreach (Match pokeMatch in pokes)
                    {
                        var pokeId = int.Parse(pokeMatch.Value) ;
                        var poke = ct.Pokemons.Where(p => p.PokeId == pokeId ).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                        if (poke != null) // if the pokemons exists, we just set the height!
                            poke.Height = height;
                        else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                            ct.Pokemons.Add(new Pokemon() { PokeId = pokeId, Height = height });
                    }
                }
            }

            ct.SaveChanges();
        }

        static void ParseWeight()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeWeight.txt"))).ReadToEnd();
            file = file.Replace('\r', ' ');

            var pokeLine = regInt.Split(file);

            foreach (var poke in pokeLine)
            {
                // LEMBRAR: PORYGON-Z PARA PORYGONZ, NIDORANMACHO PARA NIDORANM, NIDORANFEMEA PARA NIDORANF
                float weight;
                String name;
                int i = 0;

                var pokeInf = poke.Split('|');

                name = pokeInf[0];

                weight = float.Parse(pokeInf[4]);

                int en = (int)Enum.GetValue(typeof(PokeNum), name);

                var pokeBase = ct.Pokemons.Where(p => p.PokeId == en).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                if (pokeBase != null) // if the pokemons exists, we just set the height!
                    pokeBase.Weight = weight;
                else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                    ct.Pokemons.Add(new Pokemon() { PokeId = en, Weight = weight });
            }
            ct.SaveChanges();
        }

   
    }

 
}
