using Dapper;
using DiplomData.Entities;
using DiplomData.Repositories;
using DiplomData.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomData.Repositories
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (_db_connection)
            {
                _db_connection.Open();

                return await SqlMapper.QueryAsync<Product>(_db_connection, "SELECT * FROM products");
            }
        }

        public List<Product> GetByCategory(long product_category_id)
        {
            List<Product> product = new List<Product>();
            _db_connection.Open();
            product = SqlMapper.Query<Product>(_db_connection, "SELECT * FROM products WHERE product_category_id = @product_category_id ", new { product_category_id = product_category_id }).ToList();
            _db_connection.Close();
            return product;
        }
        public Product GetById(long id)
        {
            Product product = new Product();
            _db_connection.Open();
            product = SqlMapper.Query<Product>(_db_connection,"SELECT * FROM products WHERE product_id =@id", new { id = id }).FirstOrDefault();
            _db_connection.Close();
            return product;
        }
        public Product AddNewProduct(string name, long product_category_id, string image,  int count, int place1, int place2, decimal price)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var item = SqlMapper.QueryMultiple(_db_connection, "INSERT INTO products (product_name, product_count, product_place1, product_price, product_category_id, product_image, product_place2) VALUES" +
                " (@name,@count,@place1,@price,@product_category_id,@image,@place2); " +
                    "SELECT * FROM products WHERE product_id= LAST_INSERT_ID()", new { name, count, place1, price, product_category_id, image, place2 })) { return item.ReadFirstOrDefault<Product>(); };
            }
        }
    }
}