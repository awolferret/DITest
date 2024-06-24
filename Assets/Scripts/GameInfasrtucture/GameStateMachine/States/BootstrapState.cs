using System.ComponentModel;
using GameInfasrtucture.AssetManagement;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.Input;
using GameInfasrtucture.Services.PersistentProgress;
using GameInfasrtucture.Services.PersistentProgress.SaveLoad;
using StaticData;
using UnityEngine;

namespace GameInfasrtucture.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.BootstrapSceneName, OnLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IAsset>(new Asset());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAsset>(), _services.Single<StaticDataService>(),
                    _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            StaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandAloneInputService();
            else
                return new MobileInputService();
        }
    }
}