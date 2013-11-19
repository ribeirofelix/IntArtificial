using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System;

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

        public string Exp { get; set; }

        private void foo()
        {
            var p = new Pokemon();
            var a = default(Pokemon).Type;
            
        }

        public override string ToString()
        {
            var cult = new CultureInfo("en-US");
            return Height.ToString(cult) + "," +
                    Weight.ToString(cult) + "," +
                    Body + "," +
                    CatchRate + "," +
                    (PerfSpeed == 0 ? "?" : PerfSpeed.ToString() )+ "," +
                    (PerfPower == 0 ? "?" : PerfPower.ToString())+ "," +
                    (PerfSkill == 0 ? "?" : PerfSkill.ToString())+ "," +
                    (PerfStamina == 0 ? "?" : PerfStamina.ToString())+ "," +
                    (PerfJump == 0 ? "?" : PerfJump.ToString())+ "," +
                    BaseHp + "," +
                    BaseAttack + "," +
                    BaseDefense + "," +
                    BaseSpAttack + "," +
                    BaseSpDefense + "," +
                    BaseSpeed + "," +
                    (Color == null ? "?" : (Color.Trim() == "" ? "?" : "\"" + Color + "\"")) + "," +
                    (Habitat == null ? "?" : (Habitat.Trim() == "" ? "?" : "\"" + Habitat + "\"")) + "," +
                    (Ability1 == null ? "?" : (Ability1.Trim() == "" ? "?" : "\""+ Ability1 +"\"")) + "," +
                    (Ability2 == null ? "?" : (Ability2.Trim() == "" ? "?" : "\"" + Ability2 + "\"")) + "," +
                    (Hidden == null ? "?" : (Hidden.Trim() == "" ? "?" : "\"" + Hidden + "\"")) + "," +
                    (Exp) + ","+
                    (Type);
        }

        public string ToString(bool numeric)
        {
            if (!numeric)
                return ToString();
            var cult = new CultureInfo("en-US");
            return Height.ToString(cult) + "," +
                    Weight.ToString(cult) + "," +
                    (int)Body + "," +
                    CatchRate + "," +
                    (PerfSpeed == 0 ? "?" : PerfSpeed.ToString()) + "," +
                    (PerfPower == 0 ? "?" : PerfPower.ToString()) + "," +
                    (PerfSkill == 0 ? "?" : PerfSkill.ToString()) + "," +
                    (PerfStamina == 0 ? "?" : PerfStamina.ToString()) + "," +
                    (PerfJump == 0 ? "?" : PerfJump.ToString()) + "," +
                    BaseHp + "," +
                    BaseAttack + "," +
                    BaseDefense + "," +
                    BaseSpAttack + "," +
                    BaseSpDefense + "," +
                    BaseSpeed + "," +
                    (Color == null ? "?" : (Color.Trim() == "" ? "?" :   ( (int) Enum.Parse(typeof(Color),Color.Replace(" ","_") ) ).ToString() )) + "," +
                    (Habitat == null ? "?" : (Habitat.Trim() == "" ? "?" : ( (int) Enum.Parse(typeof(Habitat), Habitat.Replace(" ", "_"))).ToString() )) + "," +
                    (Ability1 == null ? "?" : (Ability1.Trim() == "" ? "?" : ((int) Enum.Parse(typeof(Ability1), Ability1.Replace(" ", "_"))).ToString()  )) + "," +
                    (Ability2 == null ? "?" : (Ability2.Trim() == "" ? "?" : ( (int) Enum.Parse(typeof(Ability2), Ability2.Replace(" ", "_"))).ToString() )) + "," +
                    (Hidden == null ? "?" : (Hidden.Trim() == "" ? "?" :( (int) Enum.Parse(typeof(Hidden), Hidden.Replace(" ", "_"))).ToString() ) ) + "," +
                    (int)(Type);

        }
    
    }


    
}
