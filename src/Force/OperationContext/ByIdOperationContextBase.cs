using System;
using Force.Ddd;

namespace Force.OperationContext
{
    public abstract class ByIdOperationContextBase<TKey, TRequest> : OperationContextBase<TRequest>, IHasId<TKey>
        where TRequest : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        protected ByIdOperationContextBase(TRequest request) : base(request)
        {
        }

        object IHasId.Id => Id;

        public TKey Id => Request.Id;
    }
}