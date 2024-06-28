using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using GameInfrastructure.AssetManagement;
using GameInfrastructure.Services;
using GameInfrastructure.Services.PersistentProgress;
using GameInfrastructure.UI.Elements;
using GameInfrastructure.UI.Services.Windows;
using Logic;
using Logic.EnemySpawners;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameInfrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject _heroGameObject { get; set; }

        public async Task WarmUp()
        {
            await _asset.Load<GameObject>(Constants.LootAddress);
            await _asset.Load<GameObject>(Constants.SpawnerAddress);
        }

        public GameFactory(IAsset asset, IStaticDataService staticData, IPersistentProgressService progressService,
            IWindowService windowService)
        {
            _asset = asset;
            _staticData = staticData;
            _progressService = progressService;
            _windowService = windowService;
        }

        public async Task<GameObject> CreateHero(Vector3 initialPoint) =>
            _heroGameObject = await InstantiateRegisteredAsync(Constants.HeroPath, initialPoint);

        public async Task CreateHud()
        {
            GameObject hud = await InstantiateRegisteredAsync(Constants.HUDPath);
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.PlayerProgress.WorldData);
            hud.GetComponent<ActorUI>().Constract(_heroGameObject.GetComponent<IHealth>());

            foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
                button.Construct(_windowService);
        }

        public async Task<GameObject> CreateMonster(MonsterTypeId monsterType, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterType);

            GameObject prefab = await _asset.Load<GameObject>(monsterData.PrefabReference);

            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHealth = monsterData.Health;
            health.MaxHealth = monsterData.Health;
            monster.GetComponent<ActorUI>().Constract(health);
            monster.GetComponent<AgentMoverToPlayer>().Constract(_heroGameObject.transform, monsterData.MoveSpeed);
            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(_heroGameObject.transform, monsterData.Damage, monsterData.Range, monsterData.Cleavage,
                monsterData.AttackCoolDown);

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterData.MinValue, monsterData.MaxValue);
            lootSpawner.Construct(this);

            return monster;
        }

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _asset.Load<GameObject>(Constants.LootAddress);

            LootPiece lootPiece = InstantiateRegistered(prefab)
                .GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.PlayerProgress.WorldData);
            return lootPiece;
        }

        public async Task CreateSpawner(Vector3 position, string id, MonsterTypeId monsterType)
        {
            GameObject prefab = await _asset.Load<GameObject>(Constants.SpawnerAddress);
            SpawnPoint spawner = InstantiateRegistered(prefab, position)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Init(id, monsterType);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _asset.CleanUp();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _asset.Instantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _asset.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}