using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.API.Entities
{
    public class Option
    {
        public long id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public long productID { get; set; }
        public bool enable { get; set; }
    }
}
