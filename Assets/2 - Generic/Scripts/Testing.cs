
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace Generic_2 {
    public class Testing : MonoBehaviour {

        [SerializeField] private HeatMapVisual heatMapVisual;
        [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
        private Grid<HeatMapGridObject> grid;

        private void Start() {
            grid = new Grid<HeatMapGridObject>(10, 10, 10f, Vector3.zero, 
                () => new HeatMapGridObject()); // pourquoi pas juste new HeatMapGridObject ?
            //  heatMapVisual.SetGrid(grid);
            // heatMapBoolVisual.SetGrid(grid);
        }

        private void Update() {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = UtilsClass.GetMouseWorldPosition();
               // grid.SetValue(position, true);
            }
        }
    }
}