using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomApi.Entities
{
    public class OrderItemOption
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long OptionID { get; set; }
        public long OrderItemID { get; set; }
    }
}
