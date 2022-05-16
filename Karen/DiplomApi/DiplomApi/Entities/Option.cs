using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomApi.Entities
{
    public class Option
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long ProductID { get; set; }
    }
}
