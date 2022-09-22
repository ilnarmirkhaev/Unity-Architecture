using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float RotationAngleX;
        public float Distance;
        public float OffsetY;

        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            if (_target == null) return;

            var rotation = Quaternion.Euler(RotationAngleX, 0, 0);

            var position = rotation * new Vector3(0, 0, -Distance) + GetTargetPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject target) => _target = target.transform;

        private Vector3 GetTargetPosition()
        {
            var targetPosition = _target.position;
            targetPosition.y += OffsetY;

            return targetPosition;
        }
    }
}