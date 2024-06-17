using System.Collections.Generic;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
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
    }
}