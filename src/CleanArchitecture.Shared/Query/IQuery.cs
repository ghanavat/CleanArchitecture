using MediatR;

namespace CleanArchitecture.Shared.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
