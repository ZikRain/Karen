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
    public class OptionRepository : BaseRepository
    {
        public OptionRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Option>> List()
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<Option>(_db_connection, "SELECT * FROM options;");
            }
        }

        public async Task<IEnumerable<Option>> ListByProduct(long option_product_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<Option>(_db_connection, "SELECT * FROM options WHERE option_product_id = @option_product_id;",
                    new { option_product_id });
            }
        }

        public async Task<Option> GetByID(long option_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();
                return await SqlMapper.QueryFirstOrDefaultAsync(_db_connection, "SELECT * FROM options WHERE option_id = @option_id;", new { option_id = option_id });
            }
        }

        public async Task<Option> Update(Option option)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "UPDATE options SET option_name = @option_name, option_price = @option_price, option_product_id = @option_product_id WHERE option_id = @option_id; SELECT * FROM options WHERE option_id = @option_id;",
                option
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<Option>();
                }
            }
        }

        public async Task<Option> Create(Option option)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "INSERT INTO options SET option_name = @option_name, option_price = @option_price, option_product_id = @option_product_id; SELECT * FROM options WHERE option_id = LAST_INSERT_ID();",
                option
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<Option>();
                }
            }
        }
    }
}
