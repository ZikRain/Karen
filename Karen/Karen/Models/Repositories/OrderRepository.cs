using Dapper;
using Karen.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Models.Repositories
{
    class OrderRepository:BaseRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration)
        {

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
