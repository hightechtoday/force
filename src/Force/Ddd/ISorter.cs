using System.Linq;

namespace Force.Ddd
{
    public interface ISorter<TQueryable, in TPredicate>
    {
        IOrderedQueryable<TQueryable> Sort(IQueryable<TQueryable> queryable, TPredicate predicate);
    }
}