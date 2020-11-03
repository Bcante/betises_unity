
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual : MonoBehaviour
{
    private Grid grid;
    private Mesh mesh;

    public void Awake()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] trinagles); // A run à chaque fois ?

        for (int i = 0; i < grid.GetWidth(); i++ )
        {
            for (int j = 0; j < grid.GetHeight(); j++)
            {
                int index = i * grid.GetHeight() + j;
                Debug.Log(index);
            }
        }
    }
}
