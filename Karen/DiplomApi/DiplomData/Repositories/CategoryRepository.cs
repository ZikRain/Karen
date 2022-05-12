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
    public class CategoryRepository : BaseRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Category>> List()
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<Category>(_db_connection, "SELECT * FROM categories;");
            }
        }

        public async Task<Category> GetByID(long category_id)
        {
            using (_db_connection)
            {
                _db_connection.Open();
                return await SqlMapper.QueryFirstOrDefaultAsync(_db_connection, "SELECT * FROM categories WHERE category_id = @category_id;", new { category_id = category_id });
            }
        }

        public async Task<Category> Update(Category category)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "UPDATE categories SET category_name = @category_name WHERE category_id = @category_id; SELECT * FROM categories WHERE category_id = @category_id;",
                category
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<Category>();
                }
            }
        }

        public async Task<Category> Create(Category category)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var items = await SqlMapper.QueryMultipleAsync(_db_connection,
                "INSERT INTO categories SET category_name = @category_name; SELECT * FROM categories WHERE category_id = LAST_INSERT_ID();",
                category
                ))
                {
                    return await items.ReadFirstOrDefaultAsync<Category>();
                }
            }
        }
    }
}
