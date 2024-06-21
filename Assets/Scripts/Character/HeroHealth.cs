using System;
using Data;
using GameInfasrtucture.Services.PersistentProgress;
using UnityEngine;

namespace Character
{
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        private HeroState _progressHeroState;

        public Action HealthChanged;

        public float CurrentHealth
        {
            get => _progressHeroState.CurrentHealth;
            set
            {
                if (_progressHeroState.CurrentHealth != value)
                {
                    HealthChanged?.Invoke();
                    _progressHeroState.CurrentHealth = value;
                }
            }
        }

        public float MaxHealth
        {
            get => _progressHeroState.MaxHealth;
            set => _progressHeroState.MaxHealth = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progressHeroState = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdareProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHealth = CurrentHealth;
            progress.HeroState.MaxHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
                return;

            CurrentHealth -= damage;
        }
    }
}