using Character;
using Data;
using UnityEngine;

namespace Enemy
{
    public class LootPiece : MonoBehaviour
    {
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData) =>
            _worldData = worldData;

        public void Init(Loot lootPiece) =>
            _loot = lootPiece;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<HeroHealth>())
                PickUp();
        }

        private void PickUp()
        {
            if (!_picked)
            {
                _picked = true;

                Debug.Log("Wd" + _worldData);
                _worldData.LootData.Collect(_loot);
                Destroy(gameObject);
            }
        }
    }
}