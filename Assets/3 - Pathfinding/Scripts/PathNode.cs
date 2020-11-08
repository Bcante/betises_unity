using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar_3 { 
    public class PathNode
    {
        private Grid<PathNode> grid;
        public int x;
        public int y;

        public int fCost;
        public int gCost; //cost of the path from the start node to n,
        public int hCost; //estimation du meilleur chemin de N vers la fin

        public PathNode cameFromNode;

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
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