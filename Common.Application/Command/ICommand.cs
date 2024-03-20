using Common.Domain;
using MediatR;

namespace Common.Application.Command;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
