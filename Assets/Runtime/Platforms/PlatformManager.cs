using System;
using GameInput;
using UnityEngine;

namespace Runtime.Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        public bool canTurn;

        private IRotator _platformRotator;

        private void Awake()
        {
            _platformRotator = GetComponent<IRotator>();
        }

        private void Start()
        {
            GameInputHandler.instance.turnPlatformsHandler.AddListener(Turn);
        }

        private void Turn()
        {
            if (_platformRotator.IsTurning() || !canTurn) return;

            _platformRotator.TurnAround();
        }
    }
}
