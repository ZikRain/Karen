using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomData.Entities
{
    public class Ticket
    {
        public long ticket_id { get; set; }
        public long ticket_user_id { get; set; }
        public string ticket_raw { get; set; }
        public DateTime ticket_creation_date { get; set; }
    }
}
