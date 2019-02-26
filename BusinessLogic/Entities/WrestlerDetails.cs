using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class WrestlerDetails
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string WrestlingName { get; set; }
        public string RealName { get; set; }
        public string BilledFrom { get; set; }
        public string HomeTown { get; set; }
    }
}
