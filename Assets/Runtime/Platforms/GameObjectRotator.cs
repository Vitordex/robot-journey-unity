using System;
using UnityEngine;

namespace Runtime.Platforms
{
    public class GameObjectRotator : MonoBehaviour, IRotator
    {
        public Transform rotateTransform;
        public float speed = 2f;
        public float tolerance;
        
        private bool _isTurning;
        private Vector3 _targetRotation = new Vector3(0f, 0f, 0f);
        public float multiplyFactor;

        public void TurnAround()
        {
            _isTurning = true;
            var is180 = Math.Abs(_targetRotation.z - 180f) < float.Epsilon;
            _targetRotation = new Vector3(0f, 0f, is180 ? 360f : 180f);
        }

        private void Update()
        {
            if (!_isTurning) return;

            var targetQuaternion = Quaternion.Euler(_targetRotation);
            
            var cachedRotation = rotateTransform.rotation;
            cachedRotation = Quaternion.Lerp(cachedRotation, targetQuaternion,
                speed * Time.deltaTime * multiplyFactor);
            rotateTransform.rotation = cachedRotation;
            
            var dotRotation = Quaternion.Angle(cachedRotation, targetQuaternion);
            if (dotRotation < tolerance) _isTurning = false;
        }

        public bool IsTurning() => _isTurning;
    }
}