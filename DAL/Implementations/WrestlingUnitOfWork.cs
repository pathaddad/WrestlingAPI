using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public class WrestlingUnitOfWork : IWrestlingUnitOfWork
    {
        readonly ObjectContext _context;
        private const string ConnectionStringName = "WrestlingEntities";

        private SQLRepository<Company> _company { get; set; }
        private SQLRepository<CompanyDetails> _companyDetails { get; set; }
        private SQLRepository<Show> _show { get; set; }
        private SQLRepository<ShowDetails> _showDetails { get; set; }
        private SQLRepository<ShowInstance> _showInstance { get; set; }
        private SQLRepository<ShowInstanceDetails> _showInstanceDetails { get; set; }
        private SQLRepository<Contract> _contract { get; set; }
        private SQLRepository<Wrestler> _wrestler { get; set; }
        private SQLRepository<WrestlerDetails> _wrestlerDetails { get; set; }


        public IRepository<Company> Company => _company ?? (_company = new SQLRepository<Company>(_context));

        public IRepository<CompanyDetails> CompanyDetails => _companyDetails ?? (_companyDetails = new SQLRepository<CompanyDetails>(_context));

        public IRepository<Show> Show => _show ?? (_show = new SQLRepository<Show>(_context));

        public IRepository<ShowDetails> ShowDetails => _showDetails ?? (_showDetails = new SQLRepository<ShowDetails>(_context));

        public IRepository<ShowInstance> ShowInstance => _showInstance ?? (_showInstance = new SQLRepository<ShowInstance>(_context));

        public IRepository<ShowInstanceDetails> ShowInstanceDetails => _showInstanceDetails ?? (_showInstanceDetails = new SQLRepository<ShowInstanceDetails>(_context));

        public IRepository<Contract> Contract => _contract ?? (_contract = new SQLRepository<Contract>(_context));

        public IRepository<Wrestler> Wrestler => _wrestler ?? (_wrestler = new SQLRepository<Wrestler>(_context));

        public IRepository<WrestlerDetails> WrestlerDetails => _wrestlerDetails ?? (_wrestlerDetails = new SQLRepository<WrestlerDetails>(_context));

        public WrestlingUnitOfWork()
        {
            var connectionString =
                ConfigurationManager
                    .ConnectionStrings[ConnectionStringName]
                    .ConnectionString;

            _context = new ObjectContext(connectionString);
            _context.ContextOptions.LazyLoadingEnabled = false;
            _context.ContextOptions.ProxyCreationEnabled = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
