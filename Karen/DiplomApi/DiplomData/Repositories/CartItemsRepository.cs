using Dapper;
using DiplomData.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DiplomData.Repositories
{
    public class CartItemsRepository:BaseRepository
    {
        public CartItemsRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<CartItem> GetByCartId(long id)
        {
            _db_connection.Open();
            List<CartItem> cartItems = SqlMapper.Query<CartItem>(_db_connection, "SELECT * FROM cartitems WHERE cartitem_cartid=@id", new { id }).ToList();
            _db_connection.Close();
            return cartItems;
        }

        public CartItem GetByCartAndProductId(long cartid,long productid)
        {
            _db_connection.Open();
            CartItem cartItems = SqlMapper.Query<CartItem>(_db_connection, "SELECT * FROM cartitems WHERE cartitem_cartid=@cartid AND cartitem_productid=@productid", new { cartid,productid }).FirstOrDefault();
            _db_connection.Close();
            return cartItems;
        }

        public void Add(CartItem item)
        {
            _db_connection.Open();
            SqlMapper.Execute(_db_connection, "INSERT INTO cartitems (cartitem_cartid,cartitem_productid,cartitem_price,cartitem_quantity,cartitem_name) VALUES " +
                "(@cartid,@productid,@price,@quantity,@name)", new { cartid = item.cartitem_cartid, productid = item.cartitem_productid, price = item.cartitem_price, quantity = item.cartitem_quantity,name=item.cartitem_name});
            _db_connection.Close();
        }

        public CartItem GetById(long id)
        {
            _db_connection.Open();
            CartItem cartItems = SqlMapper.Query<CartItem>(_db_connection, "SELECT * FROM cartitems WHERE cartitem_id=@id", new { id }).FirstOrDefault();
            _db_connection.Close();
            return cartItems;

        }

        public void MinusById(long id)
        {
            _db_connection.Open();
            CartItem cartItems = SqlMapper.Query<CartItem>(_db_connection, "SELECT * FROM cartitems WHERE cartitem_id=@id", new { id }).FirstOrDefault();
            if(cartItems.cartitem_quantity == 1)
            {
                SqlMapper.Execute(_db_connection, "DELETE FROM cartitems WHERE cartitem_id=@id", new { id });
            }
            else
            {
                SqlMapper.Execute(_db_connection, "UPDATE cartitems SET cartitem_quantity=@count WHERE cartitem_id=@id", new { count = cartItems.cartitem_quantity - 1, id = id });
            }
            _db_connection.Close();
        }

        public void PlusById(long id)
        {
            _db_connection.Open();
            CartItem cartItems = SqlMapper.Query<CartItem>(_db_connection, "SELECT * FROM cartitems WHERE cartitem_id=@id", new { id }).FirstOrDefault();
            SqlMapper.Execute(_db_connection, "UPDATE cartitems SET cartitem_quantity=@count WHERE cartitem_id=@id", new { count = cartItems.cartitem_quantity + 1, id = id });
            _db_connection.Close();
        }

        public void DeleteById(long id)
        {
            _db_connection.Open();
            SqlMapper.Execute(_db_connection, "DELETE FROM cartitems WHERE cartitem_id=@id", new { id });
            _db_connection.Close();
        }

        public void DeleteByCartId(long id)
        {
            _db_connection.Open();
            SqlMapper.Execute(_db_connection, "DELETE FROM cartitems WHERE cartitem_cartid=@id", new { id });
            _db_connection.Close();
        }
    }
}
