using Data;
using GameInfasrtucture;
using GameInfasrtucture.Services;
using GameInfasrtucture.Services.Input;
using GameInfasrtucture.Services.PersistentProgress;
using Logic;
using UnityEngine;

namespace Character
{
    public class HeroAttack : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _controller;

        private float _radius;
        private float _damage;
        private int _layer;
        private IInputService _input;
        private Collider[] _hit = new Collider[3];

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _layer = 1 << LayerMask.NameToLayer(Constants.LayerNameHitable);
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp())
            {
                Debug.Log("HeroAttack");
                OnAttack();
            }
        }

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                if (_hit[i].transform.parent.TryGetComponent(out IHealth health))
                    health.TakeDamage(_damage);
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _radius, _hit, _layer);

        private Vector3 StartPoint() => new Vector3(transform.position.x, _controller.height / 2, transform.position.z);

        public void LoadProgress(PlayerProgress progress)
        {
            _damage = progress.HeroStats.Damage;
            _radius = progress.HeroStats.DamageRadius;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}