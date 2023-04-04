using UnityEngine;

namespace TestProject.Services.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Get(string path) =>
            Resources.Load<GameObject>(path);
    }
}