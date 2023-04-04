using System.Collections.Generic;
using System.Linq;
using TestProject.GameField;
using UnityEngine;

namespace TestProject.PathFinding
{
    public class PathFinder : IPathFinder
    {
        private readonly IField _field;
        private readonly IComparer<PathNode> _openNodesComparer;
        private readonly Vector2Int[] _directions = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };

        public PathFinder(IField field)
        {
            _field = field;
            _openNodesComparer = new OpenNodesComparer();
        }

        public Vector2Int[] Find(Vector2Int start, Vector2Int goal)
        {
            var openNodes = new List<PathNode>();
            var closedNodes = new HashSet<PathNode>();

            openNodes.Add(CreateStartNode(start, goal));

            while (openNodes.Count > 0)
            {
                var currentNode = GetMostProfitableNode(openNodes);

                if (IsGoal(currentNode, goal))
                    return BuildPathFor(currentNode);

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                foreach (var neighbour in GetNeighbours(currentNode, goal))
                {
                    if (closedNodes.Contains(neighbour))
                        continue;

                    UpdateOpenNodes(openNodes, neighbour);
                }
            }

            return new Vector2Int[] {};
        }

        private IEnumerable<PathNode> GetNeighbours(PathNode currentNode, Vector2Int goal)
        {
            var result = new List<PathNode>();
            var currentPoint = currentNode.Point;

            foreach (var direction in _directions)
            {
                var point = currentPoint + direction;
                
                if (!_field.IsExist(point))
                    continue;
                
                if (!_field.IsPassableMove(currentPoint, point))
                    continue;
                
                result.Add(CreateNeighbourNode(point));
            }

            return result;

            PathNode CreateNeighbourNode(Vector2Int p)
            {
                return new PathNode(p)
                {
                    PreviousNode = currentNode,
                    DistanceFromStart = currentNode.DistanceFromStart + _field.DistanceBetween(currentPoint, p),
                    HeuristicDistance = GetHeuristicDistance(p, goal)
                };
            }
        }

        private static void UpdateOpenNodes(List<PathNode> openNodes, PathNode neighbour)
        {
            var nodeInOpenSet = openNodes.FirstOrDefault(x => x.Equals(neighbour));

            if (nodeInOpenSet == null)
            {
                openNodes.Add(neighbour);
                return;
            }

            if (nodeInOpenSet.DistanceFromStart <= neighbour.DistanceFromStart) 
                return;
            
            openNodes.Remove(nodeInOpenSet);
            openNodes.Add(neighbour);
        }

        private static Vector2Int[] BuildPathFor(PathNode node)
        {
            var result = new List<Vector2Int>();
            var current = node;

            while (current.PreviousNode != null)
            {
                result.Add(current.Point);
                current = current.PreviousNode;
            }

            result.Add(current.Point);
            result.Reverse();
            return result.ToArray();
        }

        private static bool IsGoal(PathNode node, Vector2Int goal) => 
            node.Point == goal;

        private PathNode GetMostProfitableNode(List<PathNode> openNodes)
        {
            openNodes.Sort(_openNodesComparer);
            return openNodes.First();
        }

        private static PathNode CreateStartNode(Vector2Int start, Vector2Int goal)
        {
            return new PathNode(start)
            {
                DistanceFromStart = 0,
                HeuristicDistance = GetHeuristicDistance(start, goal),
                PreviousNode = null
            };
        }

        private static float GetHeuristicDistance(Vector2Int start, Vector2Int end) => 
            Vector2Int.Distance(start, end);
    }
}