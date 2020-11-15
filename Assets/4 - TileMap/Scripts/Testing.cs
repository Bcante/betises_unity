
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
namespace Tilemap_4
{
    public class Testing : MonoBehaviour
    {
        Tilemap tilemap;
        private void Start()
        {

            tilemap = new Tilemap(10, 20, 10f, Vector3.zero);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                tilemap.SetTilemapSprite(mouseWorldPosition, TilemapObject.TilemapSprite.Ground);
            }
        }

    }
        
}