using Data;
using GameInfrastructure.Factory;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory) => _factory = factory;

        private void OnEnable() =>
            _enemyDeath.OnDeath += SpawnLoot;

        private void OnDisable() =>
            _enemyDeath.OnDeath -= SpawnLoot;

        private async void SpawnLoot()
        {
            LootPiece loot = await _factory.CreateLoot();
            loot.transform.position = transform.position;
            Loot lootItem = SetLootValue();
            loot.Init(lootItem);
        }

        private Loot SetLootValue() =>
            new()
            {
                Value = Random.Range(_lootMin, _lootMax)
            };

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}