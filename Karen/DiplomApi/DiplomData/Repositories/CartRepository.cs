using Dapper;
using DiplomData.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DiplomData.Repositories
{
    public class CartRepository : BaseRepository
    {
        public CartRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Cart GetByUserId(long id)
        {
            _db_connection.Open();
            Cart cart = SqlMapper.Query<Cart>(_db_connection, "SELECT * FROM carts WHERE cart_userid=@id", new {id}).FirstOrDefault();
            _db_connection.Close();
            return cart;
        } 
        public void AddByUserId(long id)
        {
            _db_connection.Open();
            SqlMapper.Execute(_db_connection,"INSERT INTO carts (cart_userid) VALUES (@id)",new {id});
            _db_connection.Close();
        }
        public void DeleteById(long id)
        {
            _db_connection.Open();
            SqlMapper.Execute(_db_connection, "DELETE FROM carts WHERE cart_id=@id", new { id });
            _db_connection.Close();
        }
    }
}
