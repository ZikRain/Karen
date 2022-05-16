using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.API.Entities
{
    public class Category
    {
        public long id { get; set; }
        public string name { get; set; }
        public List<Product> products { get; set; }
    }
}
