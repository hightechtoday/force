using Force.Ccc;
using Force.Workflow;

namespace Force.Cqrs
{
    public class HandlerResult<T>: Result<T, FailureInfo>
    {
        public HandlerResult(T success) : base(success)
        {
        }

        public HandlerResult(FailureInfo failure) : base(failure)
        {
        }
        
        public static implicit operator HandlerResult<T>(FailureInfo failure) => 
            new HandlerResult<T>(failure);

        public static implicit operator HandlerResult<T>(T success) => 
            new HandlerResult<T>(success);
    }
}