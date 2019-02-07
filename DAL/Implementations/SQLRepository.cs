using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public class SQLRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected ObjectSet<T> ObjectSet;

        public SQLRepository(ObjectContext context)
        {
            ObjectSet = context.CreateObjectSet<T>();
        }

        private static IQueryable<T> ExpressionQuery(IEnumerable<Expression<Func<T, object>>> includes, IQueryable<T> query)
        {
            var result = (from child in includes
                          select (MemberExpression)child.Body into expression
                          select expression.ToString()
                              into relations
                          select relations.Replace(".First()", "")).Aggregate(query, (current, relations)
                        => current.Include(relations.Substring(relations.IndexOf(".", StringComparison.Ordinal) + 1)));

            return result;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (!includes.Any()) return ObjectSet.Where(predicate);
            var query = ExpressionQuery(includes, ObjectSet.Where(predicate));

            return query;
        }

        public IQueryable<T> FindPaged<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> orderBy, int? pageIndex, int? pageSize, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query;

            if (orderBy != null && pageIndex.HasValue && pageSize.HasValue)
                query = ExpressionQuery(includes, ObjectSet.Where(predicate).OrderBy(orderBy).Skip(pageIndex.Value).Take(pageSize.Value));
            else
                query = ExpressionQuery(includes, ObjectSet.Where(predicate));

            return query;
        }

        public int AddWithIdentity(T newEntity)
        {
            ObjectSet.AddObject(newEntity);
            ObjectSet.Context.SaveChanges();
            return newEntity.Id;
        }

        public void Remove(T entity)
        {
            ObjectSet.DeleteObject(entity);
        }

    }
}
