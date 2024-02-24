using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItem : MonoBehaviour
    {
        public Button BuyItemButton;
        public TMP_Text PriceText;
        public TMP_Text QuantityText;
        public TMP_Text AvailableItemsLeftText;
        public Image Icon;

        private IIAPService _iapService;
        private ProductDescription _productDescription;
        private IAssets _assets;

        public void Construct(IIAPService iapService, ProductDescription productDescription, IAssets assets)
        {
            _iapService = iapService;
            _productDescription = productDescription;
            _assets = assets;
        }

        public async void Initialize()
        {
            BuyItemButton.onClick.AddListener(OnBuyItemClick);

            PriceText.text = _productDescription.Config.Price;
            QuantityText.text = _productDescription.Config.Quantity.ToString();
            AvailableItemsLeftText.text = _productDescription.AvailablePurchasesLeft.ToString();
            Icon.sprite = await _assets.Load<Sprite>(_productDescription.Config.Icon);
        }

        private void OnBuyItemClick() => 
            _iapService.StartPurchase(_productDescription.Id);
    }
}