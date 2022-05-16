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
    public class OrderItemOptionRepository : BaseRepository
    {
        public OrderItemOptionRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<OrderItemOption>> ListByOrderItem(long order_item_option_order_item_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<OrderItemOption>(_db_connection, "SELECT * FROM order_item_options WHERE order_item_option_order_item_id = @order_item_option_order_item_id;",
                    new { order_item_option_order_item_id });
            }
        }

        public async Task<IEnumerable<OrderItemOption>> ListByOrderItems(long[] order_item_option_order_item_ids)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<OrderItemOption>(_db_connection, "SELECT * FROM order_item_options WHERE order_item_option_order_item_id IN @order_item_option_order_item_ids;",
                    new { order_item_option_order_item_ids });
            }
        }

        public async Task<OrderItemOption> GetByID(long order_item_option_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();
                return await SqlMapper.QueryFirstOrDefaultAsync(_db_connection, "SELECT * FROM order_item_options WHERE order_item_option_id = @order_item_option_id;", new { order_item_option_id = order_item_option_id });
            }
        }

        public async Task<OrderItemOption> Update(OrderItemOption orderItemOption)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "UPDATE order_item_options SET order_item_option_name = @order_item_option_name, order_item_option_price = @order_item_option_price, order_item_option_option_id = @order_item_option_option_id, order_item_option_order_item_id = @order_item_option_order_item_id WHERE order_item_option_id = @order_item_option_id; SELECT * FROM order_item_options WHERE order_item_option_id = @order_item_option_id;",
                orderItemOption
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<OrderItemOption>();
                }
            }
        }

        public async Task<OrderItemOption> Create(OrderItemOption orderItemOption)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "INSERT INTO order_item_options SET order_item_option_name = @order_item_option_name, order_item_option_price = @order_item_option_price, order_item_option_option_id = @order_item_option_option_id, order_item_option_order_item_id = @order_item_option_order_item_id; SELECT * FROM order_item_options WHERE order_item_option_id = LAST_INSERT_ID();",
                orderItemOption
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<OrderItemOption>();
                }
            }
        }
    }
}
