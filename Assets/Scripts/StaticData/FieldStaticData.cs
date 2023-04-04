using UnityEngine;

namespace TestProject.StaticData
{
    [CreateAssetMenu(menuName = "Game/StaticData/FieldData", fileName = "FieldData")]
    public class FieldStaticData : ScriptableObject
    {
        public TextAsset Config;
        public Vector3 StartPosition;
        public Vector3 Offset;
        public LayerMask BlockLayer;
    }
}