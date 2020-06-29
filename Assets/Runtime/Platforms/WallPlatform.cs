using UnityEngine;

namespace Runtime.Platforms
{
    public class WallPlatform : MonoBehaviour
    {
        public float tolerance = 0.02f;
        public float turnUpDelay = 0.2f;
        public GameObject playerBlocker;

        private Transform _cachedTransform;
        public Animator controller;

        private static readonly int Opened = Animator.StringToHash("opened");
        private bool _turningUp;
        private float _timePassed;

        private void Awake()
        {
            _cachedTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            if (IsUp()) AnimateWall(true);
        }

        private void Update()
        {
            if (!_turningUp) return;

            _timePassed += Time.deltaTime;

            if (_timePassed >= turnUpDelay)
            {
                _turningUp = false;
                _timePassed = 0f;
                AnimateWall(true);
            }
        }

        public void OnTurned()
        {
            if (IsUp()) AnimateWall(false);
            else _turningUp = true;
        }

        private void AnimateWall(bool shouldOpen)
        {
            controller.SetBool(Opened, shouldOpen);
            playerBlocker.SetActive(shouldOpen);
        }

        private bool IsUp()
        {
            return Mathf.Abs(Vector3.Angle(_cachedTransform.up, Vector3.up)) < tolerance;
        }
    }
}