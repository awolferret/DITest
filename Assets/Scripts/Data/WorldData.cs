using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string name)
        {
            PositionOnLevel = new PositionOnLevel(name);
        }
    }
}