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
        public string PokeId { get; set; }
        public float Height { get; set; }
        public float Widht { get; set; }
        public float Weight { get; set; }
        public PokeType Type { get; set; }
    }
}
