using System;
using GameInput;
using Runtime.GameInput;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        public bool canTurn;

        public UnityEvent turned;

        private IRotator _platformRotator;
        private bool _isEnergized = false;

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
            if (_platformRotator.IsTurning() || !canTurn || !_isEnergized) return;

            _platformRotator.TurnAround();
            turned.Invoke();
        }

        public void ToggleEnergy()
        {
            _isEnergized = !_isEnergized;
        }
    }
}
