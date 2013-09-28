﻿using Controller.Properties;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Controller
{
    public class MapController
    {

        private const int mapLength = 42 * 42;

        public MapController()
        {
            _kantoMap = new Map(Resources.mapPath, Resources.pokePath);
            posAshBdg = _kantoMap.ashAndBdgsPos;

            BrotarAsh();
        }


        private Helper.Point[] posAshBdg;
        
        private int[][] distMap =  Enumerable.Repeat<int[]>(new int[9],9).ToArray() ;
        public int[][] DistMap
        {
            get{ return distMap; }
        }

        private Map _kantoMap;
        public Map KantoMap
        {
            get 
            {
                if (_kantoMap == null)
                {
                    _kantoMap = new Map(Resources.mapPath, Resources.pokePath);
                    posAshBdg = _kantoMap.ashAndBdgsPos; 
                }
                return _kantoMap; 
            }
        }

        private void ChangeRoute()
        {
            //call genetic
            //call astar
        }

        private List<IAshChanged> listeners = new List<IAshChanged>();

        public void MoveSprite(object source, ElapsedEventArgs e)
        {
            Random random = new Random();
            int randomx = random.Next(0, 42);
            int randomy = random.Next(0, 42);
            _kantoMap.GetTile(randomx, randomy).Ash = _kantoMap.GetTile(_kantoMap.AshIndex.x, _kantoMap.AshIndex.y).Ash;
            _kantoMap.GetTile(_kantoMap.AshIndex.x, _kantoMap.AshIndex.y).Ash = null;
            _kantoMap.AshIndex = new Helper.Point(randomx, randomy);

            foreach (IAshChanged listener in listeners)
            {
                listener.AshChanged(this);
            }
        }

        public void RegisterListener(IAshChanged InterestedObject)
        {
            listeners.Add(InterestedObject);
        }

        public void BrotarAsh()
        {
            var timerMoveSprite = new Timer(500);
            timerMoveSprite.AutoReset = true;
            timerMoveSprite.Elapsed += new ElapsedEventHandler(MoveSprite);
            timerMoveSprite.Start();
        }

        public void UpdateDistances()
        {
            var aStar = new AStar(this._kantoMap);
            int totalCost;
            for (int i = 0; i < this.posAshBdg.Length; i++)
            {
                distMap[i][i] = 0;
                for (int j = i+1; j < this.posAshBdg.Length; j++)
                {
                    aStar.Star(posAshBdg[i], posAshBdg[j], out totalCost);
                    distMap[i][j] = totalCost;
                    distMap[j][i] = totalCost;
                }
            }

        }

    }
}
