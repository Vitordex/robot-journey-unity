using UnityEngine;

namespace Runtime.Platforms
{
    public class PlatformMaterial : MonoBehaviour
    {
        private Material[] materials;

        void Awake()
        {
            var platformManager = GetComponent<PlatformManager>();
            var renderes = GetComponentsInChildren<Renderer>(true);
            materials = new Material[renderes.Length];

            for (int i = 0; i < renderes.Length; i++)
                materials[i] = renderes[i].material;

            platformManager.onChangeEnergized
                .AddListener(()=> Energized(platformManager.IsEnergized));

            Energized(platformManager.IsEnergized);
        }

        public void Energized(bool isEnergized = true)
        {
            foreach (var material in materials)
                material.SetInt("_IsOn", isEnergized ? 1 : 0);
        }
    }
}