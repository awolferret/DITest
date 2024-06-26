using CameraLogic;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.PersistentProgress;
using GameInfasrtucture.UI.Services.UIFactory;
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
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
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
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.PlayerProgress);
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitGameWorld()
        {
            LevelStaticData levelData = GetLevelData();
            InitSpawners(levelData);
            GameObject hero = InitHero(levelData);
            InitHud();
            CameraFollow(hero);
        }

        private LevelStaticData GetLevelData() => 
            _staticData.ForLevel(SceneManager.GetActiveScene().name);

        private void InitSpawners(LevelStaticData levelData)
        {
            for (int i = 0; i < levelData.EnemySpawners.Count; i++)
                _gameFactory.CreateSpawner(levelData.EnemySpawners[i].Position, levelData.EnemySpawners[i].Id,
                    levelData.EnemySpawners[i].MonsterType);
        }

        private void InitHud() =>
            _gameFactory.CreateHud();

        private GameObject InitHero(LevelStaticData levelData) =>
            _gameFactory.CreateHero(levelData.InitialPoint);

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}