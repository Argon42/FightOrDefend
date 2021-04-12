using System.Collections;
using UnityEngine;

namespace YodeGroup.Utility.Jobs
{
    class CoroutineJobQueue : JobQueue
    {
        public override void StartJobQueue()
        {
            StartCoroutine(StartJobs());
        }

        private IEnumerator StartJobs()
        {
            do
            {
                if (shuffleJobQueue)
                    Shuffle(jobQueue);

                foreach (var job in jobQueue)
                {
                    yield return new WaitForSeconds(job.Delay);
                    if (!Enabled)
                        yield break;
                    job.Invoke();
                }
            } while (isCycle && Enabled);
        }
    }
}