using UnityEngine;

namespace Code.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float rotationAngleX = 20f;
        [SerializeField] private float distance = 10f;
        [SerializeField] private float offsetY = 5f;
        [SerializeField] private float smoothSpeed = 0.125f;

        private Transform _following;

        private void LateUpdate()
        {
            if (!_following)
            {
                return;
            }

            Vector3 desiredPosition = CalculateCameraPosition();

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
            transform.LookAt(FollowingPointPosition());
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 CalculateCameraPosition()
        {
            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);
            Vector3 followingPosition = FollowingPointPosition();

            return followingPosition + offset;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += offsetY;

            return followingPosition;
        }
    }
}