using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using TMPro;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopWindow : WindowBase
  {
    public TextMeshProUGUI SkullText;
    public RewardedAdItem AdItem;
    public ShopItemsContainer ShopItems;

    public void Construct(
      IAdsService adsService,
      IPersistentProgressService progressService,
      IIAPService iapService,
      IAssets assets)
    {
      base.Construct(progressService);
      AdItem.Construct(adsService, progressService);
      ShopItems.Construct(iapService, progressService, assets);
    }

    protected override void Initialize()
    {
      AdItem.Initialize();
      ShopItems.Initialize();
      RefreshSkullText();
    }

    protected override void SubscribeUpdates()
    {
      AdItem.Subscribe();
      ShopItems.Subscribe();
      Progress.WorldData.LootData.Changed += RefreshSkullText;
    }

    protected override void CleanUp()
    {
      base.CleanUp();
      AdItem.CleanUp();
      ShopItems.CleanUp();
      Progress.WorldData.LootData.Changed -= RefreshSkullText;
    }

    private void RefreshSkullText() =>
      SkullText.text = Progress.WorldData.LootData.Collected.ToString();
  }
}