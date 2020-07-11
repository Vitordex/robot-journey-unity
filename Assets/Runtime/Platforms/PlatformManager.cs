using Runtime.GameInput;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        public UnityEvent turned;

        private IRotator _platformRotator;
        public bool isEnergized = false;
        public bool canTurn;

        [HideInInspector] public string platformType;

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
            if (_platformRotator.IsTurning() || !canTurn || !isEnergized) return;

            _platformRotator.TurnAround();
            turned.Invoke();
        }

        public void ToggleEnergy()
        {
            isEnergized = !isEnergized;
        }
    }
}
