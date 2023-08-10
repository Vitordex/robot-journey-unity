using UnityEngine;

namespace Runtime.Events
{
    [RequireComponent(typeof(Collider))]
    public class TriggerEnterEvent : MonoBehaviour
    {
        public string tagToCompare;
        public ColliderEvent triggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!string.IsNullOrEmpty(tagToCompare) && !other.CompareTag(tagToCompare)) return;

            triggered.Invoke(other);
        }
    }
}