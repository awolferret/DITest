using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        
        public PlayerProgress(string name)
        {
            WorldData = new WorldData(name);
            HeroState = new HeroState();
        }
    }
}