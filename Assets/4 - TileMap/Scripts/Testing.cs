
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace Tilemap_4
{
    public class Testing : MonoBehaviour
    {
        //private Pathfinding pathfinding;
        //[SerializeField]
        //private HeatMapGenericVisual heatMapGenericVisual;
        //private void Start()
        //{

        //    pathfinding = new Pathfinding(10, 10);
        //    Grid<PathNode> g = pathfinding.GetGrid();
        //    int width = g.GetWidth();
        //    for (int x = 0; x < width - 1; x ++) { 
        //        g.GetGridObject(x, 1).nodeType = NodeType.Wall;
        //        g.GetGridObject(width - 1 - x, 3).nodeType = NodeType.Wall;
        //        g.GetGridObject(x, 5).nodeType = NodeType.Wall;
        //    }
        //    heatMapGenericVisual.SetGrid(g);
        //}

        private void Update()
        {
           //if (Input.GetMouseButtonDown(0))
           // {
           //     Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
           //         pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);


           //     if (pathfinding.GetGrid().ClickInRange(x,y))
           //     {
           //          List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
           //         if (path != null)
           //         {
           //             for (int i = 0; i < (path.Count-1); i++)
           //             {
           //                 Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, 
           //                     new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 100);
           //             }
           //         }
           //     }
                

           // }
        }

    }
        
}