using Dapper;
using DiplomData.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomData.Repositories
{
    public class OrderRepository : BaseRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Order>> ListByUser(long user_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<Order>(_db_connection, "SELECT * FROM orders WHERE order_customer_id = @user_id;",
                    new { user_id });
            }
        }

        public async Task<Order> GetByID(long order_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();
                return SqlMapper.QueryFirstOrDefault(_db_connection, "SELECT * FROM orders WHERE order_id = @order_id;", new { order_id = order_id });
            }
        }

        public async Task<Order> Update(Order order)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "UPDATE orders SET order_amount = @order_amount, order_date = @order_date, order_state = @order_state, order_contract_name = @order_contract_name, order_worker_id = @order_worker_id, order_customer_id = @order_customer_id WHERE order_id = @order_id; SELECT * FROM orders WHERE order_id = @order_id;",
                order
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<Order>();
                }
            }
        }

        public Order AddOrder(Order order)
        {
            _db_connection.Open();
            SqlMapper.Execute(_db_connection, "INSERT INTO orders (order_amount,order_date,order_state,order_customer_id) VALUES " +
                " (@amount,@date,@state,@customerid)",
                new {amount=order.order_amount,date=order.order_date,state=order.order_state,customerid=order.order_customer_id});
            order = SqlMapper.Query<Order>(_db_connection, "SELECT * FROM orders").Last();
            _db_connection.Close();
            return order;
        }
    }
}
