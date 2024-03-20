using Common.Domain;
using MediatR;

namespace Common.Application.Sender;

public class Dispatcher : IDispatcher
{
    private readonly ISender _sender;

    public Dispatcher(ISender sender)
    {
        _sender = sender;
    }

    public Task<Result> Send(IRequest<Result> request)
    {
        return _sender.Send(request);
    }

    public Task<Result<TValue>> Send<TValue>(IRequest<Result<TValue>> request)
    {
        return _sender.Send(request);
    }
}
