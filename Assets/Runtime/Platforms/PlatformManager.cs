using Runtime.GameInput;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        public UnityEvent turned;
        public UnityEvent onChangeEnergized;

        private IRotator _platformRotator;
        [SerializeField] private bool isEnergized = false;
        public bool canTurn;

        [HideInInspector] public string platformType;

        public bool IsEnergized 
        { 
            get => isEnergized; 
            set 
            {
                isEnergized = value;
                onChangeEnergized.Invoke();
            } 
        }

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
