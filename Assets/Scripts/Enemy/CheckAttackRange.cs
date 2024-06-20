using UnityEngine;

namespace Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start() => DisableAttack();

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj) => EnableAttack();

        private void TriggerExit(Collider obj) => DisableAttack();

        private void DisableAttack() => _attack.enabled = false;

        private void EnableAttack() => _attack.enabled = true;
    }
}