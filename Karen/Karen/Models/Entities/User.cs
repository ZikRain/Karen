using Karen.Models.Enums;

namespace Karen.Models.Entities
{
    public class User
    {
        public long user_id { get; set; }
        public string user_login { get; set; }
        public string user_password { get; set; }
        public string user_name { get; set; }
        public string user_surname { get; set; }
        public string user_patronymic { get; set; }
        public UserType user_type { get; set; }
    }
}
