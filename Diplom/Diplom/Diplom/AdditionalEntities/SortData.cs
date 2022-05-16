using Diplom.API.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.AdditionalEntities
{
    public class SortData
    {
        public ProductType type { get; set; }
        public string name_type
        {
            get { return Utility.Utility.ProductTypeToString(type); }
        }
        public bool enable_type { get; set; }
    }
}
