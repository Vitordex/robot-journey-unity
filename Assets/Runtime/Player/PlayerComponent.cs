using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        public UnityEvent killedPlayer;
        private Transform _cachedTransform;

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
    }
}