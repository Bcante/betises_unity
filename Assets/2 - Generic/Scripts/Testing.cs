
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace Generic_2 {
    public class Testing : MonoBehaviour {

        [SerializeField] private HeatMapVisual heatMapVisual;
        [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
        private Grid<bool> grid;

        private void Start() {
            grid = new Grid<bool>(10, 10, 10f, Vector3.zero);
            //  heatMapVisual.SetGrid(grid);
            heatMapBoolVisual.SetGrid(grid);
        }

        private void Update() {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = UtilsClass.GetMouseWorldPosition();
                grid.SetValue(position, true);
            }
        }
    }
}