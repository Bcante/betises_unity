﻿
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Generic_2;

namespace Tilemap_4
{
    public class TileMapVisual : MonoBehaviour
    {
        private Grid<TilemapObject> grid;
        private Mesh mesh;
        private bool updateMesh;


        public void Awake()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        public void SetGrid(Grid<TilemapObject> grid)
        {
            this.grid = grid;
            UpdateHeatMapVisual();

            grid.OnGridValueChanged += Grid_OnGridValueChanged; // Subscribe à l'evenement 

        }

        // Pour des raisons de performance on va mettre en buffer les update avant de les déclencher
        private void Grid_OnGridValueChanged(object sender, Grid<TilemapObject>.OnGridValueChangedEventArgs e)
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

                    TilemapObject tilemapObject = grid.GetGridObject(i, j);
                    TilemapObject.TilemapSprite tilemapSprite = tilemapObject.GetTilemapSprite();

                    Vector2 gridValueUV00 = Vector2.zero;
                    Vector2 gridValueUV11;
                    if (tilemapSprite == TilemapObject.TilemapSprite.None)
                    {
                        gridValueUV11 = Vector2.zero;
                        quadSize = Vector3.zero;
                    }
                    else
                    {
                        gridValueUV11 = Vector2.one;
                    }
                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(i, j) + quadSize * .5f, 0f, quadSize, gridValueUV00, gridValueUV11);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;


        }

    }
}