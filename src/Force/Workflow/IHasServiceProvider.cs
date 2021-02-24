using System;

namespace Force.Workflow
{
    public interface IHasServiceProvider
    { 
        IServiceProvider ServiceProvider { get; set; }
    }
}