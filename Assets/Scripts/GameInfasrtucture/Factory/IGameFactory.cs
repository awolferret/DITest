using GameInfasrtucture.Services;
using UnityEngine;

namespace GameInfasrtucture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
        void CreateHud();
    }
}