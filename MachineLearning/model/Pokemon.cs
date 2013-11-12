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

        public float Height { get; set; }
        public float Widht { get; set; }
        public float Weight { get; set; }

        
        /*Performance stats*/
        public int PerfSpeed { get; set; }
        public int PerfPower { get; set; }
        public int PerfSkill { get; set; }
        public int PerfStamina { get; set; }
        public int PerfJump { get; set; }
        public int PerfTotal{ get; set; }
        public int PerfAvg { get; set; }
        /* Final - Performance stats */
        
       /* Base stats */
        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseSpAttack { get; set; }
        public int BaseSpDefense { get; set; }
        public int BaseSpeed { get; set; }
        public int BaseTotal { get; set; }
        public float BaseAvarage { get; set; }
        /* Final - Base stats */
        
        public string Color { get; set; }

        public string Habitat { get; set; }
        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string Hidden { get; set; }

        public PokeType Type { get; set; }
        public PokeBody Body { get; set; }
    }


    
}
