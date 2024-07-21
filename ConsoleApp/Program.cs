using ConsoleApp.Factory;
using ConsoleApp.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

public class Program
{
    private static ILogger<ThreadBackendTask> _threadBackendLogger;
    private static ILogger<ExpressionBackendTask> _expressionBackendTaskLogger;

    private static async Task Main(string[] args)
    {
        Configuration();

        var task1 = new ThreadBackendTaskCreator(_threadBackendLogger, 5); // 5 count of thread
        IBackendTask task2 = null;

        int exit = 0;
        do
        {
            int number = HandleAnswer("Do you want use program with Logger? 1 - yes, 0 - no");
            bool isUseLogger = number == 1;

            await task1.FactoryMethod(isUseLogger).RunAsync();

            Console.WriteLine("Set specific field names with commas:");
            string[] specificFields = Console.ReadLine().Split(',').Select(x=>x.Trim(' ')).ToArray();
            
            task2 = new ExpressionBackendTaskCreator(_expressionBackendTaskLogger, specificFields).FactoryMethod(isUseLogger);
            await task2.RunAsync();

            exit = HandleAnswer("Do you want exit? 1 - yes, 0 - no");

        } while (exit != 1);
    }

    static int HandleAnswer(string question)
    {
        Console.WriteLine(question);
        var answer = Console.ReadLine();
        int number = 0;
        int.TryParse(answer, out number);

        return number;
    }

    static void Configuration()
    {
        var services = new ServiceCollection();
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/ConsoleAppLogs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddLogging(
            builder => builder.ClearProviders()
                              .AddFilter("Microsoft", LogLevel.Warning)
                              .AddFilter("System", LogLevel.Warning)
                              .AddConsole()
                              .AddDebug()
                              .AddSerilog(dispose: true));

        var provider = services.BuildServiceProvider();
        _threadBackendLogger = provider.GetRequiredService<ILogger<ThreadBackendTask>>();
        _expressionBackendTaskLogger = provider.GetRequiredService<ILogger<ExpressionBackendTask>>();
    }
}