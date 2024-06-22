using System;
using System.Collections.Generic;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
using UnityEngine;

namespace GameInfasrtucture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
        GameObject HeroGameObject { get; set; }
        event Action HeroCrated;
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        void Register(ISavedProgressReader savedProgress);
    }
}