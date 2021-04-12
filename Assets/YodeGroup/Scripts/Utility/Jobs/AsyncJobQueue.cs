using System;
using System.Threading.Tasks;

namespace YodeGroup.Utility.Jobs
{
    class AsyncJobQueue : JobQueue
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
                    await Task.Delay(TimeSpan.FromSeconds(job.Delay));
                    if (!Enabled)
                        return;
                    job.Invoke();
                }
            } while (isCycle && Enabled);
        }
    }
}