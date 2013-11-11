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
        static Regex regInt = new Regex(@"[0-9][0-9][0-9]([A-Z])?");
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
                        var pokeId = pokeMatch.Value ;
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

   
    }

 
}
