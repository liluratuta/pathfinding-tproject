using System.Collections.Generic;
using UnityEngine;

namespace TestProject.PathFinding
{
    public interface IPathFinder
    {
        Vector2Int[] Find(Vector2Int start, Vector2Int goal);
    }
}