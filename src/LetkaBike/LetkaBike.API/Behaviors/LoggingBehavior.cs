using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using LetkaBike.Core.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LetkaBike.API.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();

            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            _logger.LogInformation($"Executing {requestNameWithGuid}");
            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                response = await next();

                if (response is ResponseBase res && res.HasError)
                {
                    _logger.LogInformation($"Error {requestNameWithGuid}; Message: {res.ErrorMessage}");
                }
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation(
                    $"Executed {requestNameWithGuid}; Duration={stopwatch.ElapsedMilliseconds}ms");
            }

            return response;
        }
    }
}