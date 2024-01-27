using MediatR;

namespace Notes.Domain.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}