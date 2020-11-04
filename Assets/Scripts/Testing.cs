
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour {

    [SerializeField] private HeatMapVisual heatMapVisual;
    private Grid grid;

    private void Start() {
        grid = new Grid(10, 10, 10f, Vector3.zero);  
        heatMapVisual.SetGrid(grid);
    }   

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            int value = grid.GetValue(position);
            grid.AddValue(position, Grid.HEAT_MAP_INCREMENT , Grid.HEAT_MAP_CLICK_RANGE); 
        }
    }
}
