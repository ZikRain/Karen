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
    public class OrderItemRepository : BaseRepository
    {
        public OrderItemRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<OrderItem>> ListByOrder(long order_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<OrderItem>(_db_connection, "SELECT * FROM order_items WHERE order_item_order_id = @order_id;",
                    new { order_id });
            }
        }

        public async Task<OrderItem> Create(OrderItem order_item)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "insert into order_items set order_item_product_id = @order_item_product_id, order_item_name = @order_item_name, order_item_quantity = @order_item_quantity, order_item_price = @order_item_price, order_item_order_id = @order_item_order_id; select * from order_items where order_item_id = LAST_INSERT_ID();",
                order_item
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<OrderItem>();
                }
            }
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
