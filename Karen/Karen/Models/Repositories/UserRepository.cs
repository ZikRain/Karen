using Dapper;
using Karen.Models.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
            using (_db_connection)
            {
                _db_connection.Open();
                return SqlMapper.QueryFirstOrDefault(_db_connection, "SELECT * FROM users WHERE user_id = @id", new { id = id });
            }
            
        }

        public User SearchLogin(string login)
        {
            _db_connection.Open();

            User user = SqlMapper.Query<User>(_db_connection, "SELECT * FROM users WHERE user_login = @login", new { login = login }).FirstOrDefault();
            _db_connection.Close();

            return user;
        }

        public User AddNewUser(string login, string password)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using(var item =  SqlMapper.QueryMultiple(_db_connection, "INSERT INTO users (user_login,user_password,user_type) VALUES (@user_login,@user_password,1) ; " +
                    "SELECT * FROM users WHERE user_id= LAST_INSERT_ID()", new { user_login = login, user_password = password })){ return item.ReadFirstOrDefault<User>(); };
            }
        }
        public List<User> GetAll()
        {
            _db_connection.Open();

            List<User> user = SqlMapper.Query<User>(_db_connection, "SELECT * FROM users").ToList();

            _db_connection.Close();
            return user;
        }
        public User SearchLogPas(string login, string password)
        {
            _db_connection.Open();
            User user = SqlMapper.Query<User>(_db_connection, "SELECT * FROM users WHERE user_login=@login AND user_password = @password", new { password = password, login = login }).FirstOrDefault();
            _db_connection.Close();
            return user;
        }
        public void Update(string password, string login, string oldPassword, long id)
        {

            string request = "UPDATE users SET ";
            if (!string.IsNullOrWhiteSpace(password)) request += " user_password = '" + password + "' ,";
            if (!string.IsNullOrWhiteSpace(login)) request += " user_login = '" + login + "' ,";
            if (request.EndsWith(","))
            {
                request = request.Remove(request.Length - 1, 1);

            }
            _db_connection.Open();
            SqlMapper.Execute(_db_connection, request + " WHERE user_id=@id", new { id = id });
        }
    }
}
