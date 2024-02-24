using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemsContainer : MonoBehaviour
    {
        private const string ShopItemPath = "ShopItem";
        
        public GameObject[] UnavailableObjects;
        public Transform parent;
        
        private IIAPService _iapService;
        private IPersistentProgressService _progressService;
        private IAssets _assets;
        private List<GameObject> _shopItems = new();

        public void Construct(IIAPService iapService, IPersistentProgressService progressService, IAssets assets)
        {
            _iapService = iapService;
            _progressService = progressService;
            _assets = assets;
        }

        public void Initialize() => 
            RefreshAvailableItems();

        public void Subscribe()
        {
            _iapService.Initialized += RefreshAvailableItems;
            _progressService.Progress.PurchaseData.Changed += RefreshAvailableItems;
        }

        public void CleanUp()
        {
            _iapService.Initialized -= RefreshAvailableItems;
            _progressService.Progress.PurchaseData.Changed -= RefreshAvailableItems;
        }

        private async void RefreshAvailableItems()
        {
            UpdateUnavailableObjects();

            if(!_iapService.IsInitialized)
                return;

            foreach (var shopItem in _shopItems) 
                Destroy(shopItem);
            
            _shopItems.Clear();
            
            await FillShopItems();
        }

        private async Task FillShopItems()
        {
            foreach (var productDescription in _iapService.Products())
            {
                var shopItemObject = await _assets.Instantiate(ShopItemPath, parent);
                var shopItem = shopItemObject.GetComponent<ShopItem>();
                
                shopItem.Construct(_iapService, productDescription, _assets);
                shopItem.Initialize();
                
                _shopItems.Add(shopItem.gameObject);
            }
        }

        private void UpdateUnavailableObjects()
        {
            foreach (var unavailableObject in UnavailableObjects) 
                unavailableObject.SetActive(!_iapService.IsInitialized);
        }
    }
}