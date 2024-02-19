using System;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using TMPro;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : BaseWindow
    {
        public TextMeshProUGUI skullText;
        public RewardedAdItem AdItem;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
	        base.Construct(progressService);
	        AdItem.Construct(adsService, progressService);
        }
        
		protected override void Initialize()
		{
			AdItem.Initialize();
			RefreshSkullText();
		}

		protected override void SubscribeUpdates()
		{
			AdItem.Subscribe();
			Progress.WorldData.LootData.Changed += RefreshSkullText;
		}

		protected override void Cleanup()
		{
			base.Cleanup();
			AdItem.Cleanup();
			Progress.WorldData.LootData.Changed -= RefreshSkullText;
		}

		private void RefreshSkullText()
		{
			skullText.text = Progress.WorldData.LootData.Collected.ToString();
		}
	}
}