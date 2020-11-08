using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar_3 { 
    public class Pathfinding
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private Grid<PathNode> grid;
        private List<PathNode> openList; // La liste des noeuds qu'on va chercher (à trier par heuristique mb)
        private List<PathNode> closedList; // La liste des noeuds déjà cherché

        public Pathfinding(int width, int height)
        {
            grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (
                Grid<PathNode> g, int x, int y) // signature du constructeur
                => new PathNode(g, x, y) // Constructeur
                );
        }

        private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = grid.GetGridObject(startX, startY);
            openList = new List<PathNode> { startNode }; // Notation pour rajouter un noeud, stylé
            closedList = new List<PathNode>();

            for (int i = 0; i < grid.GetWidth(); i++)
            {
                for (int j = 0; j < grid.GetHeight(); j++)
                {
                    PathNode pathNode = grid.GetGridObject(i, j);
                    pathNode.gCost = int.MaxValue; // infini
                    int fCost = pathNode.CalculateFCost();
                    pathNode.cameFromNode = null;

                }
            }

            return new List<PathNode>();
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = xDistance + yDistance;
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }
    }

    

}