using Controller.Properties;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class MapController
    {
        private Map _kantoMap;

        public ICollection<ICollection<Tile>> KantoMap
        {
            get 
            {
                if (_kantoMap == null)
                    _kantoMap = new Map(Resources.mapPath , Resources.pokePath);
                return _kantoMap.KantoMap; 
            }
        }


    }
}
