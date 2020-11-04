
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual : MonoBehaviour
{
    private Grid grid;
    private Mesh mesh;

    public void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();

        grid.OnGridValueChanged += Grid_OnGridValueChanged; // Subscribe à l'evenement 
    }

    private void Grid_OnGridValueChanged(object sender, Grid.OnGridValueChangedEventArgs e)
    {
        UpdateHeatMapVisual();
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles); // A run à chaque fois ?

        for (int i = 0; i < grid.GetWidth(); i++ )
        { 
            for (int j = 0; j < grid.GetHeight(); j++)
            {
                int index = i * grid.GetHeight() + j;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                int gridValue = grid.GetValue(i, j);
                float normalizedValue = (float)gridValue / Grid.HEAT_MAP_MAX_VALUE; //ça permet d'avoir un %
                Vector2 gridValueUV = new Vector2(normalizedValue, 0f);

                MeshUtils.AddToMeshArrays(vertices, uv, triangles,index, grid.GetWorldPosition(i, j) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;


    }


}
