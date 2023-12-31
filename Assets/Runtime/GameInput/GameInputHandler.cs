﻿using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameInput
{
    public class GameInputHandler : MonoBehaviour
    {
        private MainActions _actions;

        public UnityEvent turnPlatformsHandler;
        public Vector2Event walkHandler;
        public UnityEvent interactHandler;

        public static GameInputHandler instance;

        private void Awake()
        {
            instance = this;

            _actions = new MainActions();

            _actions.Game.TurnPlatforms.performed += (context) => turnPlatformsHandler.Invoke();
            _actions.Game.Interact.performed += (context) => interactHandler.Invoke();
        }

        private void OnEnable()
        {
            _actions.Game.Enable();
        }

        private void OnDisable()
        {
            _actions.Game.Disable();
        }

        private void Update()
        {
            ValidateWalk();
        }

        private Vector2 _lastWalkValue;

        private void ValidateWalk()
        {
            walkHandler.Invoke(_actions.Game.Walk.ReadValue<Vector2>());
        }
    }

    [System.Serializable]
    public class Vector2Event : UnityEvent<Vector2>
    {
    }
}