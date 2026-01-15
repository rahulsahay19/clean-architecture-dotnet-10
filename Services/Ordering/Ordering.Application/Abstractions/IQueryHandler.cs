namespace Ordering.Application.Abstractions
{
    public interface IQueryHandler<in TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
