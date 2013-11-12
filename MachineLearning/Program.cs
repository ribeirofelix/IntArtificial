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
        static Regex regInt = new Regex(@"[0-9][0-9][0-9][^\.]");
        static Regex regInt2 = new Regex(@"[0-9][0-9][0-9]");
        static PokeContext ct = new PokeContext();
           


        static void Main(string[] args)
        {        
            
         //   ParseHeight();
           // ParseBodies();
           // ParseStatsBase();
         //   ParseWeight();
          //  ParseColor();
         //   ParseHabitat();
           //ParseCatchRate();
            //ParsePerformance();
            ParseAbility();
           ct.SaveChanges();
            
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
                        var pokeId = int.Parse( pokeMatch.Value );

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
        
        static void ParseBodies()
        {

            string file = (new StreamReader(Path.Combine(pokefolder, "pokeBodies.txt"))).ReadToEnd();
            var bodiesGroups = file.Replace('\r', ' ').Replace('\n',' ').Split(new string[] { "type:" }, StringSplitOptions.None);

            foreach (var group in bodiesGroups) // iterate over height groups
            {
                string type = (new String( group.TakeWhile(c => c != '|').ToArray() )).Trim();

                var pokes = regInt.Matches(group);
                foreach (Match pokeMatch in pokes)
                {
                    var pokeId = int.Parse( pokeMatch.Value);
                    var poke = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                    if (poke != null) // if the pokemons exists, we just set the bodytype !
                        poke.Body = (PokeBody) Enum.Parse(typeof(PokeBody),type);
                    else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                        ct.Pokemons.Add(new Pokemon() { PokeId = pokeId, Body = (PokeBody) Enum.Parse(typeof(PokeBody),type) });

                    ct.SaveChanges();
                }

             
            }

            
           
        }


        static void ParseStatsBase()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeStats.txt"))).ReadToEnd();
            var pokes = file.Replace('\r', ' ').Split('\n');

            foreach (var poke in pokes)
            {
                var infos = poke.Split('|');
                int pokeId;
                int idInx = 0;
                int hpInx = 2;
                int attkInx = 3;
                int defInx = 4;
                int spAttkInx = 5;
                int spDefInx = 6;
                int speedInx = 7;
                
                if (int.TryParse(infos[0], out pokeId))// sucesso no parse! é um pokemon valido, sem letra no final
                {
                   
                    var pokeBase = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                    if (pokeBase != null) // if the pokemons exists, we just set the bodytype !
                    {
                        pokeBase.BaseHp = int.Parse(infos[hpInx]);
                        pokeBase.BaseAttack = int.Parse(infos[attkInx]);
                        pokeBase.BaseDefense = int.Parse(infos[defInx]);
                        pokeBase.BaseSpAttack = int.Parse(infos[spAttkInx]);
                        pokeBase.BaseSpDefense = int.Parse(infos[spDefInx]);
                        pokeBase.BaseSpeed = int.Parse(infos[speedInx]);
                      
                    }
                    else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                    {
                        pokeBase = new Pokemon();
                        pokeBase.PokeId = pokeId;
                        pokeBase.BaseHp = int.Parse(infos[hpInx]);
                        pokeBase.BaseAttack = int.Parse(infos[attkInx]);
                        pokeBase.BaseDefense = int.Parse(infos[defInx]);
                        pokeBase.BaseSpAttack = int.Parse(infos[spAttkInx]);
                        pokeBase.BaseSpDefense = int.Parse(infos[spDefInx]);
                        pokeBase.BaseSpeed = int.Parse(infos[speedInx]);

                    }
                    ct.SaveChanges();
                }
            }
        }


        static void ParseWeight()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeWeight.txt"))).ReadToEnd();
            file = file.Replace('\r', ' ');

            var pokeLine = regInt.Split(file);

            foreach (var poke in pokeLine)
            {
                if (poke == "" || poke == "|")
                    continue;
                // LEMBRAR: PORYGON-Z PARA PORYGONZ, NIDORANMACHO PARA NIDORANM, NIDORANFEMEA PARA NIDORANF
                float weight;
                String name;
                int i = 0;

                var pokeInf = poke.Split('|');

                name = pokeInf[0] == "" ? pokeInf[1] : pokeInf[0] ;

                weight = float.Parse( regFloat.Match( pokeInf[4] ).Value, new CultureInfo("en-US") );

                int en = (int)Enum.Parse(typeof(PokeNum), name);

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
                if (group == "") continue;
                var lines = group.Split('\n');
                String color = lines[0].Trim() == "" ? lines[1].Trim() : lines[0].Trim();
                int init = lines[0].Trim() == "" ? 2 : 1;
                for (int i = init; i < lines.Length; i = i + 3)
                {
                    var pokes = regInt2.Matches(lines[i]);
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
            ct.SaveChanges();

        }

        static void ParseHabitat()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeHabitat.txt"))).ReadToEnd();
            var habitatGroups = file.Replace('\r', ' ').Split(new string[] { "END" }, StringSplitOptions.None);

            foreach (var group in habitatGroups) // iterate over habitat groups
            {
                var lines = group.Split('\n');
                String habitat = lines[0].Trim() == "" ? lines[1].Trim() : lines[0].Trim();
                int init = lines[0].Trim() == "" ? 2 : 1;

                for (int i = init; i < lines.Length-1; i++)
                {
                    if (lines[i][0] == '{')
                    {
                        var pokes = regInt2.Matches(lines[i]);
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
            ct.SaveChanges();

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

        static void ParsePerformance()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokePerformance.txt"))).ReadToEnd().Replace('\r',' ');
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
                    pokeBase.PerfJump = jump;
                    pokeBase.PerfPower = power;
                    pokeBase.PerfSkill = skill;
                    pokeBase.PerfSpeed = speed;
                    pokeBase.PerfStamina = stamina;
                }
                else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                    ct.Pokemons.Add(new Pokemon() 
                        { PokeId = pokeId,
                          PerfJump = jump,
                          PerfPower = power,
                          PerfSkill = skill,
                          PerfStamina = stamina,
                          PerfSpeed = speed
                        });

                
            }
        }

        static void ParseAbility()
        {
            string file = (new StreamReader(Path.Combine(pokefolder, "pokeAbility.txt"))).ReadToEnd().Replace('\r',' ');
            var lines = file.Split('\n');

            foreach (var line in lines)
            {
                var item = line.Split('|');
                int pokeId;
                String ability1, ability2, hidden;

                if (!int.TryParse(item[0], out pokeId))
                    continue;
                ability1 = item[2];
                ability2 = item[3];
                hidden = item[4];

                var pokeBase = ct.Pokemons.Where(p => p.PokeId == pokeId).FirstOrDefault(); // Doing a query to find the pokemon with this ID
                if (pokeBase != null) // if the pokemons exists, we just set the ability!
                {
                    pokeBase.Ability1 = ability1 == string.Empty ? null : ability1 ;
                    pokeBase.Ability2 = ability2 == string.Empty ? null : ability2;
                    pokeBase.Hidden = hidden == string.Empty ? null : hidden;
                }
                else // if the variabel is null, then the pokemon doenst exists in the database, let's create it!
                    ct.Pokemons.Add(new Pokemon()
                    {
                        PokeId = pokeId,
                        Ability1 = ability1 == string.Empty ? null : ability1,
                        Ability2 = ability2 == string.Empty ? null : ability2,
                        Hidden = hidden == string.Empty ? null : hidden 

                    });

            }

        }
    }

 
}
