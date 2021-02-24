using Force.Ddd;

namespace Force.OperationContext
{
    public class QueryByStringIdOperationContextBase<TQuery, TRequest> : QueryByIdOperationContextBase<string, TQuery, TRequest>
        where TRequest : class, IHasId<string>
    {
        public QueryByStringIdOperationContextBase(TRequest request) : base(request)
        {
        }
    }
}