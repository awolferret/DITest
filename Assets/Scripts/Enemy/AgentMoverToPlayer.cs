using GameInfasrtucture.Factory;
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

        public void Constract(Transform hero, float speed)
        {
            _heroTransform = hero;
            _agent.speed = speed;
        }
        
        private void Update()
        {
            if (_heroTransform && CheckDistance())
                _agent.destination = _heroTransform.position;
        }
        
        private bool CheckDistance() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}