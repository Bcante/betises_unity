
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
        UpdateVisualSize();
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
                MeshUtils.AddToMeshArrays(vertices, uv, triangles,index, grid.GetWorldPosition(i, j) * 1f, 0f, quadSize, Vector2.zero, Vector2.zero);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Adapter le gameobject par rapport a l'offset lié au cellsize
        GetComponent<Transform>().position += new Vector3(grid.GetCellSize()*.5f, grid.GetCellSize()*.5f, 0); 

    }

    private void UpdateVisualSize()
    {
        //GetComponent<Transform>().localScale = new Vector3(2, 2, 0); // La honte ...

    }
}
