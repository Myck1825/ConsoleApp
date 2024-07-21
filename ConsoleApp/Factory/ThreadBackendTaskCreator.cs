using ConsoleApp.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Factory
{
    public class ThreadBackendTaskCreator : Creator
    {
        private readonly ILogger<ThreadBackendTask> _logger;
        private readonly int _limitThread;

        public ThreadBackendTaskCreator(ILogger<ThreadBackendTask> logger, int limitThread)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _limitThread = limitThread;
        }

        public override IBackendTask FactoryMethod(bool isUseLogger)
        {
            if (!isUseLogger)
            {
                return new ThreadBackendTask();
            }
            else
            {
                return new TasksWithLogger.ThreadBackendTask(_logger, _limitThread);
            }
        }
    }
}
