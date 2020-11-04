
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace old
{
    public class Testing : MonoBehaviour
    {

        [SerializeField] private HeatMapVisual heatMapVisual;
        private Grid grid;

        private void Start()
        {
            grid = new Grid(35, 20, 5f, Vector3.zero);
            heatMapVisual.SetGrid(grid);
        }

        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = UtilsClass.GetMouseWorldPosition();
                int value = grid.GetValue(position);
                grid.AddValue(position, 100, 2, 10);
            }
        }
    }
}