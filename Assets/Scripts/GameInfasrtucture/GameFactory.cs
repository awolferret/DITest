using UnityEngine;

public class GameFactory : IGameFactory
{
    public GameObject CreateHero(GameObject initialPoint) =>
        Instantiate(Constants.HeroPath, initialPoint.transform.position);

    public void CreateHud() => Instantiate(Constants.HUDPath);

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