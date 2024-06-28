using System.Threading.Tasks;
using CameraLogic;
using GameInfrastructure.Factory;
using GameInfrastructure.Services;
using GameInfrastructure.Services.PersistentProgress;
using GameInfrastructure.UI.Services.UIFactory;
using Logic;
using StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameInfrastructure.GameStateMachine.States
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
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingScreen.Hide();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.PlayerProgress);
        }

        private async Task InitUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async Task InitGameWorld()
        {
            LevelStaticData levelData = GetLevelData();
            await InitSpawners(levelData);
            GameObject hero = await InitHero(levelData);
            await InitHud();
            CameraFollow(hero);
        }

        private LevelStaticData GetLevelData() =>
            _staticData.ForLevel(SceneManager.GetActiveScene().name);

        private async Task InitSpawners(LevelStaticData levelData)
        {
            for (int i = 0; i < levelData.EnemySpawners.Count; i++)
                await _gameFactory.CreateSpawner(levelData.EnemySpawners[i].Position, levelData.EnemySpawners[i].Id,
                    levelData.EnemySpawners[i].MonsterType);
        }

        private async Task InitHud() =>
            await _gameFactory.CreateHud();

        private async Task<GameObject> InitHero(LevelStaticData levelData) =>
            await _gameFactory.CreateHero(levelData.InitialPoint);

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}