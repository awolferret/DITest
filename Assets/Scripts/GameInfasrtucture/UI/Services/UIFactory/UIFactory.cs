using GameInfasrtucture.AssetManagement;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
using GameInfasrtucture.UI.Services.Windows;
using StaticData.Windows;
using UnityEngine;

namespace GameInfasrtucture.UI.Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAsset _assetProvider;
        private readonly IStaticDataService _staticData;
        
        private Transform _uiRoot;
        private readonly IPersistentProgressService _progressService;

        public UIFactory(IAsset assetProvider, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab,_uiRoot);
            window.Construct(_progressService);
        }
        
        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(Constants.UIRootPath).transform;
    }
}