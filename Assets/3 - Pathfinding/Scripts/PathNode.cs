using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar_3 {

    public enum NodeType
    {
        Grass,
        Wall,
        Valid,
        Fog
    }
    public class PathNode
    {
        private Grid<PathNode> grid;
        public int x;
        public int y;
        
        public int gCost; //cost of the path from the start node to n,
        public int hCost; //estimation du meilleur chemin de N vers la fin
        public int fCost; // somme des dux

        public NodeType nodeType;

        public PathNode cameFromNode;

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            this.nodeType = NodeType.Grass;
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

        internal void setType(NodeType nodeType)
        {
            this.nodeType = nodeType;
            grid.TriggerGridObjectChanged(x,y);
        }

        class PathNodeComparator : IComparer<PathNode>
        {
            public int Compare(PathNode x, PathNode y)
            {
                // CompareTo() method 
                return x.fCost.CompareTo(y.fCost);

            }
        }
    }


}