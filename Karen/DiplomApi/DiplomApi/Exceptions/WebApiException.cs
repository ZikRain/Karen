using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomApi.Exceptions
{
    [Serializable]
    public class WebApiException : Exception
    {
        public WebApiException() { }

        public WebApiException(string message) : base(message)
        {
        }
    }
}
