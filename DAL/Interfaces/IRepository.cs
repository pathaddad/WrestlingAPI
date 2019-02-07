using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T>
       where T : class
    {
        IQueryable<T> FindPaged<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> orderBy, int? pageIndex, int? pageSize, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        int AddWithIdentity(T newEntity);
        void Remove(T entity);
    }
}
