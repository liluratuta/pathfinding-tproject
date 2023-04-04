using UnityEngine;

namespace TestProject.PathFinding
{
    public class PathNode
    {
        public Vector2Int Point { get; }
        
        public PathNode PreviousNode;
        public float DistanceFromStart;
        public float HeuristicDistance;

        public PathNode(Vector2Int point)
        {
            Point = point;
        }

        public float EstimateFullDistance() =>
            DistanceFromStart + HeuristicDistance;

        public override bool Equals(object other) => 
            other is PathNode node && Point.Equals(node.Point);

        public override int GetHashCode() => 
            Point.GetHashCode();
    }
}
