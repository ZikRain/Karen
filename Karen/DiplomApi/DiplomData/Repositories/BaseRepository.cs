using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace DiplomData.Repositories
{
    public class BaseRepository : IDisposable
    {
        internal MySqlConnection _db_connection;
        public BaseRepository(IConfiguration configuration)
        {
            string connection_string = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            _db_connection = new MySqlConnection(connection_string);
        }
        public void Dispose()
        {
            _db_connection.Clone();
            _db_connection.Dispose();
        }
    }
}
