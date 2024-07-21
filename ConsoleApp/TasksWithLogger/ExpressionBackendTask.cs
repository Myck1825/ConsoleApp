using Microsoft.Extensions.Logging;

namespace ConsoleApp.TasksWithLogger
{
    public class ExpressionBackendTask : Tasks.ExpressionBackendTask
    {
        private readonly ILogger<Tasks.ExpressionBackendTask> _logger;
        private readonly string[] specificFields;
        public ExpressionBackendTask(ILogger<Tasks.ExpressionBackendTask> logger, string[] specificFields)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.specificFields = specificFields ?? throw new ArgumentNullException(nameof(specificFields));
        }

        protected override void WriteRecord<T>(T record, params string[] fields)
        {
            _logger.LogInformation($"{nameof(ExpressionBackendTask)} : Record {record.Id}:");

            string[] newFields = new string[specificFields.Length];

            if (specificFields.Length != 0)
            {
                newFields = specificFields;
            }
            else
            {
                newFields = fields;
            }

            var fieldMessages = Reflection.GetSpecificFieldsWithValues<T>(record, newFields);

            if (fieldMessages.Count() == 0 || fieldMessages.Any(x=>x == null))
            {
                fieldMessages = fields.Select(field => $"{field} = record[field]");
            }

            _logger.LogInformation(string.Join("; ", fieldMessages));
        }
    }
}
