using System.Numerics;

namespace Code.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fly";

        public abstract Vector2 Axis { get; }

        public bool IsAxelerationButtonUp() => 
            SimpleInput.GetButtonDown(Button);

        protected static Vector2 SimpleInputAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}