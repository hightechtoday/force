using System;

namespace Force.Ddd
{
    public abstract class EntityBase<T>: 
        IHasId<T>
        where T: IEquatable<T>
    {
        object IHasId.Id => Id;

        public T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as EntityBase<T>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id?.Equals(default) != false || other.Id?.Equals(default) != false)
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}