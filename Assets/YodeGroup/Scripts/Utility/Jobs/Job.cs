using UnityEngine;
using UnityEngine.Events;

namespace YodeGroup.Utility.Jobs
{
    [System.Serializable]
    public class Job
    {
        [SerializeField] private float delay;
        [SerializeField] private UnityEvent onJob;

        public float Delay => delay;

        public void Invoke() => onJob?.Invoke();
    }
}