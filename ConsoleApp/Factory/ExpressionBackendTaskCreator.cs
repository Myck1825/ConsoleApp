using ConsoleApp.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Factory
{
    public class ExpressionBackendTaskCreator : Creator
    {
        private readonly ILogger<ExpressionBackendTask> _logger;
        private readonly string[] specificFields = default!;

        public ExpressionBackendTaskCreator(ILogger<ExpressionBackendTask> logger, string[] specificFields)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.specificFields = specificFields;
        }

        public override IBackendTask FactoryMethod(bool isUseLogger)
        {
            if (!isUseLogger)
            {
                return new ExpressionBackendTask();
            }
            else
            {
                return new TasksWithLogger.ExpressionBackendTask(_logger, specificFields);
            }
        }
    }
}
