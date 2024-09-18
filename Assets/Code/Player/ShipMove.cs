using Code.Services.Input;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShipMove : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float rotationSpeed = 2f;

        private Rigidbody _rb;
        private IInputService _inputService;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            _inputService = Code.Infrastructure.Game.InputService;

            _rb.drag = 0.5f;
            _rb.angularDrag = 0.5f;
        }

        private void FixedUpdate()
        {
            ApplyRotation();
            ApplyThrust();
        }

        private void ApplyRotation()
        {
            var inputAxis = _inputService.Axis;

            float yaw = inputAxis.X;
            float pitch = -inputAxis.Y;

            var rotation = new Vector3(pitch, yaw, 0) * rotationSpeed;

            _rb.AddTorque(transform.up * (yaw * rotationSpeed), ForceMode.Force);
            _rb.AddTorque(transform.right * (pitch * rotationSpeed), ForceMode.Force);
        }

        private void ApplyThrust()
        {
            var accelerate = _inputService.IsAxelerationButtonUp();
            //bool decelerate = _inputService.IsDecelerationButtonUp();

            if (accelerate)
            {
                _rb.AddForce(transform.forward * movementSpeed, ForceMode.Impulse);
            }
            
            // if (decelerate)
            // {
            //     rb.AddForce(-transform.forward * movementSpeed, ForceMode.Impulse);
            // }
        }
    }
}
