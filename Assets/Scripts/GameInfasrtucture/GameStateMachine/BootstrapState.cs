using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        RegisterServices();
        _sceneLoader.Load(Constants.BootstrapSceneName, OnLoaded: EnterLoadLeve);
    }


    public void Exit()
    {
    }

    private void EnterLoadLeve() => _gameStateMachine.Enter<LoadLevelState, string>("Game");

    private void RegisterServices() => Game.InputService = RegisterInputService();

    private static IInputService RegisterInputService()
    {
        if (Application.isEditor)
            return new StandAloneInputService();
        else
            return new MobileInputService();
    }
}