using System.Numerics;

namespace Code.Services.Input
{
    public class StandaloneInputServices : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();
                
                if (axis == Vector2.Zero) 
                    axis = UnityInputAxis();
                
                return axis;
            }
        }

        private static Vector2 UnityInputAxis() => 
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}