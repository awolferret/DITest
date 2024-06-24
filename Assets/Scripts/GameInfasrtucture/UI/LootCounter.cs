using Data;
using TMPro;
using UnityEngine;

namespace GameInfasrtucture.UI
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lootText;

        private WorldData _worldData;

        private void Start() => 
            UpdateCounter();

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        private void OnDisable() => 
            _worldData.LootData.Changed -= UpdateCounter;

        private void UpdateCounter() => 
            _lootText.text = $"{_worldData.LootData.Collected}";
    }
}