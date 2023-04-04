using UnityEngine;

namespace TestProject.GameField
{
    public interface IField
    {
        Vector2Int Size { get; }
        bool IsPassableMove(Vector2Int from, Vector2Int to);
        float DistanceBetween(Vector2Int from, Vector2Int to);
        bool IsExist(Vector2Int point);
        int GetHeight(Vector2Int position);
        Vector3Int GetTop(Vector2Int position);
    }
}