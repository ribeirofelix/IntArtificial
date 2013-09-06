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
        private Tile[][] _map =  new Tile[42][];

        public void ReadMap(string mapFile)
        {
            StreamReader st = new System.IO.StreamReader(mapFile);
            
            for (int j = 0; j < 42; j++)
            {
                _map[j]= new Tile[42];

                string arquivo = st.ReadLine();                

                for (int k = 0; k < arquivo.Length; k++)
                {
                    _map[j][k] = new Tile(arquivo[k]);
                }
            }
            
            st.Close();
        }

        Tile getTile(int x, int y)
        {
            return _map[x][y];
        }

        public void ReadPokemons(string pokemonFile)
        {
            StreamReader st = new System.IO.StreamReader(pokemonFile);
            Random random = new Random();

            Console.WriteLine("entrei aqui!");

            for (int i = 0; i < 48; i++)
            {
                string temp = st.ReadLine();
                char t = char.Parse(temp);
                int x = int.Parse(st.ReadLine());
                int y = int.Parse(st.ReadLine());


                if (x == -1 && y == -1)
                {
                    do
                    {
                        x = random.Next(0, 42);
                        y = random.Next(0, 42);

                    } while (getTile(x, y).TileType != TileTypes.Grass ||
                       getTile(x, y).TilePokemon != null ||
                       getTile(x, y).TileBadge != null ||
                       getTile(x, y).TileAsh != null);

                }

                Pokemon _pokemon = new Pokemon(t);
                getTile(x, y).TilePokemon = _pokemon;
            }
        }

        void PositionBadges()
        {
            getTile(4, 2).TileBadge = new Badge(); //coracao
            getTile(36, 4).TileBadge = new Badge(); //fogo
            getTile(19, 2).TileBadge = new Badge(); //sol
            getTile(32, 40).TileBadge = new Badge(); //pedra
            getTile(2, 22).TileBadge = new Badge(); //arco-iris
            getTile(39, 20).TileBadge = new Badge(); //folhinha
            getTile(14, 19).TileBadge = new Badge(); //gotinha
            getTile(19, 37).TileBadge = new Badge(); //moeda
        }

        void PositionAsh()
        {
            getTile(24, 19).TileAsh = new Ash(); //ash
        }

        public Map(string mapFile, string pokemonFile)
        {
            for (int i = 0; i < 42; i++)
            {
                _map[i] = new Tile[42];
            }
            Console.WriteLine("print1");
            ReadMap(mapFile);
            Console.WriteLine("print2");
            PositionBadges();
            Console.WriteLine("print3");
            PositionAsh();
            Console.WriteLine("print4");
            ReadPokemons(pokemonFile);
            Console.WriteLine("print5");

            //teste!

            Console.WriteLine("antes for");

            for (int count = 0; count <= 41; count++)
            {
                for (int count2 = 0; count2 < 5; count2++)

                {
   
                        Tile temp = getTile(count, count2);
                        Console.WriteLine("Posição " + count.ToString() + " " + count2.ToString() + " Tipo: " + temp.TileType + " " + temp.TilePokemon);

                }

              }
 
        }

    }
}
