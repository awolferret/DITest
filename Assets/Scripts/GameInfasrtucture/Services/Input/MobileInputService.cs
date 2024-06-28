using UnityEngine;

namespace GameInfrastructure.Services.Input
{
    class MobileInputService : InputSevice
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}
