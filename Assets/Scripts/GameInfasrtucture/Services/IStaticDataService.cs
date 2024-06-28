using GameInfrastructure.UI.Services.Windows;
using StaticData;
using StaticData.Windows;

namespace GameInfrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId shop);
    }
}