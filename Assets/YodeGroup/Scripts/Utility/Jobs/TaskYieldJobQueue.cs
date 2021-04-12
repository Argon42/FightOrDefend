using System;
using System.Threading.Tasks;
using UnityEngine;

namespace YodeGroup.Utility.Jobs
{
    class TaskYieldJobQueue : JobQueue
    {
        public override void StartJobQueue()
        {
            StartJobs();
        }

        private async Task StartJobs()
        {
            do
            {
                if (shuffleJobQueue)
                    Shuffle(jobQueue);

                foreach (var job in jobQueue)
                {
                    var time = 0f;
                    while (time < job.Delay)
                    {
                        await Task.Yield();
                        time += Time.deltaTime;

                        if (!Enabled)
                            return;
                    }

                    if (!Enabled)
                        return;
                    job.Invoke();
                }
            } while (isCycle && Enabled);
        }
    }
}