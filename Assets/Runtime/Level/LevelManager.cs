using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Level
{
    public class LevelManager : MonoBehaviour
    {
        public UnityEvent levelEnded;
        
        public void EndLevel()
        {
            levelEnded.Invoke();
        }
    }
}
