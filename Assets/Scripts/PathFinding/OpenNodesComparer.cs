using System.Collections.Generic;

namespace TestProject.PathFinding
{
    public class OpenNodesComparer : IComparer<PathNode>
    {
        public int Compare(PathNode x, PathNode y)
        {
            if (x == null)
                return -1;

            if (y == null)
                return 1;

            return x.EstimateFullDistance().CompareTo(y.EstimateFullDistance());
        }
    }
}