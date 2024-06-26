using System.Collections.Generic;
using Enemy;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace GameInfasrtucture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 initialPoint);
        void CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        GameObject CreateMonster(MonsterTypeId monsterType, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(Vector3 position, string id, MonsterTypeId monsterType);
    }
}