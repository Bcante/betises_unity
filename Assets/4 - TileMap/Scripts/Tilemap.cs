using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tilemap_4
{
    public class Tilemap : MonoBehaviour
    {
        private Grid<TilemapObject> grid;
        private TileMapVisual tileMapVisual;

        public Tilemap(int width, int height, float cellSize, Vector3 originPosition)
        {
            grid = new Grid<TilemapObject>(width, height, cellSize, originPosition,
                (Grid<TilemapObject> g, int x, int y) => new TilemapObject(g,x,y));
        }

        internal void SetTilemapVisual(TileMapVisual tileMapVisual) // pourquoi l'appeler comme ça alors ????
        {
            tileMapVisual.SetGrid(grid);
        }

        public Grid<TilemapObject> GetGrid()
        {
            return this.grid;
        }

        public void SetTilemapSprite(Vector3 worldPosition, TilemapObject.TilemapSprite tilemapSprite)
        {
            TilemapObject tilemapObject= grid.GetGridObject(worldPosition);
            if (tilemapObject != null)
                tilemapObject.SetTilemapSprite(tilemapSprite);
        }

    }

    public class TilemapObject
    {
        public enum TilemapSprite
        {
            None,
            Ground,
            Path,
        }

        private Grid<TilemapObject> grid;
        private int x, y;

        private TilemapSprite tilemapSprite;

        public TilemapObject(Grid<TilemapObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetTilemapSprite (TilemapSprite tilemap)
        {
            this.tilemapSprite = tilemap;
            grid.TriggerGridObjectChanged(x,y);
        }

        public TilemapSprite GetTilemapSprite()
        {
            return this.tilemapSprite;
        }

        public override string ToString()
        {
            return this.tilemapSprite.ToString();
        }
    }
}