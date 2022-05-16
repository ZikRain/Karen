using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.API.Entities
{
    public class OrderItemOption
    {
        public long id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public long optionID { get; set; }
        public long orderItemID { get; set; }
    }
}
