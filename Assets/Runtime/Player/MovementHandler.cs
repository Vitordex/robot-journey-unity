using System;
using UnityEngine;

namespace Runtime.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementHandler : MonoBehaviour
    {
        public float speed = 2f;
        public Transform playerModelTransform;

        private Rigidbody _physicsBody;
        
        private void Awake()
        {
            _physicsBody = GetComponent<Rigidbody>();
        }

        private Vector3 _movementInput;
        private Quaternion _targetLookRotation;
        
        [Range(0f, 9f)]
        public float lookSpeed;

        public void Walk(Vector2 direction)
        {
            var newInput = new Vector3(direction.x, 0f, direction.y) * speed;
            newInput.Normalize();
            
            _movementInput = newInput;

            if (Vector3.Distance(newInput, Vector3.zero) <= 0.02f) return;
            
            var targetY = 0f;
            if (InputEquals(newInput, .7f, .7f))
                targetY = 45f;
            else if (InputEquals(newInput, -.7f, -.7f))
                targetY = 225f;
            else if (InputEquals(newInput, -.7f, .7f))
                targetY = 315f;
            else if (InputEquals(newInput, .7f, -.7f))
                targetY = 135f;
            else if (InputEquals(newInput, 1f, 0f))
                targetY = 90f;
            else if (InputEquals(newInput, -1f, 0f))
                targetY = 270f;
            else if (InputEquals(newInput, 0f, 1f))
                targetY = 0f;
            else if (InputEquals(newInput, 0f, -1f))
                targetY = 180f;
            
            _targetLookRotation = Quaternion.Euler(0f, targetY, 0f);
        }

        private static bool InputEquals(Vector3 newInput,
            float x,
            float z,
            float tolerance = 0.02f) =>
            Math.Abs(newInput.x - x) < tolerance && Math.Abs(newInput.z - z) < tolerance;

        private void Update()
        {
            var rotation = playerModelTransform.rotation;
            rotation = Quaternion.Lerp(rotation, _targetLookRotation, Time.deltaTime * lookSpeed);
            playerModelTransform.rotation = rotation;
        }

        private void FixedUpdate()
        {
            _physicsBody.velocity = new Vector3(_movementInput.x, _physicsBody.velocity.y, _movementInput.z);
        }
    }
}