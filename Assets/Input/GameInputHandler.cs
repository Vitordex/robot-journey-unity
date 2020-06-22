using UnityEngine;
using UnityEngine.Events;

namespace RobotJourney.Input
{
    public class GameInputHandler : MonoBehaviour
    {
        private MainActions _actions;

        public UnityEvent turnPlatformsHandler;

        private void Awake()
        {
            _actions = new MainActions();

            _actions.Game.TurnPlatforms.performed += (context) => turnPlatformsHandler.Invoke();
        }

        private void OnEnable()
        {
            _actions.Game.Enable();
        }

        private void OnDisable()
        {
            _actions.Game.Disable();
        }
    }
}