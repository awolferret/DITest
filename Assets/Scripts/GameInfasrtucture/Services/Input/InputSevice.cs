using UnityEngine;

namespace GameInfrastructure.Services.Input
{
    public abstract class InputSevice : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string ButtonName = "Fire";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(ButtonName);

        protected static Vector2 SimpleInputAxis() => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}