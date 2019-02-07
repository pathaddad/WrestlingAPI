using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wrestling
{
    public class AutoMapperBootStrapper
    {
        public static void BootStrap()
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<BusinessLogic.Entities.Wrestler, Models.Entities.Wrestler>();
                });

                Mapper.Configuration.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
    }
}