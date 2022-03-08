using Karen.Models.Enums;
using System;

namespace Karen.Models.Entities
{
    public class Order
    {
        public long order_id { get; set; }
        public decimal order_amount { get; set; }
        public DateTime order_date { get; set; }
        public OrderState order_state { get; set; }
        public string order_contract_name { get; set; }
        public long order_worker_id { get; set; }
        public long order_customer_id  { get; set; }
    }
}
