using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class CreateWrestlerRequestItem
    {
        public DateTime? DebutDate { get; set; }
        public List<WrestlerDetails> WrestlerDetails {get;set;}
    }
}
