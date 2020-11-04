
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace old
{
    public class HeatMapVisual : MonoBehaviour
    {
        private Grid grid;
        private Mesh mesh;
        private bool updateMesh;

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

        // Pour des raisons de performance on va mettre en buffer les update avant de les déclencher
        private void Grid_OnGridValueChanged(object sender, Grid.OnGridValueChangedEventArgs e)
        {
            //UpdateHeatMapVisual();
            updateMesh = true;
        }

        // fonction système donc pas besoin de l'appeler de manière externe. Elle le sera à la fin de chaque frame, et pas x fois par frame
        private void LateUpdate()
        {
            if (updateMesh)
            {
                updateMesh = false;
                UpdateHeatMapVisual();
            }
        }

        private void UpdateHeatMapVisual()
        {
            MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles); // A run à chaque fois ?

            for (int i = 0; i < grid.GetWidth(); i++)
            {
                for (int j = 0; j < grid.GetHeight(); j++)
                {
                    int index = i * grid.GetHeight() + j;
                    Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                    int gridValue = grid.GetValue(i, j);
                    float normalizedValue = (float)gridValue / Grid.HEAT_MAP_MAX_VALUE; //ça permet d'avoir un %
                    Vector2 gridValueUV = new Vector2(normalizedValue, 0f);

                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(i, j) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;


        }


    }
}