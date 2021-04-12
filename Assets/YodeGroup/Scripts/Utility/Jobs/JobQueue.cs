using System.Collections.Generic;
using UnityEngine;

namespace YodeGroup.Utility.Jobs
{
    public abstract class JobQueue : MonoBehaviour
    {
        [SerializeField] protected TimeToStart start;
        [SerializeField] protected bool isCycle;
        [SerializeField] protected bool stopJobsOnDisable;
        [SerializeField] protected bool shuffleJobQueue;
        [SerializeField] protected List<Job> jobQueue = new List<Job>();

        public abstract void StartJobQueue();

        protected bool Enabled => this && (!stopJobsOnDisable || gameObject.activeInHierarchy);

        protected static IList<T> Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            var random = new System.Random();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        [ContextMenu("Start job queue")]
        private void StartJobsEditor() => StartJobQueue();

        private void Awake()
        {
            if (start.HasFlag(TimeToStart.Awake))
                StartJobQueue();
        }

        private void Start()
        {
            if (start.HasFlag(TimeToStart.Start))
                StartJobQueue();
        }

        private void OnEnable()
        {
            if (start.HasFlag(TimeToStart.OnEnable))
                StartJobQueue();
        }

        private void OnDisable()
        {
            if (start.HasFlag(TimeToStart.OnDisable))
                StartJobQueue();
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            if (start.HasFlag(TimeToStart.OnApplicationFocus) && focusStatus ||
                start.HasFlag(TimeToStart.OnApplicationUnfocus) && !focusStatus)
                StartJobQueue();
        }
    }
}