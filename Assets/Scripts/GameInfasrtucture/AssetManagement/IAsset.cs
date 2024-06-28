using System.Threading.Tasks;
using GameInfrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameInfrastructure.AssetManagement
{
    public interface IAsset : IService
    {
        Task<GameObject> Instantiate(string address);
        Task<GameObject> Instantiate(string address, Vector3 spawnPoint);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void CleanUp();

        Task<T> Load<T>(string assetAddress) where T : class;
        void Initialize();
    }
}