
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour {

    [SerializeField] private HeatMapVisual heatMapVisual;
    private Grid grid;

    private void Start() {
        grid = new Grid(5, 10, 2f, Vector3.zero);  
        heatMapVisual.SetGrid(grid);
    }   

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            int value = grid.GetValue(position);
            grid.SetValue(position, value + 5); 
        }
    }
}
