﻿using System.Threading.Tasks;

 namespace Force.OperationContext
{
    public interface IAsyncOperationContextFactory<in TRequest, TContext>
        where TContext: OperationContextBase<TRequest> 
        where TRequest : class
    {
        Task<TContext> BuildAsync(TRequest request);
    }
}