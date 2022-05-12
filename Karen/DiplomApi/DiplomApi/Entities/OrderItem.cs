using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomApi.Entities
{
    public class OrderItem
    {
        public long ID { get; set; }
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<OrderItemOption> Options { get; set; }
    }
}
