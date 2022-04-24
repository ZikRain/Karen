using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomApi.Utilities
{
    public class Helper
    {
        public static string NormalizePhoneNumber(string phone)
        {
            if (phone == null)
                return string.Empty;

            StringBuilder r_NormalizePhone = new StringBuilder();

            foreach (char chr in phone)
                if (Char.IsDigit(chr))
                    r_NormalizePhone.Append(chr);

            string _phone = r_NormalizePhone.ToString();

            return _phone.Length > 11 ? _phone.Substring(0, 11) : _phone;
        }
    }
}
