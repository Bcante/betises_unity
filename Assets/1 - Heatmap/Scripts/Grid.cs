/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

namespace old { 
public class Grid {

    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;
    public const int HEAT_MAP_CLICK_RANGE = 3; //Range comme dans advance wars
    public const int HEAT_MAP_INCREMENT = 5;

    // Event Handler
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;


    // Local class definition
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        bool showDebug = false;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => 
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
            };
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPosition, int value) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public void AddValue(int x, int y, int value) {
        SetValue(x, y, GetValue(x, y) + value);
    }

    public int GetValue(int x, int y) {
        if (clickInRange(x, y)) {
            return gridArray[x, y];
        } else {
            return 0; // Mani�re pas styl�e de g�rer les erreurs.
        }
    }

    public int GetValue(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public bool clickInRange(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    // Chaqun de ces add values va d�clencher l'�v�nement, donc th�oriquement pour un clic on va avoir i*j*4 appels � notre truc d'update alors qu'on veut juste un r�sult par frame
    public void AddValue(Vector3 worldPosition, int value, int fullValueRange, int totalRange ) {
        UnityEngine.Profiling.Profiler.BeginSample("My Sample");

        int lowerValueAmount = Mathf.RoundToInt((float) value / (totalRange - fullValueRange));

        GetXY(worldPosition, out int originX, out int originY);
        if (clickInRange(originX, originY)) {
            for (int i = 0; i < totalRange; i++)
                {
                for (int j = 0; j < totalRange - i; j++)
                    {
                        int radius = i + j;
                        int addValueAmount = value;
                        if (radius > fullValueRange)
                        {
                            addValueAmount -= lowerValueAmount * ( radius - fullValueRange);
                        }
                             
                        AddValue(originX + i, originY + j, addValueAmount); // triangle haut droite
                        if (i > 0)
                        {
                            AddValue(originX - i, originY + j, addValueAmount); // triangle haut gauche
                        }

                    if (j > 0)
                    {
                        AddValue(originX + i, originY - j, addValueAmount); // triangle bas droite
                        if (i > 0)
                        {
                            AddValue(originX - i, originY - j, addValueAmount); // triangle bas gauche
                        }
                    } 
                    
                }
             }
            UnityEngine.Profiling.Profiler.EndSample();
        }
        


        //int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

        //GetXY(worldPosition, out int originX, out int originY);
        //for (int x = 0; x < totalRange; x++) {
        //    for (int y = 0; y < totalRange - x; y++) {
        //        int radius = x + y;
        //        int addValueAmount = value;
        //        if (radius >= fullValueRange) {
        //            addValueAmount -= lowerValueAmount * (radius - fullValueRange);
        //        }

        //        AddValue(originX + x, originY + y, addValueAmount);

        //        if (x != 0) {
        //            AddValue(originX - x, originY + y, addValueAmount);
        //        }
        //        if (y != 0) {
        //            AddValue(originX + x, originY - y, addValueAmount);
        //            if (x != 0) {
        //                AddValue(originX - x, originY - y, addValueAmount);
        //            }
        //        }
        //    }
        //}
    }

}
}