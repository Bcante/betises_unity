using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar_3 { 
    public class PathNode
    {
        private Grid<PathNode> grid;
        private int x;
        private int y;

        public int fCost;
        public int gCost;
        public int hCost;

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
    }


}