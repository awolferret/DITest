using System.Linq;
using Character;
using GameInfasrtucture;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services;
using UnityEngine;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float Cleavage = .5f;
        [SerializeField] private float _attackDistance = .5f;
        [SerializeField] private float _damage = 10;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _time;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCrated += OnHeroCreated;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.LayerName);
            UpdateTime();
        }

        private void OnHeroCreated()
        {
            _gameFactory.HeroCrated -= OnHeroCreated;
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private void Update()
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
                DoAttack();
        }

        private void DoAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<HeroHealth>().TakeDamage(_damage);
                Debug.Log("Attack");
                UpdateTime();
            }
        }

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * _attackDistance;

        private void UpdateTime() => _time = _attackCooldown;
    }
}