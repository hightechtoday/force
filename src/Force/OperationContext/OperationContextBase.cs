namespace Force.OperationContext
{
    public abstract class OperationContextBase<T>: IOperationContext<T>
        where T: class
    {
        public T Request { get; }

        protected OperationContextBase(T request)
        {
            Request = request;
        }
        
    }
}