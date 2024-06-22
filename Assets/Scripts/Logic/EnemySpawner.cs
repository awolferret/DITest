using Data;
using GameInfasrtucture.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterType;
        private string _id;

        private bool _slain;

        private void Awake() => 
            _id = GetComponent<UniqueId>().Id;

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
        }
    }
}