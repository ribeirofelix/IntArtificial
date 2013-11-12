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

        static void ParseColor()
        {

            string file = (new StreamReader(Path.Combine(pokefolder, "pokeColor.txt"))).ReadToEnd();
            var colorGroups = file.Replace('\r', ' ').Split(new string[] { "END" }, StringSplitOptions.None);

            foreach (var group in colorGroups) // iterate over color groups
            {
                var lines = group.Split('\n');
                String color = lines[0];

                for (int i = 1; i < lines.Length; i = i + 3)
                {
                    var pokes = regInt.Matches(lines[i]);
                    foreach (Match pokeMatch in pokes)
                    {
                        var pokeId = int.Parse(pokeMatch.Value);
                        var poke = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                        if (poke != null) // if the pokemons exists, we just set the color!
                            poke.Color = color;
                        else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                            ct.Pokemons.Add(new Pokemon() { PokeId = pokeId, Color = color });
                    }
                }
            }

        }

        static void ParseHabitat()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeHabitat.txt"))).ReadToEnd();
            var habitatGroups = file.Replace('\r', ' ').Split(new string[] { "END" }, StringSplitOptions.None);

            foreach (var group in habitatGroups) // iterate over habitat groups
            {
                var lines = group.Split('\n');
                String habitat = lines[0];

                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i][0] == '{')
                    {
                        var pokes = regInt.Matches(lines[i]);
                        foreach (Match pokeMatch in pokes)
                        {
                            var pokeId = int.Parse(pokeMatch.Value);
                            var poke = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                            if (poke != null) // if the pokemons exists, we just set the habitat!
                                poke.Habitat = habitat;
                            else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                                ct.Pokemons.Add(new Pokemon() { PokeId = pokeId, Habitat = habitat });
                        }
                    }           
                }
            }

        }

        static void ParseCatchRate()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeCatch.txt"))).ReadToEnd();
            var lines = file.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                int pokeId, catchRate;

                if (i % 5 == 0)
                {
                    pokeId = int.Parse(lines[i]);
                    catchRate = int.Parse(lines[i + 3]);

                    var pokeBase = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                    if (pokeBase != null) // if the pokemons exists, we just set the catchRate!
                        pokeBase.CatchRate = catchRate;
                    else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                        ct.Pokemons.Add(new Pokemon() { PokeId = pokeId, CatchRate = catchRate });
                }
            }

        }

        static void ParseCatchRate()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokePerformance.txt"))).ReadToEnd();
            var lines = file.Split('\n');

            foreach (var line in lines)
            {
                var item = line.Split('|');

                int pokeId, speed, power, skill, stamina, jump ;

                pokeId = int.Parse(item[0]);
                speed = int.Parse(item[2]) ;
                power = int.Parse(item[3]);
                skill = int.Parse(item[4]) ;
                stamina = int.Parse(item[5]);
                jump = int.Parse(item[6]) ;

                var pokeBase = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                if (pokeBase != null) // if the pokemons exists, we just set the performance!
                {
                    pokeBase.Jump = jump;
                    pokeBase.Power = power;
                    pokeBase.Skill = skill;
                    pokeBase.Speed = speed;
                    pokeBase.Stamina = stamina;
                }
                else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                    ct.Pokemons.Add(new Pokemon() { PokeId = pokeId, Jump = jump, Power = power,
                                                    Skill =skill, Stamina = stamina, Speed = speed});

                
            }
        }

        static void ParseAbility()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeAbility.txt"))).ReadToEnd();
            var lines = file.Split('\n');

            foreach (var line in lines)
            {
                var item = line.Split('|');
                int pokeId;
                String ability1, ability2, hidden;

                pokeId = int.Parse(item[0]);
                ability1 = item[2];
                ability2 = item[3];
                hidden = item[4];

                var pokeBase = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                if (pokeBase != null) // if the pokemons exists, we just set the ability!
                {
                    pokeBase.Ability1 = ability1;
                    pokeBase.Ability2 = ability2;
                    pokeBase.Hidden = hidden;
                }
                else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                    ct.Pokemons.Add(new Pokemon()
                    {
                        PokeId = pokeId,
                        Ability1 = ability1,
                        Ability2 = ability2,
                        Hidden = hidden 

                    });

            }

        }
    }

 
}
