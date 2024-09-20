using UnityEngine;

namespace Code.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float distance = 20f;     // Відстань від гравця
        [SerializeField] private float offsetY = 10f;       // Відстань по вертикалі
        [SerializeField] private float smoothSpeed = 0.4f; // Швидкість плавності
        [SerializeField] private float tiltAngleX = 25f;   // Нахил по осі X

        private Transform _following;

        private void LateUpdate()
        {
            if (!_following)
            {
                return;
            }

            Vector3 desiredPosition = CalculateCameraPosition();

            // Плавне переміщення камери до бажаної позиції
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;

            // Встановлюємо ротацію камери з урахуванням повороту гравця і нахилу по осі X
            Quaternion playerRotation = _following.rotation;
            Quaternion tiltRotation = Quaternion.Euler(tiltAngleX, 0, 0); // Нахил по осі X

            // Поєднуємо поворот гравця з нахилом камери по осі X
            transform.rotation = playerRotation * tiltRotation;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 CalculateCameraPosition()
        {
            // Відстань від гравця з урахуванням його ротації
            Vector3 offset = _following.rotation * new Vector3(0, offsetY, -distance);

            return _following.position + offset;
        }
    }
}