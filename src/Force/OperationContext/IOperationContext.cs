namespace Force.OperationContext
{
    // assembly-scanning optimization
    public interface IOperationContext<out T>
    {
        T Request { get; }
    }
}