using System.Numerics;

namespace Code.Services.Input
{
    public class MobileInputServices : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}