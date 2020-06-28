using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Events
{
    [RequireComponent(typeof(Rigidbody))]
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
    
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> {}
}