using Force.Ccc;

namespace Force.Workflow
{
    internal interface IHasUnitOfWork
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}