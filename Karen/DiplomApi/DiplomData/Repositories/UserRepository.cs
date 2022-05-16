using Dapper;
using DiplomData.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomData.Repositories
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

        public async Task<User> Update(User user)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "UPDATE users SET user_login = @user_login, user_password = @user_password, user_name = @user_name, user_surname = @user_surname, user_patronymic = @user_patronymic, user_type = @user_type, user_telephone = @user_telephone WHERE user_id = @user_id; select * from users where user_id = @user_id;",
                user
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<User>();
                }
            }
        }

        public async Task<User> Create(User user)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "insert into users set user_login = @user_login, user_password = @user_password, user_name = @user_name, user_surname = @user_surname, user_patronymic = @user_patronymic, user_type = @user_type, user_telephone = @user_telephone; select * from users where user_id = LAST_INSERT_ID();",
                user
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<User>();
                }
            }
        }

        public async Task<User> GetByTicket(string ticket_raw)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryFirstOrDefaultAsync<User>(_db_connection,
                "select * from users join tickets on (user_id = ticket_user_id) where ticket_raw = @ticket_raw;",
                new { ticket_raw }
                );
            }
        }

        public List<User> GetAll()
        {
            _db_connection.Open();

            List<User> user = SqlMapper.Query<User>(_db_connection, "SELECT * FROM users").ToList();

            _db_connection.Close();
            return user;
        }

        public async Task<User> GetByUserAndPassword(string login, string password)
        {
            using (_db_connection)
            {
                return await SqlMapper.QueryFirstOrDefaultAsync<User>(_db_connection, 
                    "SELECT * FROM users WHERE user_login = @login AND user_password = @password", 
                    new { password = password, login = login });
            }
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
