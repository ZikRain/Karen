using Diplom.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diplom.API.Entities
{
    public class Order
    {
        public long id { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }
        public OrderState state { get; set; }
        public string contractName { get; set; }
        public long workerID { get; set; }
        public long customerID { get; set; }
        public List<OrderItem> items { get; set; }
        public int CountItems
        {
            get
            {   if (items != null)
                {
                    return items.Sum(x => x.quantity);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
