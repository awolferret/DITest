using CameraLogic;
using GameInfasrtucture.Factory;
using UI;
using UnityEngine;

namespace GameInfasrtucture.GameStateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingScreen.Hide();

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag("InitialPoint");
            GameObject hero = _gameFactory.CreateHero(initialPoint);
            _gameFactory.CreateHud();
            CameraFollow(hero);
            _gameStateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject hero) => Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}