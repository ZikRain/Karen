using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomData.Entities
{
    public class OrderItemOption
    {
        public long order_item_option_id { get; set; }
        public string order_item_option_name { get; set; }
        public decimal order_item_option_price { get; set; }
        public long order_item_option_option_id { get; set; }
        public long order_item_option_order_item_id { get; set; }
    }
}
