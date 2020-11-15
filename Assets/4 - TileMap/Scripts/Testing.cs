
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace Tilemap_4
{
    public class Testing : MonoBehaviour
    {
        Tilemap tilemap;
        TilemapObject.TilemapSprite tilemapSprite;

        [SerializeField]
         private TileMapVisual tileMapVisual;
        private void Start()
        {
            tilemap = new Tilemap(10, 20, 10f, Vector3.zero);
            tilemapSprite = TilemapObject.TilemapSprite.Ground;
            tilemap.SetTilemapVisual(tileMapVisual);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                tilemap.SetTilemapSprite(mouseWorldPosition, tilemapSprite);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                tilemapSprite = TilemapObject.TilemapSprite.None;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                tilemapSprite = TilemapObject.TilemapSprite.Ground;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                tilemapSprite = TilemapObject.TilemapSprite.Path;
            }
        }

    }
        
}