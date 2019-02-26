using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class WrestlingDAL : IWrestlingDAL
    {
        private readonly IWrestlingUnitOfWork _unitOfWork;
        private readonly IRepository<Company> _repositoryCompany;
        private readonly IRepository<CompanyDetails> _repositoryCompanyDetails;
        private readonly IRepository<Show> _repositoryShow;
        private readonly IRepository<ShowDetails> _repositoryShowDetails;
        private readonly IRepository<ShowInstance> _repositoryShowInstance;
        private readonly IRepository<ShowInstanceDetails> _repositoryShowInstanceDetails;
        private readonly IRepository<Contract> _repositoryContract;
        private readonly IRepository<Wrestler> _repositoryWrestler;
        private readonly IRepository<WrestlerDetails> _repositoryWrestlerDetails;

        public WrestlingDAL()
            : this(new WrestlingUnitOfWork())
        {
        }

        public WrestlingDAL(WrestlingUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryCompany = _unitOfWork.Company;
            _repositoryCompanyDetails = _unitOfWork.CompanyDetails;
            _repositoryContract = _unitOfWork.Contract;
            _repositoryShow = _unitOfWork.Show;
            _repositoryShowDetails = _unitOfWork.ShowDetails;
            _repositoryShowInstance = _unitOfWork.ShowInstance;
            _repositoryShowInstanceDetails = _unitOfWork.ShowInstanceDetails;
            _repositoryWrestler = _unitOfWork.Wrestler;
            _repositoryWrestlerDetails = _unitOfWork.WrestlerDetails;
        }

        public IQueryable<Wrestler> FindWrestlers(Expression<Func<Wrestler, bool>> predicate, params Expression<Func<Wrestler, object>>[] includes)
        {
            var model = _repositoryWrestler.Find(predicate, includes);
            return model;
        }

        public IQueryable<Wrestler> FindWrestlersPaged(int pageIndex, int pageSize, Expression<Func<Wrestler, bool>> predicate, Expression<Func<Wrestler, object>> orderBy, params Expression<Func<Wrestler, object>>[] includes)
        {
            var model = _repositoryWrestler.FindPaged(predicate, orderBy, pageIndex, pageSize, includes);
            return model;
        }

        public IQueryable<Company> FindCompanies(Expression<Func<Company, bool>> predicate, params Expression<Func<Company, object>>[] includes)
        {
            var model = _repositoryCompany.Find(predicate, includes);
            return model;
        }

        public IQueryable<Show> FindShow(Expression<Func<Show, bool>> predicate, params Expression<Func<Show, object>>[] includes)
        {
            var model = _repositoryShow.Find(predicate, includes);
            return model;
        }

        public IQueryable<ShowInstance> FindShowInstance(Expression<Func<ShowInstance, bool>> predicate, params Expression<Func<ShowInstance, object>>[] includes)
        {
            var model = _repositoryShowInstance.Find(predicate, includes);
            return model;
        }

        public IQueryable<Contract> FindContract(Expression<Func<Contract, bool>> predicate, params Expression<Func<Contract, object>>[] includes)
        {
            var model = _repositoryContract.Find(predicate, includes);
            return model;
        }

        public int? AddWrestler(Wrestler w)
        {
            w.CreationDate = DateTime.UtcNow;
            var id = _repositoryWrestler.AddWithIdentity(w);
            _unitOfWork.Commit();
            return id;
        }

        public int? AddShow(Show s)
        {
            s.CreationDate = DateTime.UtcNow;
            var id = _repositoryShow.AddWithIdentity(s);
            _unitOfWork.Commit();
            return id;
        }

        public int? AddShowInstance(ShowInstance s)
        {
            s.CreationDate = DateTime.UtcNow;
            var id = _repositoryShowInstance.AddWithIdentity(s);
            _unitOfWork.Commit();
            return id;
        }

        public bool UpdateWrestler(Wrestler w)
        {
            var wrestlerDb = _repositoryWrestler.Find(wres => wres.Id == w.Id, wres => wres.WrestlerDetails).FirstOrDefault();
            if (wrestlerDb == null)
                return false;

            //Remove Details
            var dbToDeleteDetails = wrestlerDb.WrestlerDetails.ToList();
            foreach (var dbToDeleteDetail in dbToDeleteDetails)
            {
                _repositoryWrestlerDetails.Remove(dbToDeleteDetail);
            }

            wrestlerDb.DebutDate = w.DebutDate;
            wrestlerDb.WrestlerDetails = w.WrestlerDetails;
            wrestlerDb.LastUpdateDate = DateTime.UtcNow;
            _unitOfWork.Commit();
            return true;
        }

        public bool UpdateShow(Show s)
        {
            var showDb = _repositoryShow.Find(show => show.Id == s.Id, wres => wres.ShowDetails).FirstOrDefault();
            if (showDb == null)
                return false;

            //Remove Details
            var dbToDeleteDetails = showDb.ShowDetails.ToList();
            foreach (var dbToDeleteDetail in dbToDeleteDetails)
            {
                _repositoryShowDetails.Remove(dbToDeleteDetail);
            }

            showDb.ShowDetails = s.ShowDetails;
            showDb.LastUpdateDate = DateTime.UtcNow;
            _unitOfWork.Commit();
            return true;
        }

        public bool UpdateShowInstance(ShowInstance s)
        {
            var showInstanceDb = _repositoryShowInstance.Find(si => si.Id == s.Id, si => si.ShowInstanceDetails).FirstOrDefault();
            if (showInstanceDb == null)
                return false;

            //Remove Details
            var dbToDeleteDetails = showInstanceDb.ShowInstanceDetails.ToList();
            foreach (var dbToDeleteDetail in dbToDeleteDetails)
            {
                _repositoryShowInstanceDetails.Remove(dbToDeleteDetail);
            }

            showInstanceDb.ShowDate = s.ShowDate;
            showInstanceDb.ShowInstanceDetails = s.ShowInstanceDetails;
            showInstanceDb.LastUpdateDate = DateTime.UtcNow;
            _unitOfWork.Commit();
            return true;
        }

        public bool AddWrestlerContracts(int wrestlerId, List<Contract> contracts)
        {
            var wrestler = _repositoryWrestler.Find(w => w.Id == wrestlerId, w => w.Contract).FirstOrDefault();
            if (wrestler == null)
                return false;
            foreach (var c in contracts)
            {
                c.CreationDate = DateTime.UtcNow;
                wrestler.Contract.Add(c);
            }
            _unitOfWork.Commit();
            return true;
        }

    }
}

