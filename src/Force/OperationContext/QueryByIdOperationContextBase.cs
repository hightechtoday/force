using System;
using Force.Cqrs;
using Force.Ddd;

namespace Force.OperationContext
{
    public abstract class QueryByIdOperationContextBase<TKey, TQuery, TRequest> : 
        ByIdOperationContextBase<TKey, TRequest>, IQuery<TQuery>
        where TRequest : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        protected QueryByIdOperationContextBase(TRequest request) : base(request)
        {
        }
    }
}