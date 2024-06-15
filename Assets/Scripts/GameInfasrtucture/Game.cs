using UnityEngine;

public class Game : MonoBehaviour
{
    public static IInputService InputService;
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
    }
}