
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Generic_2;

namespace AStar_3
{
    public class HeatMapGenericVisual : MonoBehaviour
    {
        private Grid<PathNode> grid;
        private Mesh mesh;
        private bool updateMesh;

        private Dictionary<NodeType,float> colorMap;

        public void Awake()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            colorMap = new Dictionary<NodeType, float>();
            colorMap.Add(NodeType.Wall, 0f);
            colorMap.Add(NodeType.Grass, 0.5f);
            colorMap.Add(NodeType.Valid, 1f);
        }

        public void SetGrid(Grid<PathNode> grid)
        {
            this.grid = grid;
            UpdateHeatMapVisual();

            grid.OnGridValueChanged += Grid_OnGridValueChanged; // Subscribe à l'evenement 

        }

        // Pour des raisons de performance on va mettre en buffer les update avant de les déclencher
        private void Grid_OnGridValueChanged(object sender, Grid<PathNode>.OnGridValueChangedEventArgs e)
        {
            UpdateHeatMapVisual();
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

                    PathNode node = grid.GetGridObject(i, j);
                    NodeType nodeType = node.nodeType;
                   
                    Vector2 gridValueUV = new Vector2(colorMap[nodeType], 0f);


                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(i, j) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;


        }

    }
}