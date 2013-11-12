using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MachineLearning.model
{
    public class Pokemon
    {       
        public int Id { get; set; }
        public int PokeId { get; set; }
        public int CatchRate { get; set; }
        public int Speed { get; set; }
        public int Power { get; set; }
        public int Skill { get; set; }
        public int Stamina { get; set; }
        public int Jump { get; set; }
        public float Height { get; set; }
        public float Widht { get; set; }
        public float Weight { get; set; }
        public String Color { get; set; }
        public String Habitat { get; set; }
        public String Ability1 { get; set; }
        public String Ability2 { get; set; }
        public String Hidden { get; set; }
        public PokeType Type { get; set; }
    }
}
