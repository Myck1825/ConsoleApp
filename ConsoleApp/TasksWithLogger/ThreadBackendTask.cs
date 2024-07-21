using Microsoft.Extensions.Logging;
namespace ConsoleApp.TasksWithLogger
{
    public class ThreadBackendTask : Tasks.ThreadBackendTask
    {
        private readonly ILogger<Tasks.ThreadBackendTask> _logger;
        private readonly int _limitThread;

        public ThreadBackendTask(ILogger<Tasks.ThreadBackendTask> logger, int limitThread) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<IEnumerable<ThreadTaskItemResult>> ExecuteAsync(
            IEnumerable<ThreadTaskItemConfig> configs)
        {
            if (_limitThread != 0)
            {
                return configs.AsParallel().WithDegreeOfParallelism(5).Select(x => (ExecuteAsync(x).GetAwaiter().GetResult()));
            }
            else
            {
                return await base.ExecuteAsync(configs);
            }
        }

        protected override void WriteResult(ThreadTaskItemResult result)
        {
            _logger.LogInformation($"{nameof(Tasks.ThreadBackendTask)}:{result.Message}");
        }
    }
}
