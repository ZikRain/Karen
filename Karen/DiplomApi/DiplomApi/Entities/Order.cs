using DiplomData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomApi.Entities
{
    public class Order
    {
        public long ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public string ContractName { get; set; }
        public long WorkerID { get; set; }
        public long CustomerID { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
