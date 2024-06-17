using Data;
using GameInfasrtucture.GameStateMachine.States;
using GameInfasrtucture.Services.PersistentProgress;
using GameInfasrtucture.Services.PersistentProgress.SaveLoad;

namespace GameInfasrtucture.GameStateMachine
{
    public class LoadProgressState : IState
    {

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOnInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel
                .Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOnInitNew()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress() =>
            new PlayerProgress(Constants.GameSceneName);
    }
}