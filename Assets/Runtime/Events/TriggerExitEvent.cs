using UnityEngine;

namespace Runtime.Events
{
    [RequireComponent(typeof(Collider))]
    public class TriggerExitEvent : MonoBehaviour
    {
        public string tagToCompare;
        public ColliderEvent triggered;
        
        private void OnTriggerExit(Collider other)
        {
            if (!string.IsNullOrEmpty(tagToCompare) && !other.CompareTag(tagToCompare)) return;

            triggered.Invoke(other);
        }
    }
}