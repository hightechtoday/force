using System;
using System.ComponentModel.DataAnnotations;

namespace Force.Ddd.Domain
{
    public abstract class IntHasIdBase: HasIdBase<int>
    {}
    
    public abstract class HasIdBase<TKey>
        where TKey: IEquatable<TKey>
    {
        [Key, Required]
        public virtual TKey Id { get; set; }
    }
}