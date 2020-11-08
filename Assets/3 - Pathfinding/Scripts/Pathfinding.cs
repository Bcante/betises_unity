using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar_3 { 
    public class Pathfinding
    {
        private Grid<PathNode> grid;
        public Pathfinding(int width, int height)
        {
            grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (
                Grid<PathNode> g, int x, int y) // signature du constructeur
                => new PathNode(g, x, y) // Constructeur
                );
        }
    }

}