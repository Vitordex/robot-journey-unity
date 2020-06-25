using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementHandler : MonoBehaviour
    {
        private Rigidbody _physicsBody;
        private Transform _cacheTransform;

        public float speed = 2f;

        private Vector2 _lastMovementInput;
        private Vector3 _currentVelocity;
        private Vector3 _currentRotation;

        private void Awake()
        {
            _physicsBody = GetComponent<Rigidbody>();
            _cacheTransform = transform;
        }

        public void Walk(Vector2 direction)
        {
            _lastMovementInput = direction;

            UpdateVelocity();

            var rotateVector = new Vector3(0f, direction.x * Time.deltaTime * speed, 0f);
            _currentRotation = rotateVector;
        }

        private void Update()
        {
            _cacheTransform.Rotate(_currentRotation);

            if (_currentRotation.sqrMagnitude > 0f)
                UpdateVelocity();
        }

        private void FixedUpdate()
        {
            var differentVelocity = Math.Abs(_physicsBody.velocity.sqrMagnitude - _currentVelocity.sqrMagnitude) <=
                                    float.Epsilon;
            if (differentVelocity) return;

            _physicsBody.velocity = _currentVelocity;
        }

        private void UpdateVelocity()
        {
            var moveAmount = _lastMovementInput.y * speed * Time.deltaTime;
            _currentVelocity = _cacheTransform.forward * moveAmount;
        }
    }
}