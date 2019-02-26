using BusinessLogic.Entities;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class WrestlingService
    {
        private static Logger log = new Logger();

        public Wrestler GetWrestlerById(int wrestlerId, CommonPack cp, out string sfmTag)
        {
            var startDate = DateTime.Now;
            sfmTag = Constants.SfmConstants.ERROR.ToString();

            Wrestler wrestler = null;
            try
            {
                var wrestlerDb = ServiceLocator.Instance.WrestlingDAL.FindWrestlers(w => w.Id == wrestlerId, w => w.WrestlerDetails, w => w.Contract).FirstOrDefault();
                if (wrestlerDb != null)
                {
                    var companies = wrestlerDb.Contract.Select(c => c.Company.Id);
                    var companiesDb = ServiceLocator.Instance.WrestlingDAL.FindCompanies(c => companies.Contains(c.Id), c => c.CompanyDetails).ToList();
                    var companyNames = companiesDb.Select(c => c.CompanyDetails.FirstOrDefault(cd => cd.LanguageCode == cp.LanguageCode)?.Name).ToList();
                    companyNames.RemoveAll(c => string.IsNullOrWhiteSpace(c));
                    var details = wrestlerDb.WrestlerDetails.FirstOrDefault(wd => wd.LanguageCode == cp.LanguageCode);
                    wrestler = new Wrestler
                    {
                        BilledFrom = details?.BilledFrom,
                        Companies = companyNames,
                        DebutDate = wrestlerDb.DebutDate,
                        HomeTown = details?.HomeTown,
                        Id = wrestlerDb.Id,
                        IsExclusive = wrestlerDb.Contract.Any(c => c.IsExclusive),
                        RealName = details?.RealName,
                        WrestlingName = details?.WrestlingName
                    };
                    sfmTag = Constants.SfmConstants.SUCCESS.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken, sfmTag, startDate, DateTime.Now);
            return wrestler;
        }

        public List<Wrestler> GetAllWrestlers(int pageIndex, int pageSize, CommonPack cp, out string sfmTag)
        {
            var startDate = DateTime.Now;
            log.Info("Started", cp.RequestToken);
            sfmTag = Constants.SfmConstants.ERROR.ToString();

            var wrestlers = new List<Wrestler>();
            try
            {
                var wrestlersDb = ServiceLocator.Instance.WrestlingDAL.FindWrestlersPaged(pageIndex, pageSize, w => true, w => w.Id, w => w.Contract).ToList();
                foreach (var wrestlerDb in wrestlersDb)
                {
                    var companies = wrestlerDb.Contract.Select(c => c.Company.Id);
                    var companiesDb = ServiceLocator.Instance.WrestlingDAL.FindCompanies(c => companies.Contains(c.Id), c => c.CompanyDetails).ToList();
                    var companyNames = companiesDb.Select(c => c.CompanyDetails.FirstOrDefault(cd => cd.LanguageCode == cp.LanguageCode)?.Name).ToList();
                    companyNames.RemoveAll(c => string.IsNullOrWhiteSpace(c));
                    var details = wrestlerDb.WrestlerDetails.FirstOrDefault(wd => wd.LanguageCode == cp.LanguageCode);
                    wrestlers.Add(new Wrestler
                    {
                        BilledFrom = details?.BilledFrom,
                        Companies = companyNames,
                        DebutDate = wrestlerDb.DebutDate,
                        HomeTown = details?.HomeTown,
                        Id = wrestlerDb.Id,
                        IsExclusive = wrestlerDb.Contract.Any(c => c.IsExclusive),
                        RealName = details?.RealName,
                        WrestlingName = details?.WrestlingName
                    });
                }
                sfmTag = wrestlersDb.Count == pageSize ? Constants.SfmConstants.SUCCESS.ToString() : Constants.SfmConstants.SUCCESS_WITH_WARNING.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken, sfmTag, startDate, DateTime.Now);
            return wrestlers;
        }

        public int? CreateWrestler(CreateWrestlerRequestItem wrestler, CommonPack cp, out string sfmTag)
        {
            var startDate = DateTime.Now;
            log.Info("Started", cp.RequestToken);
            sfmTag = Constants.SfmConstants.ERROR.ToString();

            int? result = null;
            try
            {
                var dbDetails = new List<DAL.WrestlerDetails>();
                wrestler.WrestlerDetails?.ForEach(d => dbDetails.Add(new DAL.WrestlerDetails
                {
                    BilledFrom = d.BilledFrom,
                    HomeTown = d.HomeTown,
                    LanguageCode = d.LanguageCode,
                    RealName = d.RealName,
                    WrestlingName = d.WrestlingName
                }));

                var dbWrestler = new DAL.Wrestler
                {
                    DebutDate = wrestler.DebutDate,
                    WrestlerDetails = dbDetails
                };

                result = ServiceLocator.Instance.WrestlingDAL.AddWrestler(dbWrestler);
                sfmTag = result.HasValue ? Constants.SfmConstants.SUCCESS.ToString() : Constants.SfmConstants.ERROR.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken, sfmTag, startDate, DateTime.Now);
            return result;
        }

        public bool AddWrestlerDetails(int wrestlerId, List<WrestlerDetails> wrestlerDetails, CommonPack cp, out string sfmTag)
        {
            var startDate = DateTime.Now;

            log.Info("Started", cp.RequestToken);
            sfmTag = Constants.SfmConstants.ERROR.ToString();

            var result = false;
            try
            {
                var wrestlerDb = ServiceLocator.Instance.WrestlingDAL.FindWrestlers(w => w.Id == wrestlerId, w => w.WrestlerDetails).FirstOrDefault();
                if (wrestlerDb != null)
                {
                    foreach (var d in wrestlerDetails)
                    {
                        wrestlerDb.WrestlerDetails.Add(new DAL.WrestlerDetails
                        {
                            BilledFrom = d.BilledFrom,
                            HomeTown = d.HomeTown,
                            LanguageCode = d.LanguageCode,
                            RealName = d.RealName,
                            WrestlingName = d.WrestlingName
                        });
                    }
                    result = ServiceLocator.Instance.WrestlingDAL.UpdateWrestler(wrestlerDb);
                    sfmTag = result ? Constants.SfmConstants.SUCCESS.ToString() : Constants.SfmConstants.ERROR.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken, sfmTag, startDate, DateTime.Now);
            return result;
        }

        public bool AddWrestlerContracts(int wrestlerId, List<WrestlerContract> contracts, CommonPack cp, out string sfmTag)
        {
            var startDate = DateTime.Now;

            log.Info("Started", cp.RequestToken);
            sfmTag = Constants.SfmConstants.ERROR.ToString();
            var result = false;
            var dbContracts = new List<DAL.Contract>();
            try
            {
                var wrestlerDb = ServiceLocator.Instance.WrestlingDAL.FindWrestlers(w => w.Id == wrestlerId, w => w.WrestlerDetails).FirstOrDefault();
                if (wrestlerDb != null)
                {
                    foreach (var c in contracts)
                    {
                        dbContracts.Add(new DAL.Contract
                        {
                            CompanyId = c.CompanyId,
                            EndDate = c.EndDate,
                            StartDate = c.StartDate,
                            IsExclusive = c.IsExclusive,
                            WrestlerId = wrestlerId
                        });
                    }
                    result = ServiceLocator.Instance.WrestlingDAL.AddWrestlerContracts(wrestlerId, dbContracts);
                    sfmTag = result ? Constants.SfmConstants.SUCCESS.ToString() : Constants.SfmConstants.ERROR.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken, sfmTag, startDate, DateTime.Now);
            return result;
        }
    }
}

