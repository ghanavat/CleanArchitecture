using MediatR;

namespace CleanArchitecture.Shared.Command;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
