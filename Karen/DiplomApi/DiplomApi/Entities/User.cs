using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomApi.Entities
{
    public class User
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Telephone { get; set; }
    }
}
