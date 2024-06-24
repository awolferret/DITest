using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterType;
        public Vector3 Position;

        public EnemySpawnerData(string id, MonsterTypeId monsterType, Vector3 position)
        {
            Id = id;
            MonsterType = monsterType;
            Position = position;
        }
    }
}