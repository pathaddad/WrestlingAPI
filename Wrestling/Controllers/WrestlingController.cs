using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wrestling.Models.Requests;
using Wrestling.Models.Responses;

namespace Wrestling.Controllers
{
    public class WrestlingController : ApiController
    {
        private Logger log = new Logger();

        [HttpPost]
        [ActionName("GetWrestlerById")]
        public IHttpActionResult GetWrestlerById([FromBody] GetWrestlerByIdRequest request)
        {
            var cp = GetCP(request);
            var sfmTag = string.Empty;
            log.Info("Start", cp.RequestToken);
            var startDate = DateTime.UtcNow;
            var apiResponse = new GetWrestlerByIdResponse();
            try
            {
                var wrestlerResult = ServiceLocator.Instance.Wrestling.GetWrestlerById(request.WrestlerId, cp, out sfmTag);
                if(wrestlerResult != null)
                {
                    apiResponse.Wrestler = Mapper.Map<BusinessLogic.Entities.Wrestler, Models.Entities.Wrestler>(wrestlerResult);
                }
            }        
            catch (Exception ex)
            {              
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken);
            return Ok(apiResponse);
        }
        
        [HttpPost]
        [ActionName("GetAllWrestlers")]
        public IHttpActionResult GetAllWrestlers([FromBody] GetAllWrestlersRequest request)
        {
            var cp = GetCP(request);
            var sfmTag = string.Empty;
            log.Info("Start", cp.RequestToken);
            var startDate = DateTime.UtcNow;
            var apiResponse = new GetAllWrestlersResponse();
            try
            {
                var wrestlersResult = ServiceLocator.Instance.Wrestling.GetAllWrestlers(request.PageIndex, request.PageSize, cp, out sfmTag);
                apiResponse.Wrestlers = Mapper.Map<List<BusinessLogic.Entities.Wrestler>, List<Models.Entities.Wrestler>>(wrestlersResult);
            }
            catch (Exception ex)
            {
                log.Error("Error", cp.RequestToken, ex);
            }
            log.Info("End", cp.RequestToken);
            return Ok(apiResponse);
        }

        #region Private
        private CommonPack GetCP(dynamic commonPack)
        {
            return new CommonPack
            {
                LanguageCode = commonPack.LanguageCode,
                RequestToken = Guid.NewGuid().ToString()          
            };
        }
        #endregion
    }
}
