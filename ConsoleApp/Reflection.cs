
namespace ConsoleApp
{
    public static class Reflection
    {
        public static IEnumerable<string> GetSpecificFieldsWithValues<T>(T record, params string[] fields)
        {
            List<string> fieldMessages = new List<string>();

            var properties = record.GetType().GetProperties();

            foreach (var field in properties)
            {
                var specificField = fields.FirstOrDefault(x => x.ToLower() == field.Name.ToLower());
                if (specificField != null)
                {
                    fieldMessages.Add($"{field.Name} = {field.GetValue(record)}");
                }
            }

            return fieldMessages;
        }
    }
}
