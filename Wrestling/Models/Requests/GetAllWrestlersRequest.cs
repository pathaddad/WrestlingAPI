using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wrestling.Models.Requests
{
    public class GetAllWrestlersRequest : RequestCommonPack
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}