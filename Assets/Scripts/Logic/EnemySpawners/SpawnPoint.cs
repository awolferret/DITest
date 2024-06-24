using Data;
using Enemy;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        private MonsterTypeId _monsterType;
        private string _id;
        private bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public bool Slain => _slain;

        public void Construct(IGameFactory factory) => _factory = factory;

        public void Init(string id, MonsterTypeId monsterType)
        {
            _id = id;
            _monsterType = monsterType;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(_monsterType, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            if (_enemyDeath)
                _enemyDeath.OnDeath -= OnDeath;

            _slain = true;
        }
    }
}