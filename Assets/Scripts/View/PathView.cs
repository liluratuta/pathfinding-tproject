using System.Linq;
using TestProject.GameField;
using TestProject.PathFinding;
using TestProject.Utility;
using UnityEngine;

namespace TestProject.View
{
    public class PathView : MonoBehaviour
    {
        public LineRenderer Line;
        
        private IPositionConverter _positionConverter;
        private IPathFinder _pathFinder;
        private IField _field;

        public void Init(IPositionConverter positionConverter, IPathFinder pathFinder, IField field)
        {
            _positionConverter = positionConverter;
            _pathFinder = pathFinder;
            _field = field;
        }
        
        public void Show(Vector2Int from, Vector2Int to)
        {
            var path = _pathFinder.Find(from, to);
            Line.positionCount = path.Length;
            Line.SetPositions(path.Select(GetPathNodePosition).ToArray());
        }

        private Vector3 GetPathNodePosition(Vector2Int fieldPosition)
        {
            var worldPosition = _positionConverter.ConvertToWorld(fieldPosition, useOffset: true);
            worldPosition.y = _field.GetHeight(fieldPosition) + 0.5f;
            return worldPosition;
        }

        public void Hide()
        {
            Line.positionCount = 0;
        }
    }
}