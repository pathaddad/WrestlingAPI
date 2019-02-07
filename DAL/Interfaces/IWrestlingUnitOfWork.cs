using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IWrestlingUnitOfWork
    {
        IRepository<Company> Company { get; }
        IRepository<CompanyDetails> CompanyDetails { get; }
        IRepository<Show> Show { get; }
        IRepository<ShowDetails> ShowDetails { get; }
        IRepository<ShowInstance> ShowInstance { get; }
        IRepository<ShowInstanceDetails> ShowInstanceDetails { get; }
        IRepository<Contract> Contract { get; }
        IRepository<Wrestler> Wrestler { get; }
        IRepository<WrestlerDetails> WrestlerDetails { get; }
        void Commit();

    }
}
