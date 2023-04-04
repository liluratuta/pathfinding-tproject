using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject.GameField
{
    public class Field : IField
    {
        public Vector2Int Size { get; }
        
        private readonly List<int> _heights;

        public Field(Vector2Int size, List<int> heights)
        {
            Size = size;
            _heights = heights;
        }
        
        public bool IsPassableMove(Vector2Int from, Vector2Int to)
        {
            return IsNeighbours() && IsPossibleMoveHeight();

            bool IsNeighbours() => 
                (from - to).sqrMagnitude <= 1;

            bool IsPossibleMoveHeight() => 
                Mathf.Abs(GetHeight(from) - GetHeight(to)) <= 1;
        }

        public float DistanceBetween(Vector2Int from, Vector2Int to) => 
            Vector3Int.Distance(GetTop(from), GetTop(to));

        public bool IsExist(Vector2Int point)
        {
            if (point.x < 0 || point.x >= Size.x)
                return false;

            if (point.y < 0 || point.y >= Size.y)
                return false;

            return true;
        }

        public int GetHeight(Vector2Int position)
        {
            if (!IsExist(position))
                throw new ArgumentException($"Position ({position}) not in field ({Size})");
            
            var index = position.x + position.y * Size.x;
            return _heights[index];
        }

        public Vector3Int GetTop(Vector2Int position) => 
            new Vector3Int(position.x, GetHeight(position), position.y);
    }
}