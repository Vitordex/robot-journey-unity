using System;
using UnityEngine;

namespace Runtime.Platforms
{
    public class MovablePlatform : MonoBehaviour
    {
        private Rigidbody _parentRigidbody;

        public Vector3 moveDirection = Vector3.forward;
        public float moveSpeed = 5;
        public float gridMoveAmount = 2;

        private Vector3 _origin;
        private Transform _cachedTransform;
        private readonly float _gridSize = 1.5f;

        private void Awake()
        {
            _parentRigidbody = GetComponentInParent<Rigidbody>();
            _cachedTransform = transform;
            _origin = _cachedTransform.position;
        }

        private void FixedUpdate()
        {
            _parentRigidbody.velocity = moveDirection * (moveSpeed * Time.deltaTime);

            var distance = Mathf.Abs(Vector3.Distance(_cachedTransform.position, _origin));
            if (!(distance > _gridSize * gridMoveAmount)) return;
            
            moveDirection *= -1f;
            _origin = _cachedTransform.position;
        }
    }
}
