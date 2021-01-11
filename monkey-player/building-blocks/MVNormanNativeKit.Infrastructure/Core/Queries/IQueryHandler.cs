using MediatR;

namespace MVNormanNativeKit.Infrastructure.Core.Queries
{
    public interface IQueryHandler<in TRequest, TResponse> : 
        IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    { }
}
