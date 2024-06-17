using Data;
using GameInfasrtucture.Factory;
using UnityEngine;

namespace GameInfasrtucture.Services.PersistentProgress.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress ProgressWriter in _gameFactory.ProgressWriters)
                ProgressWriter.UpdareProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(Constants.ProgressKey, _progressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(Constants.ProgressKey)?
                .ToDeserealized<PlayerProgress>();
    }
}