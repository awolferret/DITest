using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using GameInfrastructure.Services;
using GameInfrastructure.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace GameInfrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateHero(Vector3 initialPoint);
        Task CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        Task<GameObject> CreateMonster(MonsterTypeId monsterType, Transform parent);
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(Vector3 position, string id, MonsterTypeId monsterType);
        Task WarmUp();
    }
}