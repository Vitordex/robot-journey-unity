using Runtime.Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        public UnityEvent killedPlayer;
        private Transform _cachedTransform;
        private IInteractable _terminal;

        private void Awake()
        {
            _cachedTransform = GetComponent<Transform>();
        }

        public void KillPlayer()
        {
            killedPlayer.Invoke();
        }

        public void AttachToTransform(Transform parent)
        {
            _cachedTransform.SetParent(parent);
        }

        public void Interact()
        {
            _terminal?.Interact();
        }

        public void SetActiveTerminal(Collider terminal)
        {
            _terminal = terminal.GetComponent<IInteractable>();
        }
    }
}