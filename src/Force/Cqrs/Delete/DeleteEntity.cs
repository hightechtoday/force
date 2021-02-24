using System;
using Force.Ddd;

namespace Force.Cqrs.Delete
{
    public class DeleteEntity<TKey>: HasIdBase<TKey> 
        where TKey : IEquatable<TKey>
    {
    }
}