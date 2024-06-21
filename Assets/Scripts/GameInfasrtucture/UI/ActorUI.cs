using System;
using Character;
using UnityEngine;

namespace GameInfasrtucture.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private HeroHealth _heroHealth;

        private void OnDestroy() =>
            _heroHealth.HealthChanged -= UpdateHealthBar;

        public void Constract(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar() => _healthBar.SetValue(_heroHealth.CurrentHealth,_heroHealth.MaxHealth);
    }
}