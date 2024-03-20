using Common.Domain;
using MediatR;

namespace Common.Application.Sender
{
    public interface IDispatcher
    {
        Task<Result> Send(IRequest<Result> request);
        Task<Result<TValue>> Send<TValue>(IRequest<Result<TValue>> request);
    }
}