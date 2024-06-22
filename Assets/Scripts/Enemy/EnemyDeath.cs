using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;

        public event Action OnDeath;

        private void OnEnable()
        {
            _enemyHealth.HealthChanged += OnHelthChanged;
        }

        private void OnDisable()
        {
            _enemyHealth.HealthChanged -= OnHelthChanged;
        }

        private void OnHelthChanged()
        {
            if (_enemyHealth.CurrentHealth <= 0) 
                Die();
        }

        private void Die()
        {
            OnDeath?.Invoke();
            StartCoroutine(DestroyItself());
        }

        private IEnumerator DestroyItself()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}