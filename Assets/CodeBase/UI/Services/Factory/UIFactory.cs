using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        
        private IAssetProvider _assets;
        private IStaticDataService _staticData;
        
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowId.Shop);
            Object.Instantiate(config.Prefab, _uiRoot);
        }
        
        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}