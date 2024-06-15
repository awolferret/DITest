using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingScreen = loadingScreen;
    }

    public void Enter(string sceneName)
    {
        _loadingScreen.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => _loadingScreen.Hide();

    private void OnLoaded()
    {
        GameObject initialPoint = GameObject.FindWithTag("InitialPoint");
        GameObject hero = Instantiate(Constants.HeroPath, initialPoint.transform.position);
        Instantiate(Constants.HUDPath);
        CameraFollow(hero);
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void CameraFollow(GameObject hero) => Camera.main.GetComponent<CameraFollow>().Follow(hero);

    private static GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    private static GameObject Instantiate(string path, Vector3 spawnPoint)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}