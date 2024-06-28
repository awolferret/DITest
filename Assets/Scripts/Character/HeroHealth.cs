using System;
using Data;
using GameInfrastructure.Services.PersistentProgress;
using Logic;
using UnityEngine;

namespace Character
{
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        private HeroState _progressHeroState;

        public event Action HealthChanged;

        public float CurrentHealth
        {
            get => _progressHeroState.CurrentHealth;
            set
            {
                if (_progressHeroState.CurrentHealth != value)
                {
                    _progressHeroState.CurrentHealth = value;
                    HealthChanged?.Invoke();
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

        public void UpdateProgress(PlayerProgress progress)
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