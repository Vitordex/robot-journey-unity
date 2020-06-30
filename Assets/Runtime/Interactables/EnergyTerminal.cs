using Runtime.Platforms;
using UnityEngine;

namespace Runtime.Interactables
{
    public class EnergyTerminal : MonoBehaviour, IInteractable
    {
        [Range(0f, 5f)]
        public float heightOffset = 0.72f;
        
        public PlatformManager[] connectedPlatforms;

        [ContextMenu("Interact")]
        public void Interact()
        {
            foreach (var platform in connectedPlatforms)
            {
                platform.ToggleEnergy();
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            var cubeSize = new Vector3(1.6f, 0.7f, 1.6f);
            foreach (var platform in connectedPlatforms)
            {
                var position = platform.transform.position;
                Gizmos.DrawIcon(position + heightOffset * Vector3.up, "arrow.png");
                Gizmos.DrawWireCube(position, cubeSize);
            }
        }
    }
}