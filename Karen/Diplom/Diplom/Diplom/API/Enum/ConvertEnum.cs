using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.API.Enum
{
    public static class ConvertEnum
    {
        public static string OrderStateToString(OrderState a)
        {
            switch (a)
            {
                case OrderState.New: return "Новый"; break;
                case OrderState.InProgress: return "В процессе"; break;
                case OrderState.Done: return "Готовый"; break;
                    default: return "";
            }
        } 
    }
}
