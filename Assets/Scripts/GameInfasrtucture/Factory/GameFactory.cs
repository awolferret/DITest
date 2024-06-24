using System.Collections.Generic;
using Enemy;
using GameInfasrtucture.AssetManagement;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
using GameInfasrtucture.UI;
using Logic;
using Logic.EnemySpawners;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameInfasrtucture.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject _heroGameObject { get; set; }

        public GameFactory(IAsset asset, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _asset = asset;
            _staticData = staticData;
            _progressService = progressService;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            _heroGameObject = InstantiateRegistered(Constants.HeroPath, initialPoint.transform.position);
            return _heroGameObject;
        }

        public void CreateHud()
        {
            GameObject hud = InstantiateRegistered(Constants.HUDPath);
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.PlayerProgress.WorldData);
            hud.GetComponent<ActorUI>().Constract(_heroGameObject.GetComponent<IHealth>());
        }

        public GameObject CreateMonster(MonsterTypeId monsterType, Transform parent)
        {
            Debug.Log(_staticData);
            MonsterStaticData monsterData = _staticData.ForMonster(monsterType);
            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);
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

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(Constants.LootPath)
                .GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.PlayerProgress.WorldData);
            return lootPiece;
        }

        public void CreateSpawner(Vector3 position, string id, MonsterTypeId monsterType)
        {
            SpawnPoint spawner = InstantiateRegistered(Constants.SpawnerPath, position)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Init(id, monsterType);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _asset.Instantiate(prefabPath);
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