using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Properties;

namespace Model
{
    public class Badge
    {

        private BadgeTypes _type;

      

        public Image BadgeImage
        {
            get
            {
              
                    if (_type == BadgeTypes.boulder) return Resources.boulder;
                    if (_type == BadgeTypes.cascade) return Resources.cascade;
                    if (_type == BadgeTypes.earth) return Resources.earth;
                    if (_type == BadgeTypes.marsh) return Resources.marsh;
                    if (_type == BadgeTypes.rainbow) return Resources.rainbow;
                    if (_type == BadgeTypes.soul) return Resources.soul;
                    if (_type == BadgeTypes.thunder) return Resources.thunder;
                    if (_type == BadgeTypes.volcano) return Resources.volcano;


                return null;
            }

        }

        public Badge(BadgeTypes type)
        {
            _type = type;
        }

    }
}
