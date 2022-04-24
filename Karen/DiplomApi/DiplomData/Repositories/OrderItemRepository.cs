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
    class OrderItemRepository : BaseRepository
    {
        public OrderItemRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public void AddItems(List<OrderItem> items)
        {
            _db_connection.Open();
            foreach(var t in items)
            {
                SqlMapper.Execute(_db_connection, "INSERT INTO order_items (order_item_order_id,order_item_product_id,order_item_name,order_item_price,order_item_quantity) VALUES " +
                " (@orderid,@productid,@name,@price,@quantity)",
                new { orderid = t.order_item_order_id, productid = t.order_item_product_id, name = t.order_item_name, price = t.order_item_price, quantity = t.order_item_quantity});
            }
            _db_connection.Close();
        }
    }
}
