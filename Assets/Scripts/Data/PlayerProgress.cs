using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string name)
        {
            WorldData = new WorldData(name);
        }
    }
}