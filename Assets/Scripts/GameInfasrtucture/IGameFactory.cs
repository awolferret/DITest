using UnityEngine;

public interface IGameFactory
{
    GameObject CreateHero(GameObject initialPoint);
    void CreateHud();
}