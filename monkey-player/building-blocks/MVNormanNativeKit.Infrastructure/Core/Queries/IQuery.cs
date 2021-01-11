using MediatR;

namespace MVNormanNativeKit.Infrastructure.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    { }
}
