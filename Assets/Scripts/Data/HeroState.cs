using System;

namespace Data
{
    [Serializable]
    public class HeroState
    {
        public float CurrentHealth;
        public float MaxHealth;

        public void ResetHP() => CurrentHealth = MaxHealth;
    }
}