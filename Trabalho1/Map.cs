using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model
{
    public class Map
    {
        #region /* PRIVATE PROPERTIES */

        private const int MAXTILES = 42;
        public Badge[] badges = new Badge[8];
        public Helper.Point[] ashAndBdgsPos = new Helper.Point[9];
        public Pokedex pokedex;
        private Tile[][] _map = new Tile[MAXTILES][];// Enumerable.Repeat<Tile[]>(new Tile[MAXTILES],MAXTILES).ToArray();

        public ICollection<ICollection<Tile>> KantoMap

        {
            get
            {
                return _map;
            }
        }

        private Helper.Point _ashIndex;
        public Helper.Point AshIndex
        {
            get
            {
                return _ashIndex;
            }
            set
            {
                GetTile(value.x, value.y).Ash = GetTile(_ashIndex.x, _ashIndex.y).Ash;
                _ashIndex = value;

            }
        }
       
        #endregion

        #region /* PRIVATE METHODS */

        #region ReadMap

        /* Reads a 42 x 42 map from a text file.
         * 
         * Parameter: mapFile - name of a text file   
         */
        private void ReadMap(string mapFile)
        {

            StreamReader st = null;

            /* Try to open a text file */
            try
            {
                st = new System.IO.StreamReader(mapFile);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }


            

            /* Read each line of a text file that defines a map. */
            for (int j = 0; j < 42; j++)
            {
                /* Read line */
                string line = st.ReadLine();
                _map[j] = new Tile[MAXTILES];
                /* Creates tiles */
                for (int k = 0; k < line.Length; k++)
                {
                    
                    _map[j][k] = new Tile(line[k] , j, k);
                }
            }

            /* Close files */
            st.Close();
        }

        #endregion

        #region ReadPokemons

        /* Reads pokemons from a text file.
         * 
         * Parameter: pokemonFile - name of a text file   
         */

        private void ReadPokemons(string pokemonFile)
        {
            StreamReader st = null;

            /* Try to open a text file */
            try
            {
                st = new System.IO.StreamReader(pokemonFile);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

           

            for (int i = 0; i < 48; i++)
            {
                /* Read tuple <type, x, y> */
                var line = st.ReadLine().Split(',');
                char t = char.Parse(line[0]);
                int x = int.Parse(line[1]);
                int y = int.Parse(line[2]);

                /* If random position */
                if (x == -1 && y == -1)
                {
                    /* Random object */
                    Random random = new Random(DateTime.Now.Millisecond.GetHashCode());
                    var temLugar = this._map.Any(a => a.Where(b => !b.HasPokemon && !b.HasAsh && !b.HasBadge && b.TileType == TileTypes.Grass).Count() > 0);
                    if(temLugar)
                    do
                    {
                        x = random.Next(0, 42);
                        y = random.Next(0, 42);

                    } while (GetTile(x, y).TileType != TileTypes.Grass ||
                       GetTile(x, y).HasPokemon ||
                       GetTile(x, y).HasBadge||
                       GetTile(x, y).HasAsh );
                    
                }

                /* Create pokemon */

                Pokemon _pokemon = new Pokemon(t);

                /* Assign pokemon */
                GetTile(x, y).Pokemon = _pokemon;
            }
        }

        #endregion

        #region PositionBadges

        /* Position badges in a map. */

        private void PositionBadges()
        {
            GetTile(2, 4).Badge = new Badge(BadgeTypes.soul); //soul - Koga - veneno
            ashAndBdgsPos[(int)BadgeTypes.soul] = new Helper.Point(2, 4);

            GetTile(4, 36).Badge = new Badge(BadgeTypes.volcano); //volcano - Blaine - fogo
            ashAndBdgsPos[(int)BadgeTypes.volcano] = new Helper.Point(4, 36);
            
            GetTile(2, 19).Badge = new Badge(BadgeTypes.thunder); //thunder - Ten Surge - eletrico
            ashAndBdgsPos[(int)BadgeTypes.thunder] = new Helper.Point(2, 19);
            
            GetTile(40, 32).Badge = new Badge(BadgeTypes.boulder); //boulder - Brock - pedra
            ashAndBdgsPos[(int)BadgeTypes.boulder] = new Helper.Point(40, 32);
            
            GetTile(22, 2).Badge = new Badge(BadgeTypes.rainbow); //rainbow - Erika - planta
            ashAndBdgsPos[(int)BadgeTypes.rainbow] = new Helper.Point(22, 2);
            
            GetTile(20, 39).Badge = new Badge(BadgeTypes.earth); //earth - Giovanni - terra
            ashAndBdgsPos[(int)BadgeTypes.earth] = new Helper.Point(20, 39);
            
            GetTile(19, 14).Badge = new Badge(BadgeTypes.cascade); //cascade - Misty - agua
            ashAndBdgsPos[(int)BadgeTypes.cascade] = new Helper.Point(19, 14);
            
            GetTile(37, 19).Badge = new Badge(BadgeTypes.marsh); //marsh - Sabrina - psiquico
            ashAndBdgsPos[(int)BadgeTypes.marsh] = new Helper.Point(37, 19);


        }

        #endregion

        #region PositionAsh

        /* Position Ash in a map. */

        private void PositionAsh()
        {
            GetTile(19, 24).Ash = new Ash() { X = 19 , Y = 24 }; //ash
            _ashIndex = new Helper.Point(19, 24);
            ashAndBdgsPos[0] = _ashIndex;
        }

        #endregion

        #region GetTile

        /* Gets tile.
         * Parameters: x, y - the coordinates of the tile
         * Return value: the tile corresponding to (x,y)
         */

        public Tile GetTile(Helper.Point point)
        {
            return GetTile(point.x, point.y);
        }

        public Tile GetTile(int x, int y)
        {
            return _map[x][y];
        }
        #endregion

        #endregion


        #region /* CONSTRUCTOR */

        /* Constructor of Map class
         * Parameters: mapFile, pokemonFile - names of the text files corresponding to the
         *                                    map configuration and the pokemon positions
         */
        public Map(string mapFile, string pokemonFile)
        {
            ReadMap(mapFile);
            PositionBadges();
            PositionAsh();
            ReadPokemons(pokemonFile);

        }

        #endregion


              
        private int[] i2XY(int ix)
        {
            return new int[2] { ix / 42, ix % 42 };
        }

        private int XY2i(int x, int y)
        {
            return y + (x * 42);
        }

    }
}

