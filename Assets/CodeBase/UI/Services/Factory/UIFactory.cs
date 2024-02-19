using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using CodeBase.StaticData;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        
        private IAssetProvider _assets;
        private IStaticDataService _staticData;
        
        private Transform _uiRoot;
		private IPersistentProgressService _progressService;
		private IAdsService _adsService;

		public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService, IAdsService adsService)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_adsService = adsService;
		}

		public void CreateShop()
        {
            var config = _staticData.ForWindow(WindowId.Shop);
			var shopWindow = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            shopWindow.Construct(_adsService, _progressService);
        }
        
        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}