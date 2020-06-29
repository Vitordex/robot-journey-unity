using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Events
{
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> {}
}