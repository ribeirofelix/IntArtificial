using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Trabalho1
{
    class Map
    {
        #region /* PRIVATE PROPERTIES */

        private Tile[][] _map = new Tile[42][];

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
                    _map[j][k] = new Tile(line[k]);
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

        void PositionBadges()
        {
            GetTile(4, 2).TileBadge = new Badge(); //coracao
            GetTile(36, 4).TileBadge = new Badge(); //fogo
            GetTile(19, 2).TileBadge = new Badge(); //sol
            GetTile(32, 40).TileBadge = new Badge(); //pedra
            GetTile(2, 22).TileBadge = new Badge(); //arco-iris
            GetTile(39, 20).TileBadge = new Badge(); //folhinha
            GetTile(14, 19).TileBadge = new Badge(); //gotinha
            GetTile(19, 37).TileBadge = new Badge(); //moeda
        }

        #endregion

        #region PositionAsh

        /* Position Ash in a map. */

        void PositionAsh()
        {
            GetTile(24, 19).TileAsh = new Ash(); //ash
        }

        #endregion

        #region GetTile

        /* Gets tile.
         * Parameters: x, y - the coordinates of the tile
         * Return value: the tile corresponding to (x,y)
         */

        Tile GetTile(int x, int y)
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
            for (int i = 0; i < 42; i++)
            {
                _map[i] = new Tile[42];
            }
            ReadMap(mapFile);
            PositionBadges();
            PositionAsh();
            ReadPokemons(pokemonFile);

            //teste!
            for (int count = 0; count < 42; count++)
            {
                for (int count2 = 0; count2 < 5; count2++)
                {
                    Tile temp = GetTile(count, count2);
                    Console.WriteLine("Posição " + count.ToString() + " " + count2.ToString() + " Tipo: " + temp.TileType + " " + temp.TilePokemon);
                }

            }
            //fim teste

        }

        #endregion
    }
}
