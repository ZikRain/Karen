using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.API.Entities
{
    public class User
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string telephone { get; set; }
    }
}
