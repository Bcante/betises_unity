using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace Generic_2
{
    public class HeatMapGridObject
    {
        public int value;
        private const int MIN = 0;
        private const int MAX = 100;

        private int x, y;
        private Grid<HeatMapGridObject> grid; // parentt


        public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void addValue(int addValue)
        {
            this.value = addValue + this.value;
            value = Mathf.Clamp(value, MIN, MAX);
            grid.TriggerGridObjectChanged(x,y); // Permet de déclencher l'évènement, qui sera envoyé aux visuals
        }

        public float getValueNormalized()
        {
            return (float)this.value / MAX;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }


    }
}