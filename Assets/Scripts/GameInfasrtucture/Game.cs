using GameInfasrtucture.Services;
using Logic;
using UnityEngine;

namespace GameInfasrtucture
{
    public class Game : MonoBehaviour
    {
        public readonly GameStateMachine.GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine.GameStateMachine(new SceneLoader(coroutineRunner),loadingScreen, AllServices.Container);
        }
    }
}