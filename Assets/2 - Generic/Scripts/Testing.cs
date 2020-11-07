
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

namespace Generic_2 {
    public class Testing : MonoBehaviour {

        [SerializeField] private HeatMapVisual heatMapVisual;
        [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
        [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;

        private Grid<HeatMapGridObject> grid;

        private void Start() {
            grid = new Grid<HeatMapGridObject>(10, 10, 10f, Vector3.zero, 
                (Grid<HeatMapGridObject> g, int x, int y) //A cette signature ...
                => new HeatMapGridObject(g,x,y)); // ... On associe une méthode, qui est onstructeur de grid object
            //  heatMapVisual.SetGrid(grid);
            //heatMapBoolVisual.SetGrid(grid);
            heatMapGenericVisual.SetGrid(grid);

            HeatMapGridObject heatMapGridObject;
            for (int i = 0; i < 5; i++){
                heatMapGridObject = grid.GetGridObject(i,i) ;
                heatMapGridObject.is_bomb = true;
            }
            heatMapGridObject = grid.GetGridObject(0, 2);
            heatMapGridObject.is_bomb = true;
            heatMapGridObject = grid.GetGridObject(9, 9);
            heatMapGridObject.is_bomb = true;
            BombInit();

        }

        

        private void BombInit()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                   if (grid.GetGridObject(x,y).is_bomb) {
                        MajVoisins(x,y);
                   }
                }
            }
        }

        private void MajVoisins(int x, int y)
        {
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (grid.clickInRange(i, j))
                        grid.GetGridObject(i, j).addValue(1);
                }
            }
        }

        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                int x = -1;
                int y = -1;
                Vector3 position = UtilsClass.GetMouseWorldPosition();
                HeatMapGridObject heatMapGridObject = grid.GetGridObject(position, ref x, ref y);

                if (heatMapGridObject != null)
                {
                    
                    AlgoRecursif(x, y);
                    heatMapGridObject.Revele();
                }
            }
        }

        private void AlgoRecursif (int x, int y)
        {
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(x, y);

            
            if (grid.clickInRange(x, y) && heatMapGridObject.value == 0 && !heatMapGridObject.is_discovered)
            {
                heatMapGridObject.Revele();
                // Check left
                heatMapGridObject = grid.GetGridObject(x - 1, y);
                if (heatMapGridObject != null && heatMapGridObject.value == 0)
                    AlgoRecursif(x - 1, y);

                //Check Right
                heatMapGridObject = grid.GetGridObject(x + 1, y);
                if (heatMapGridObject != null && heatMapGridObject.value == 0)
                    AlgoRecursif(x + 1, y);

                //Check Top
                heatMapGridObject = grid.GetGridObject(x, y - 1);
                if (heatMapGridObject != null && heatMapGridObject.value == 0)
                    AlgoRecursif(x , y - 1);

                //Check bot
                heatMapGridObject = grid.GetGridObject(x, y + 1);
                if (heatMapGridObject != null && heatMapGridObject.value == 0)
                    AlgoRecursif(x , y + 1);
            }
        }
    }
    
        
}