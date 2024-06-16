using GameInfasrtucture.Services;
using UnityEngine;

namespace GameInfasrtucture.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 spawnPoint);
    }
}