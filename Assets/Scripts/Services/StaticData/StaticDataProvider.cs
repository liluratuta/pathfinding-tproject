using TestProject.StaticData;
using UnityEngine;

namespace TestProject.Services.StaticData
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string FieldDataPath = "FieldData";
        
        public FieldStaticData FieldData { get; private set; }
        
        public void Load()
        {
            FieldData = Resources.Load<FieldStaticData>(FieldDataPath);
        }
    }
}