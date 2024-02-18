using System;
using TMPro;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : BaseWindow
    {
        public TextMeshProUGUI skullText;

		protected override void Initialize() =>
			RefreshSkullText();

		protected override void SubscribeUpdates() =>
			Progress.WorldData.LootData.Changed += RefreshSkullText;

		protected override void Cleanup()
		{
			base.Cleanup();
			Progress.WorldData.LootData.Changed -= RefreshSkullText;
		}

		private void RefreshSkullText()
		{
			skullText.text = Progress.WorldData.LootData.Collected.ToString();
		}
	}
}