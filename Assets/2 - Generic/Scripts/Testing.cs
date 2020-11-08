
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
            //  heatMapGenericVisual.SetGrid(grid);
        }

        private void Update() {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = UtilsClass.GetMouseWorldPosition();
                HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);

                if (heatMapGridObject != null)
                {
                    heatMapGridObject.addValue(5);
                }

            }
        }
        public HeatMapGridObject UneAutreFonction(Grid<HeatMapGridObject> g, int x, int y)
        {
            x = 1;
            return new HeatMapGridObject(g, x, y);
        }
    }
    
        
}