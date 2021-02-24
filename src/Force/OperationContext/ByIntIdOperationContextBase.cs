using Force.Ddd;

namespace Force.OperationContext
{
    public abstract class ByIntIdOperationContextBase<T> : ByIdOperationContextBase<int, T> where T : class, IHasId<int>
    {
        protected ByIntIdOperationContextBase(T request) : base(request)
        {
        }
    }
}