using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomData.Entities
{
    public class Option
    {
        public long option_id { get; set; }
        public string option_name { get; set; }
        public decimal option_price { get; set; }
        public long option_product_id { get; set; }
    }
}
