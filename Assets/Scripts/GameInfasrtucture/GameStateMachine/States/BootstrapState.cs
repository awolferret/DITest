﻿using GameInfrastructure.AssetManagement;
using GameInfrastructure.Factory;
using GameInfrastructure.Services;
using GameInfrastructure.Services.Input;
using GameInfrastructure.Services.PersistentProgress;
using GameInfrastructure.Services.PersistentProgress.SaveLoad;
using GameInfrastructure.UI.Services.UIFactory;
using GameInfrastructure.UI.Services.Windows;
using StaticData;
using UnityEngine;

namespace GameInfrastructure.GameStateMachine.States
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

        public void Enter() => 
            _sceneLoader.Load(Constants.BootstrapSceneName, OnLoaded: EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle(InputService());
            RegisterAssetProvider();
            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAsset>(),
                _services.Single<IStaticDataService>(), _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAsset>(), _services.Single<IStaticDataService>(),
                    _services.Single<IPersistentProgressService>(), _services.Single<IWindowService>()));
            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterAssetProvider()
        {
            Asset asset = new Asset();
            asset.Initialize();
            _services.RegisterSingle<IAsset>(asset);
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
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