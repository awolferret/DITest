using Data;

namespace GameInfasrtucture.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdareProgress(PlayerProgress progress);
    }
}