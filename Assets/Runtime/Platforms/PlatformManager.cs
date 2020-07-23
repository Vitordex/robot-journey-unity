using Runtime.GameInput;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        public UnityEvent turned;
        public BoolEvent changeEnergizedEvent;

        private IRotator _platformRotator;
        [SerializeField] private bool isEnergized = false;
        public bool canTurn;

        [HideInInspector] public string platformType;
        public bool IsEnergized => isEnergized;

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
            changeEnergizedEvent.Invoke(isEnergized);
        }
    }
}
