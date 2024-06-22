using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public Stats HeroStats;
        public KillData KillData;

        public PlayerProgress(string name)
        {
            WorldData = new WorldData(name);
            HeroState = new HeroState();
            HeroStats = new Stats();
            KillData = new KillData();
        }
    }
}