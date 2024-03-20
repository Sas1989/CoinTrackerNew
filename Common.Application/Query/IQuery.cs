using Common.Domain;
using MediatR;

namespace Common.Application.Query;

public interface IQuery<TResponse>: IRequest<Result<TResponse>>
{
}
