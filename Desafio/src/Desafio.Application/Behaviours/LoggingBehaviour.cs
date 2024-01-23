using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Desafio.Application;
public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        var requestName = typeof(TRequest);
        var responseName = typeof(TResponse);

        var watch = Stopwatch.StartNew();

        logger.LogDebug("Handling Request [{@requestName}] with payload: {@request}",
            requestName, request);

        try
        {
            var response = await next();

            logger.LogDebug("Handled Request [{@requestName}] with Response [{@responseName}]",
                requestName, responseName);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on {@requestName}", requestName);

            throw;
        }
        finally
        {
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            logger.LogInformation("Request [{@requestName}] elapsed {@elapsedMs}ms",
                requestName, elapsedMs);
        }
    }
}

