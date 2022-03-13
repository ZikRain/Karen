using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Models.Enums
{
    class ConvertEnum
    {
        public static string Convert(ProductType e)
        {
            switch (e)
            {
                case ProductType.Sofa: return "Диван";
                case ProductType.Table: return "Стол";
                case ProductType.Cupboard: return "Шкаф";
                case ProductType.Bed: return "Кровать";
                default: return null;

            }
        }
        //конвертация в int 
        public static int ConvertToInt(ProductType e)
        {
            switch (e)
            {
                case ProductType.Sofa: return 1;
                case ProductType.Table: return 2;
                case ProductType.Cupboard: return 3;
                case ProductType.Bed: return 4;
                default: return 0;

            }
        }
    }
}
