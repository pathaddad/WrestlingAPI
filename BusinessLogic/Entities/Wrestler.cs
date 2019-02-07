using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Wrestler
    {
        public int Id { get; set; }
        public string WrestlingName { get; set; }
        public string RealName { get; set; }
        public string BilledFrom { get; set; }
        public string HomeTown { get; set; }
        public List<string> Companies { get; set; }
        public bool IsExclusive { get; set; }
        public DateTime? DebutDate { get; set; }
    }
}
