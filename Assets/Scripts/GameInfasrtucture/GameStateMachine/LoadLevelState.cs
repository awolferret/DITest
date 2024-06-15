﻿using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName) => _sceneLoader.Load(sceneName, OnLoaded);

    public void Exit()
    {
    }

    private void OnLoaded()
    {
        GameObject hero = Instantiate("Hero/Cube");
        Instantiate("Hud/Hud");
        CameraFollow(hero);
    }

    private void CameraFollow(GameObject hero) => Camera.main.GetComponent<CameraFollow>().Follow(hero);

    private static GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }
}