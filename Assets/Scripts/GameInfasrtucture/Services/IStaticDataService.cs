﻿using GameInfasrtucture.UI.Services.Windows;
using StaticData;
using StaticData.Windows;

namespace GameInfasrtucture.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId shop);
    }
}