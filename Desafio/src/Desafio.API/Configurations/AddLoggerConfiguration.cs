using Serilog;

namespace Desafio.API;

public static class AddLoggerConfiguration
{
    public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.AddSerilog(Log.Logger);
        logging.Services.AddSingleton(Log.Logger);

        return logging;
    }
}

