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
        public float Height { get; set; }
        public float Widht { get; set; }
        public float Weight { get; set; }

        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
        public int Total { get; set; }
        public int Avarage { get; set; }

        public String Color { get; set; }

        public PokeType Type { get; set; }
        public PokeBody Body { get; set; }
    }
}
