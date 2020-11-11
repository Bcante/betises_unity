
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AStar_3
{
    public class NodeComparator : IComparer<PathNode>
    {

        public int Compare(PathNode x, PathNode y)
        {
            return x.fCost.CompareTo(y.fCost);
        }

    }
}