using TMPro;
using UnityEngine;

namespace GameInfasrtucture.UI
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TMP_Text _coinText;

        protected override void Initialize() =>
            RefreshCoinText();

        protected override void SubscribeUpdates() =>
            Progress.WorldData.LootData.Changed += RefreshCoinText;

        protected override void CleanUp()
        {
            base.CleanUp();
            Progress.WorldData.LootData.Changed -= RefreshCoinText;
        }

        private void RefreshCoinText() =>
            _coinText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}