using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diplom.API.Entities
{
    public class OrderItem
    {
        public long id { get; set; }
        public long orderID { get; set; }
        public long productID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public List<OrderItemOption> options { get; set; }
        public decimal amount
        {
            get { return price*quantity + options.Sum(x=>x.price); }
        }
    }
}
