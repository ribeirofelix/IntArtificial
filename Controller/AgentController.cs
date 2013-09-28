using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class AgentController
    {
        private const int pop = 100 ;        
        private const int popElt = 50 ;
        private const int popMut = 50 ;
        private const int generations = 100;

        private MapController mapCont = new MapController();
        private BRKGA genetic ; 
        private bool[] captBadges = new bool[9];
        private Pokedex pokedex;

        public AgentController()
        {
            captBadges.Initialize();
            mapCont.UpdateDistances();
            genetic = new BRKGA(9,pop,popElt,popMut,captBadges,mapCont.DistMap);
        }

        public void Walk()
        {
            genetic.Evolve(generations);
            foreach (var badg in genetic.GetChoice())
            {
                
            }
        }


    }
}
