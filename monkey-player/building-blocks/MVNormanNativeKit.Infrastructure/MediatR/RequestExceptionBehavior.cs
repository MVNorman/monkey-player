using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace MVNormanNativeKit.Infrastructure.MediatR
{
    public class RequestExceptionBehavior<TRequest, TResponse> : IRequestExceptionHandler<TRequest, TResponse>
    {
        public RequestExceptionBehavior()
        {

        }

        public Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            var a = state;

            return Task.FromResult("This request was handled correctly");
        }
    }
}
