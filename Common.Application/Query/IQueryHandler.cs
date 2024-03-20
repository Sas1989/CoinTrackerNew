using Common.Domain;
using MediatR;

namespace Common.Application.Query;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery,Result<TResponse>> where TQuery : IQuery<TResponse> { }