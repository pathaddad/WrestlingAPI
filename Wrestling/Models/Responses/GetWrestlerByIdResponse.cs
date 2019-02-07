using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wrestling.Models.Entities;

namespace Wrestling.Models.Responses
{
    public class GetWrestlerByIdResponse
    {
        public Wrestler Wrestler { get; set; }
        public string SFMResult { get; set; }
    }
}