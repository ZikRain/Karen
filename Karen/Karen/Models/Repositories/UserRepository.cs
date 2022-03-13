using Dapper;
using Karen.Models.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Karen.Models.Repositories
{
    public class UserRepository:BaseRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
       public User GetUserById(long id)
        {
            User user = new User();
            _db_connection.Open();
            user = SqlMapper.Query<User>(_db_connection, "SELECT * FROM users WHERE user_id = @id", new { id = id }).FirstOrDefault();
            _db_connection.Close();
            return user;
        }
   
    }
}
