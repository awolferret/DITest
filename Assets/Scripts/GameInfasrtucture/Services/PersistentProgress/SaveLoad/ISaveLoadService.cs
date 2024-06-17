using Data;

namespace GameInfasrtucture.Services.PersistentProgress.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}