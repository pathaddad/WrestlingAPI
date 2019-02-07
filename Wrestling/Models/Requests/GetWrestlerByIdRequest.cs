using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wrestling.Models.Requests
{
    public class GetWrestlerByIdRequest : RequestCommonPack
    {
        public int WrestlerId { get; set; }
    }
}