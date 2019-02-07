using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IWrestlingDAL
    {
        int? AddShow(Show s);
        int? AddShowInstance(ShowInstance s);
        int? AddWrestler(Wrestler w);
        IQueryable<Company> FindCompanies(Expression<Func<Company, bool>> predicate, params Expression<Func<Company, object>>[] includes);
        IQueryable<Contract> FindContract(Expression<Func<Contract, bool>> predicate, params Expression<Func<Contract, object>>[] includes);
        IQueryable<Show> FindShow(Expression<Func<Show, bool>> predicate, params Expression<Func<Show, object>>[] includes);
        IQueryable<ShowInstance> FindShowInstance(Expression<Func<ShowInstance, bool>> predicate, params Expression<Func<ShowInstance, object>>[] includes);
        IQueryable<Wrestler> FindWrestlers(Expression<Func<Wrestler, bool>> predicate, params Expression<Func<Wrestler, object>>[] includes);
        IQueryable<Wrestler> FindWrestlersPaged(int pageIndex, int pageSize, Expression<Func<Wrestler, bool>> predicate, Expression<Func<Wrestler, object>> orderBy, params Expression<Func<Wrestler, object>>[] includes);
        bool UpdateShow(Show s);
        bool UpdateShowInstance(ShowInstance s);
        bool UpdateWrestler(Wrestler w);
    }
}