using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AStar_3 { 
    public class Pathfinding
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private Grid<PathNode> grid;
        private List<PathNode> openList; // La liste des noeuds qu'on va chercher (à trier par heuristique mb)
        private List<PathNode> closedList; // La liste des noeuds déjà cherché
        
        public static Pathfinding Instance { get; private set; }


        public Pathfinding(int width, int height)
        {
            Instance = this; 
            grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (
                Grid<PathNode> g, int x, int y) // signature du constructeur
                => new PathNode(g, x, y) // Constructeur
                );
        }

        public Grid<PathNode> GetGrid()
        {
            return this.grid;
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {

            /* INITIALISIATION */
            PathNode startNode = grid.GetGridObject(startX, startY);
            PathNode endNode = grid.GetGridObject(endX, endY);

            openList = new List<PathNode> { startNode }; // Notation pour rajouter un noeud, stylé
            closedList = new List<PathNode>();

            for (int i = 0; i < grid.GetWidth(); i++)
            {
                for (int j = 0; j < grid.GetHeight(); j++)
                {
                    PathNode pathNode = grid.GetGridObject(i, j);
                    pathNode.gCost = 10000  ; // infini
                    int fCost = pathNode.CalculateFCost();
                    pathNode.cameFromNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost =  CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();
            bool endNotFound = true;

            /* MAIN LOOP */
            while (openList.Count>0 && endNotFound)
            {
                PathNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode) // énorme pas besoin de .equals()
                {
                    endNotFound = false; // inutile 
                    return CalculatePath(endNode); // Le chemin début fin est retourné ici
                }
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                List<PathNode> nodes = GetNeighbourList(currentNode);
                foreach (PathNode neighborNode in nodes)
                {
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);
                    if (tentativeGCost < neighborNode.gCost)
                    {
                        neighborNode.cameFromNode = currentNode;
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.hCost = CalculateDistanceCost(neighborNode,endNode);
                        neighborNode.CalculateFCost();

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }

            }

            return null;
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighborList = new List<PathNode>();
            for (int i = currentNode.x - 1; i < currentNode.x + 2; i++)
            {
                for (int j = currentNode.y - 1; j < currentNode.y + 2 ; j++)
                {
                    bool inRange = grid.ClickInRange(i, j);
                    PathNode tmpNode = grid.GetGridObject(i, j);
                    
                    if (inRange && !(i == currentNode.x && j == currentNode.y)) // Clic pas hors tableau et on veut pas s'ajouter nous même en voisins
                    {
                        bool walkableNode = IsWalkableNode(tmpNode);
                        if (walkableNode) 
                        {
                            neighborList.Add(tmpNode);
                        }
                    }
                }
            }
            return neighborList;
        }

        private bool IsWalkableNode(PathNode currentNode)
        {
            NodeType res = currentNode.nodeType;
            if (res == NodeType.Wall)
            {
                return false;
            }
            return true;
        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            endNode.setType(NodeType.Valid);
            List<PathNode> chemin = new List<PathNode>();
            chemin.Add(endNode);
            PathNode parent = endNode.cameFromNode;

            
            while (parent != null)
            {
                chemin.Add(parent);
                parent.setType(NodeType.Valid);
                parent = parent.cameFromNode;
            }
            chemin.Reverse();
            foreach (PathNode pn in chemin)
            {
                Debug.Log(pn);
            }
            return chemin;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b) // hCost
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Math.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }


        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) // absolument dégueulasse en terme d'opti non?
        {
            PathNode pn = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].fCost < pn.fCost)
                {
                    pn = pathNodeList[i];
                }
            }
            return pn;
        }
    }

    

}