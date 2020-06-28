using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Platforms
{
    public class GameObjectRotator : MonoBehaviour, IRotator
    {
        public float speed = 2f;
        public float tolerance;
        
        private bool _isTurning;
        private Vector3 _targetRotation = new Vector3(0f, 0f, 0f);
        private Transform _cachedTransform;
        public float multiplyFactor;

        private void Awake()
        {
            _cachedTransform = GetComponent<Transform>();
        }

        public void TurnAround()
        {
            _isTurning = true;
            var is180 = Math.Abs(_targetRotation.x - 180f) < float.Epsilon;
            _targetRotation = new Vector3(is180 ? 360f : 180f, 0f, 0f);
        }

        private void Update()
        {
            if (!_isTurning) return;

            var targetQuaternion = Quaternion.Euler(_targetRotation);
            
            var cachedRotation = _cachedTransform.rotation;
            cachedRotation = Quaternion.Lerp(cachedRotation, targetQuaternion,
                speed * Time.deltaTime * multiplyFactor);
            _cachedTransform.rotation = cachedRotation;
            
            var dotRotation = Quaternion.Angle(cachedRotation, targetQuaternion);
            if (dotRotation < tolerance) _isTurning = false;
        }

        public bool IsTurning() => _isTurning;
    }
}