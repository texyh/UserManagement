using MediatR;

namespace UserManagement.Application.Abstractions.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}