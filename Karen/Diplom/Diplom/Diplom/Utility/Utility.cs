using Diplom.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diplom.Utility
{
    internal class Utility
    {
        public static string DeleteAllCharactersExceptOrNumbers(string text)
        {
            if (text == null) return null;
            text = new string(text.Where(t => char.IsDigit(t)).ToArray());
            return text;
        }

        public static string ProductTypeToString(ProductType type)
        {
            switch (type)
            {
                case ProductType.Sofa: return "Диван";
                case ProductType.Bed: return "Кровать";
                case ProductType.Cupboard: return "Шкаф";
                case ProductType.Table: return "Стол";
                default: return "";
            }
        }

    }
}
