using Diplom.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diplom.API.Entities
{
    public class Product
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long categoryID { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public int Place1 { get; set; }
        public int Plcae2 { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public List<Option> options { get; set; }
        public decimal Amount
        {
            get
            {
                return Quantity * (Price + options.Where(x => x.enable).Sum(x => x.price));
            }
        }
    }
}
