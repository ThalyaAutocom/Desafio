using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Desafio.API;

public class LoggerHelper
{
    public static void EnsureInitialized()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .WriteTo.Async(sink =>
            {
                sink.Console();
                sink.File(
                    new CompactJsonFormatter(),
                    path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "logs.json"),
                    retainedFileCountLimit: 5,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                );
            })
            .CreateLogger();

    }
}
