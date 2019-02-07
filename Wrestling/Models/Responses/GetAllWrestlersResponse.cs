using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wrestling.Models.Entities;

namespace Wrestling.Models.Responses
{
    public class GetAllWrestlersResponse
    {
        public List<Wrestler> Wrestlers { get; set; }
        public string SFMResult { get; set; }
    }
}