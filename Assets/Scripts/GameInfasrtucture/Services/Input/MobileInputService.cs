using UnityEngine;

namespace GameInfasrtucture.Services.Input
{
    class MobileInputService : InputSevice
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}
