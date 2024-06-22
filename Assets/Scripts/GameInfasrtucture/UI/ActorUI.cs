using Logic;
using UnityEngine;

namespace GameInfasrtucture.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private IHealth _heroHealth;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Constract(health);
        }

        private void OnDestroy()
        {
            if (_heroHealth != null)
                _heroHealth.HealthChanged -= UpdateHealthBar;
        }

        public void Constract(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar() => _healthBar.SetValue(_heroHealth.CurrentHealth, _heroHealth.MaxHealth);
    }
}