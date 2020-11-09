
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace AStar_3
{
    public class Testing : MonoBehaviour
    {
        private Pathfinding pathfinding;
        private void Start()
        {
            Pathfinding pathfinding = new Pathfinding(10, 10);
        }

        private void Update()
        {
           if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                PathNode pa = pathfinding.grid.GetGridObject(mouseWorldPosition, out int x, out int y);
            }
        }
    }
        
}