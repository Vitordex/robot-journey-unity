using System;
using Runtime.Player;
using UnityEngine;

namespace Runtime.Platforms
{
    public class MovablePlatform : MonoBehaviour
    {
        public Vector3 moveDirection = Vector3.forward;
        public float moveSpeed = 5;
        public float gridMoveAmount = 2;
        public float waitTime = 1f;

        private Vector3 _origin;
        private Transform _parentTransform;
        private readonly float _gridSize = 1.6f;

        private Vector3 _initialPosition;
        private Vector3 _endPosition;
        private bool _toDestination = true;

        private void Awake()
        {
            _parentTransform = transform.parent.parent;
            _origin = _parentTransform.position;
            _initialPosition = _parentTransform.position;
            _endPosition = _initialPosition + moveDirection * _gridSize * gridMoveAmount;
        }

        private bool _isInDelay = false;
        private float _timePassed = 0f;
        
        private void Update()
        {
            if (_isInDelay)
            {
                _timePassed += Time.deltaTime;

                if (_timePassed >= waitTime)
                {
                    _timePassed = 0f;
                    _isInDelay = false;
                }
                
                return;
            }
            
            _parentTransform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
            
            var distance = Mathf.Abs(Vector3.Distance(_parentTransform.position, _origin));
            if (!(distance > _gridSize * gridMoveAmount)) return;
            
            moveDirection *= -1f;
            _toDestination = !_toDestination;
            _parentTransform.position = !_toDestination ? _endPosition : _initialPosition;
            _origin = _parentTransform.position;
            _isInDelay = true;
        }

        public void AttachPlayer(Collider player)
        {
            player.GetComponent<PlayerComponent>().AttachToTransform(_parentTransform);
        }

        public void DetachPlayer(Collider player)
        {
            player.GetComponent<PlayerComponent>().AttachToTransform(null);
        }
    }
}
