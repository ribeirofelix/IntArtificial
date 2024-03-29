﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model.Properties;

namespace Model
{
    public class Map
    {
        private static Map instance;

        public static Map Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Map();
                }
                return instance;
            }
        }

        #region /* PRIVATE PROPERTIES */

        private const int MAXTILES = 42;
        
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
                var oldIndex = _ashIndex; 
                _ashIndex = value;               
                this.Ash.Pos = new Helper.Point(_ashIndex.x, _ashIndex.y);
                GetTile(oldIndex).listUpdView();
            }
        }

        private Ash _ash;
        public Ash Ash
        {
            get { return _ash; }
            set
            {
                _ash = value;
                if (_ash != null)
                    _ash.Pos = new Helper.Point( _ashIndex.x , _ashIndex.y) ;
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
                    _map[j][k] = new Tile(line[k] , j, k , this);
                    Helper.PutGround(j, k, line[k]);

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
            StreamWriter sw = null;
            /* Try to open a text file */
            try
            {
                st = new System.IO.StreamReader(pokemonFile);
                sw = new StreamWriter(pokemonFile + ".out");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }


            var pokemons = Enum.GetValues(typeof(Pokemons)).Cast<Pokemons>().ToList();
            var rndType = new Random();

            for (int i = 0; i < 150; i++)
            {
                /* Read tuple <x, y, pokemon> */

                var line = st.ReadLine().Split(' ');
                int pokeN = int.Parse( line[2] );
                int x = int.Parse(line[0]);
                int y = int.Parse(line[1]);

                Pokemons t ;
                if (pokeN == -1) // aleatory Pokemon
                {
                    int nPoke = rndType.Next(pokemons.Count);
                    t = pokemons[nPoke];
                    pokemons.RemoveAt(nPoke);
                }
                else
                    t = (Pokemons) (pokeN -1 );
                

                /* If random position */
                if (x == -1 && y == -1)
                {
                    /* Random object */
                    Random random = new Random(DateTime.Now.Millisecond.GetHashCode());
                    var temLugar = this._map.Any(a => a.Where(b => !b.HasPokemon && !b.HasAsh && !b.hasMart && !b.hasPokeCenter && !b.hasTrainer ).Count() > 0);
                    if(temLugar)
                    do
                    {
                        x = random.Next(0, 42);
                        y = random.Next(0, 42);

                    } while (
                       GetTile(x, y).HasPokemon ||
                       GetTile(x, y).hasTrainer||
                    GetTile(x, y).hasPokeCenter ||
                    GetTile(x, y).hasMart ||
                       GetTile(x, y).HasAsh );
                    
                    
                }

                /* Create pokemon */

                Pokemon _pokemon = new Pokemon(t);

                /* Assign pokemon */
                GetTile(x, y).Pokemon = _pokemon;
                sw.WriteLine(x + " " + y + " " + _pokemon.ToString() );
            }
            sw.Close();
        }

        #endregion

        private void ReadTileElem(string path , PokeElem elem )
        {
            StreamReader sr;
            StreamWriter sw;
            try
            {
                sr = new StreamReader(path);
                sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(path), Enum.GetName( typeof(PokeElem) ,elem) + ".gen"));
            }
            catch (IOException ex)
            {
                throw ex;
            }

            var allFileByLine = sr.ReadToEnd().Split('\n');
            int posX = 0;
            int posY = 1;
            foreach (var line in allFileByLine )
            {
                var lineArgs = line.Split(' ');
                if (lineArgs.Length == 2)
                {
                    int x = int.Parse(lineArgs[posX]);
                    int y = int.Parse(lineArgs[posY].Replace('\r',' ').Trim() );
                    if (x == -1 || y == -1) // aleatory mode!
                    {
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        Tile t = null ;
                        do
                        {
                            x = rnd.Next(42); y = rnd.Next(42);
                            t = GetTile(x,y);
                        }while(t.Elem != PokeElem.None && !t.HasAsh );
                        t.Elem = elem;
                        sw.WriteLine(x + " " + y);
                    }
                    else
                        GetTile(x, y).Elem = elem;
                }                   
            }
            sw.Close();

        }

        #region PositionAsh

        /* Position Ash in a map. */

        private void PositionAsh()
        {
           this.Ash = new Ash() {}; //ash
           this.Ash.Pos = new Helper.Point(19, 24);
           _ashIndex = new Helper.Point(19, 24);
          
            
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
            if (x >= 42 || y >= 42 || x < 0 || y < 0)
                return null;
            return _map[x][y];
        }
        #endregion

        #endregion


        #region /* CONSTRUCTOR */

        /* Constructor of Map class
         * Parameters: mapFile, pokemonFile - names of the text files corresponding to the
         *                                    map configuration and the pokemon positions
         */
        private Map()
        {
            Helper.InitializeProlog(Resources.Prolog);
            ReadMap(Resources.Mapa01);
            PositionAsh();
            ReadTileElem(Resources.PositionMarts, PokeElem.Mart);
            ReadTileElem(Resources.PositionPokeCenters, PokeElem.PokeCenter);
            ReadTileElem(Resources.PositionTrainers, PokeElem.Trainer);
           
            ReadPokemons(Resources.PosicaoPokemons);

        }

        #endregion


       
    }
}

