
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
                MeshUtils.AddToMeshArrays(vertices, uv, triangles,index, grid.GetWorldPosition(i, j) * .5f, 0f, quadSize, Vector2.zero, Vector2.zero);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
