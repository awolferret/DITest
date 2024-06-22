using UnityEngine;

namespace Character
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;

        private bool _isDead;
        
        private void OnEnable() => _heroHealth.HealthChanged += HealthChanged;

        private void OnDisable() => _heroHealth.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _heroHealth.CurrentHealth <= 0) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _move.enabled = false;
            _attack.enabled = false;
        }
    }
}