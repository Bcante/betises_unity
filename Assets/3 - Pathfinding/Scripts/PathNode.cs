using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar_3 {

    public enum tileType
    {
        Plaine,
        Mur
    }
    public class PathNode
    {
        private Grid<PathNode> grid;
        public int x;
        public int y;
        
        public int gCost; //cost of the path from the start node to n,
        public int hCost; //estimation du meilleur chemin de N vers la fin
        public int fCost; // somme des dux

        public tileType type;

        public enum ArrivalStatus { Unknown = -3, Late = -1, OnTime = 0, Early = 1 };

        public PathNode cameFromNode;

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            this.type = tileType.Plaine;
        }

        public override string ToString()
        {
            return x + " - " + y;          
        }

        public int CalculateFCost()
        {
            fCost = gCost + hCost;
            return fCost;
        }
    }


}