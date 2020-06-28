using Runtime.Player;
using UnityEngine;

namespace Runtime.Platforms
{
    public class ShockPlatform : MonoBehaviour
    {
        public void ShockPlayer(Collider playerCollider)
        {
            playerCollider.GetComponent<PlayerComponent>().KillPlayer();
        }
    }
}
