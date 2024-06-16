using GameInfasrtucture.GameStateMachine.States;
using UI;
using UnityEngine;

namespace GameInfasrtucture
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;
    
        private Game _game;

        private void Awake()
        {
            _game = new Game(this,_loadingScreen);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}