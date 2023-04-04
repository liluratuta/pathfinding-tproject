using TestProject.Services.StaticData;
using TestProject.StaticData;
using UnityEngine;

namespace TestProject.Utility
{
    public class PositionConverter : IPositionConverter
    {
        private readonly FieldStaticData _fieldData;

        public PositionConverter(IStaticDataProvider dataProvider)
        {
            _fieldData = dataProvider.FieldData;
        }

        public Vector3 ConvertToWorld(Vector3 position, bool useOffset = false)
        {
            var p = position;
            p.z = -p.z;
            return _fieldData.StartPosition + p + GetOffset(useOffset);
        }

        public Vector3 ConvertToWorld(Vector2 position, bool useOffset = false) => 
            _fieldData.StartPosition + new Vector3(position.x, 0, -position.y) + GetOffset(useOffset);

        public Vector2Int ConvertToField(Vector3 position, bool useOffset = false)
        {
            var fp = position - _fieldData.StartPosition - GetOffset(useOffset);
            return new Vector2Int(Mathf.RoundToInt(fp.x), Mathf.RoundToInt(-fp.z));
        }

        private Vector3 GetOffset(bool useOffset) => 
            useOffset ? _fieldData.Offset : Vector3.zero;
    }
}