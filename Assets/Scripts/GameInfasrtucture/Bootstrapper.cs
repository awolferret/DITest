using GameInfrastructure.GameStateMachine.States;
using Logic;
using UnityEngine;

namespace GameInfrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_loadingScreenPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}