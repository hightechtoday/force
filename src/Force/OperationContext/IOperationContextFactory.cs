﻿using System;

 namespace Force.OperationContext
{
    public interface IOperationContextFactory<T, TContext>
        where T : class
        where TContext : IOperationContext<T>
    {
        Func<T, TContext> BuildFunc(IServiceProvider sp);

        TContext Build(IServiceProvider sp, T request);
    }
}