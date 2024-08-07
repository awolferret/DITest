﻿using Data;
using GameInfrastructure.GameStateMachine.States;
using GameInfrastructure.Services.PersistentProgress;
using GameInfrastructure.Services.PersistentProgress.SaveLoad;

namespace GameInfrastructure.GameStateMachine
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

        private void LoadProgressOnInitNew() =>
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerProgress playerProgress = new PlayerProgress(Constants.GameSceneName);

            playerProgress.HeroState.MaxHealth = 50f;
            playerProgress.HeroState.ResetHP();
            playerProgress.HeroStats.Damage = 1f;
            playerProgress.HeroStats.DamageRadius = .5f;

            return playerProgress;
        }
    }
}