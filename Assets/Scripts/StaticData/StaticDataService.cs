using System.Collections.Generic;
using System.Linq;
using GameInfasrtucture;
using GameInfasrtucture.Services;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {

        private Dictionary<MonsterTypeId,MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(Constants.StaticdataMonstersPath)
                .ToDictionary(x => x.MonsterTypeId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) => 
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData) ? staticData : null;
    }
}