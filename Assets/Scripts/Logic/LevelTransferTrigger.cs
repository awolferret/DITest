using Character;
using GameInfasrtucture.GameStateMachine;
using GameInfasrtucture.GameStateMachine.States;
using GameInfasrtucture.Services;
using UnityEngine;

namespace Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _transferTo;

        private IGameStateMachine _stateMachine;
        private bool _activated;

        private void Awake() =>
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();

        private void OnTriggerEnter(Collider other)
        {
            if (_activated)
                return;

            if (other.GetComponent<HeroHealth>())
            {
                _stateMachine.Enter<LoadLevelState, string>(_transferTo);
                _activated = true;
            }
        }
    }
}