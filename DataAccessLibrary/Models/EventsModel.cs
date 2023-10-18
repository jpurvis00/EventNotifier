using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class EventsModel
    {
        public DateTime EventDate { get; set; }
        public string Subject { get; set; }
        public string Event { get; set; }
        public List<string> ToEmailAddresses { get; set; } = new List<string>();
    }
}
