using Data;

namespace GameInfrastructure.Services.PersistentProgress.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}