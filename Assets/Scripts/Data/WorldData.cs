using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;

        public WorldData(string name)
        {
            PositionOnLevel = new PositionOnLevel(name);
            LootData = new LootData();
        }
    }
}