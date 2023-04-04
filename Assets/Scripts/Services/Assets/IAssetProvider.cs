using UnityEngine;

namespace TestProject.Services.Assets
{
    public interface IAssetProvider
    {
        GameObject Get(string path);
    }
}