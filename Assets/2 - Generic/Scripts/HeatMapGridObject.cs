using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Generic_2
{
    public class HeatMapGridObject
    {
        public int value;
       
        public HeatMapGridObject()
        {
            this.value = 1;
        }
        
        public void addValue(int value)
        {
            value = +value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}