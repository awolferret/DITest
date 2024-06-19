using System;
using GameInfasrtucture.Factory;
using GameInfasrtucture.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoverToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _minimalDistance;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.HeroCrated += InitializeHeroTransform;
        }

        private void Update()
        {
            if (_heroTransform && CheckDistance())
                _agent.destination = _heroTransform.position;
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
            _gameFactory.HeroCrated -= InitializeHeroTransform;
        }

        private bool CheckDistance() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}