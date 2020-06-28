using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        public UnityEvent killedPlayer;

        public void KillPlayer()
        {
            killedPlayer.Invoke();
        }
    }
}