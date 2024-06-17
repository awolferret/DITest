using GameInfasrtucture.GameStateMachine.States;
using Logic;
using UnityEngine;

namespace GameInfasrtucture
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