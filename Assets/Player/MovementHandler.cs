using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementHandler : MonoBehaviour
    {
        private Rigidbody _physicsBody;

        public float speed = 2f;

        private Vector3 _lastMovementInput;
        private Vector3 _currentVelocity;

        private void Awake()
        {
            _physicsBody = GetComponent<Rigidbody>();
        }

        public void Walk(Vector2 direction)
        {
            direction.Normalize();
            _lastMovementInput = new Vector3(direction.x, 0f, direction.y);

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
            _currentVelocity = _lastMovementInput * (speed * Time.deltaTime);
        }
    }
}