using CameraLogic;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
using Logic;
using StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameInfasrtucture.GameStateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingScreen.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            InitSpawners();
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(Constants.Initialpoint));
            InitHud();
            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            string SceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(SceneKey);

            for (int i = 0; i < levelData.EnemySpawners.Count; i++)
            {
                _gameFactory.CreateSpawner(levelData.EnemySpawners[i].Position, levelData.EnemySpawners[i].Id,
                    levelData.EnemySpawners[i].MonsterType);
            }
        }

        private void InitHud() => _gameFactory.CreateHud();

        private static void CameraFollow(GameObject hero) => Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}