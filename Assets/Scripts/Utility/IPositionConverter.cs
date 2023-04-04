using UnityEngine;

namespace TestProject.Utility
{
    public interface IPositionConverter
    {
        Vector3 ConvertToWorld(Vector3 position, bool useOffset = false);
        Vector3 ConvertToWorld(Vector2 position, bool useOffset = false);
        Vector2Int ConvertToField(Vector3 position, bool useOffset = false);
    }
}