using UnityEngine;

namespace GameInfrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}
