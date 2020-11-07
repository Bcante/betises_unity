
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
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


        }

        private void Update() {

            if (Input.GetMouseButtonDown(0))
            {
                int x = -1;
                int y = -1;
                Vector3 position = UtilsClass.GetMouseWorldPosition();
                HeatMapGridObject heatMapGridObject = grid.GetGridObject(position,ref x,ref y);
                
                if (heatMapGridObject != null)
                {
                    heatMapGridObject.Revele();
                    AlgoRecursif(x,y);
                }
            }
        }

        private void AlgoRecursif (int x, int y)
        {
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(x, y);

            if (grid.clickInRange(x, y) && !heatMapGridObject.is_bomb)
            {
                heatMapGridObject.Revele();
                // Check left
                heatMapGridObject = grid.GetGridObject(x - 1, y);
                if (heatMapGridObject != null && heatMapGridObject.is_discovered == false)
                    AlgoRecursif(x - 1, y);

                //Check Right
                heatMapGridObject = grid.GetGridObject(x + 1, y);
                if (heatMapGridObject != null && heatMapGridObject.is_discovered == false)
                    AlgoRecursif(x + 1, y);

                //Check Top
                heatMapGridObject = grid.GetGridObject(x, y - 1);
                if (heatMapGridObject != null && heatMapGridObject.is_discovered == false)
                    AlgoRecursif(x , y - 1);

                //Check bot
                heatMapGridObject = grid.GetGridObject(x, y + 1);
                if (heatMapGridObject != null && heatMapGridObject.is_discovered == false)
                    AlgoRecursif(x , y + 1);
            }
        }
    }
    
        
}