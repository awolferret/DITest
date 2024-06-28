using System.Threading.Tasks;
using GameInfrastructure.AssetManagement;
using GameInfrastructure.Services;
using GameInfrastructure.Services.PersistentProgress;
using GameInfrastructure.UI.Services.Windows;
using StaticData.Windows;
using UnityEngine;

namespace GameInfrastructure.UI.Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAsset _assetProvider;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;
        private readonly IPersistentProgressService _progressService;

        public UIFactory(IAsset assetProvider, IStaticDataService staticData,
            IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(Constants.UIRootPath);
            _uiRoot = root.transform;
        }
    }
}