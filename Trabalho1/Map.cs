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
        public int[] ashAndBdgsPos = new int[9];
        public Pokedex pokedex;
        private Tile[][] _map = new Tile[MAXTILES][];

        public ICollection<ICollection<Tile>> KantoMap

        {
            get
            {
                return _map;
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
                /* Creates columns */
                _map[j] = new Tile[42];

                /* Read line */
                string line = st.ReadLine();

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

            /* Random object */
            Random random = new Random();

            for (int i = 0; i < 48; i++)
            {
                /* Read tuple <type, x, y> */
                char t = char.Parse(st.ReadLine());
                int x = int.Parse(st.ReadLine());
                int y = int.Parse(st.ReadLine());

                /* If random position */
                if (x == -1 && y == -1)
                {
                    do
                    {
                        x = random.Next(0, 42);
                        y = random.Next(0, 42);

                    } while (GetTile(x, y).TileType != TileTypes.Grass ||
                       GetTile(x, y).TilePokemon != null ||
                       GetTile(x, y).TileBadge != null ||
                       GetTile(x, y).TileAsh != null);
                }

                /* Create pokemon */

                Pokemon _pokemon = new Pokemon(t);

                /* Assign pokemon */
                GetTile(x, y).TilePokemon = _pokemon;
            }
        }

        #endregion

        #region PositionBadges

        /* Position badges in a map. */

        private void PositionBadges()
        {
            GetTile(2, 4).TileBadge = new Badge(); //soul - Koga - veneno
            ashAndBdgsPos[(int)BadgeTypes.soul] = XY2i(2, 4);

            GetTile(4, 36).TileBadge = new Badge(); //volcano - Blaine - fogo
            ashAndBdgsPos[(int)BadgeTypes.volcano] = XY2i(4, 36);
            
            GetTile(2, 19).TileBadge = new Badge(); //thunder - Ten Surge - eletrico
            ashAndBdgsPos[(int)BadgeTypes.thunder] = XY2i(2, 19);
            
            GetTile(40, 32).TileBadge = new Badge(); //boulder - Brock - pedra
            ashAndBdgsPos[(int)BadgeTypes.boulder] = XY2i(40, 32);
            
            GetTile(22, 2).TileBadge = new Badge(); //rainbow - Erika - planta
            ashAndBdgsPos[(int)BadgeTypes.rainbow] = XY2i(22, 2);
            
            GetTile(20, 39).TileBadge = new Badge(); //earth - Giovanni - terra
            ashAndBdgsPos[(int)BadgeTypes.earth] = XY2i(20, 39);
            
            GetTile(19, 14).TileBadge = new Badge(); //cascade - Misty - agua
            ashAndBdgsPos[(int)BadgeTypes.cascade] = XY2i(19, 14);
            
            GetTile(37, 19).TileBadge = new Badge(); //marsh - Sabrina - psiquico
            ashAndBdgsPos[(int)BadgeTypes.marsh] = XY2i(37, 19);


        }

        #endregion

        #region PositionAsh

        /* Position Ash in a map. */

        private void PositionAsh()
        {
            GetTile(19, 24).TileAsh = new Ash() { X = 19 , Y = 24 }; //ash
            _ashIndex = XY2i(24, 19);
            ashAndBdgsPos[0] = _ashIndex;
        }

        #endregion

        #region GetTile

        /* Gets tile.
         * Parameters: x, y - the coordinates of the tile
         * Return value: the tile corresponding to (x,y)
         */

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
            for (int i = 0; i < MAXTILES; i++)
            {
                _map[i] = new Tile[MAXTILES];
            }
            ReadMap(mapFile);
            PositionBadges();
            PositionAsh();
            ReadPokemons(pokemonFile);

            
            /*teste*/
            var res = String.Join("\n", _map.Select(m => String.Join("", m.Select(t => t.TileType.ToString()[0]) )) );
            Console.WriteLine(res);          

        }

        #endregion


        private int _ashIndex;
        public int AshIndex 
        {
            get
            {
                return _ashIndex;
            }
            set
            {
                _ashIndex = value;
            }
        }


        
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

