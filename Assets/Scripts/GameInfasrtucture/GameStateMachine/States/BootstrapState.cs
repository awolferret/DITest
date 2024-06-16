using GameInfasrtucture.AssetManagement;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.Input;
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

        private void EnterLoadLevel() => _gameStateMachine.Enter<LoadLevelState, string>("Game");

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssetProvider>()));
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