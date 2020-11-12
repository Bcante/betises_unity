
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AStar_3
{
    public class NodeComparator : IComparer<PathNode>
    {

        public int Compare(PathNode x, PathNode y)
        {
            if (x.x == y.x && y.x == y.y)
            {
                return 0;
            }
            return x.fCost.CompareTo(y.fCost) == 0 ? 1 : x.fCost.CompareTo(y.fCost);
        }

    }
}