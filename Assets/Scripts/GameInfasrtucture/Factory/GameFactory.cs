using GameInfasrtucture.AssetManagement;
using UnityEngine;

namespace GameInfasrtucture.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject initialPoint) =>
            _assetProvider.Instantiate(Constants.HeroPath, initialPoint.transform.position);

        public void CreateHud() => _assetProvider.Instantiate(Constants.HUDPath);
    }
}