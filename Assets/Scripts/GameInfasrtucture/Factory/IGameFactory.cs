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
        GameObject CreateHero(GameObject initialPoint);
        void CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        void Register(ISavedProgressReader savedProgress);
        GameObject CreateMonster(MonsterTypeId monsterType, Transform parent);
        LootPiece CreateLoot();
    }
}