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

        private Vector3 _origin;
        private Transform _parentTransform;
        private readonly float _gridSize = 1.5f;

        private void Awake()
        {
            _parentTransform = transform.parent.parent;
            _origin = _parentTransform.position;
        }

        private void Update()
        {
            _parentTransform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
            
            var distance = Mathf.Abs(Vector3.Distance(_parentTransform.position, _origin));
            if (!(distance > _gridSize * gridMoveAmount)) return;
            
            moveDirection *= -1f;
            _origin = _parentTransform.position;
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
