using System;
using System.Collections.Generic;
using GameInfasrtucture.AssetManagement;
using GameInfasrtucture.Services.PersistentProgress;
using UnityEngine;

namespace GameInfasrtucture.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; set; }
        public event Action HeroCrated;

        public GameFactory(IAsset asset)
        {
            _asset = asset;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            HeroGameObject = InstantiateRegistered(Constants.HeroPath, initialPoint.transform.position);
            HeroCrated?.Invoke();
            return HeroGameObject;
        }

        public void CreateHud() =>
            InstantiateRegistered(Constants.HUDPath);


        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
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

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}