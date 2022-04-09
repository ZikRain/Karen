using Dapper;
using Karen.Models.Entities;
using Karen.Models.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Models.Repositories
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<Product>  GetAll()
        {
            List<Product> product = new List<Product>();
            _db_connection.Open();
            product = SqlMapper.Query<Product>(_db_connection, "SELECT * FROM products").ToList();
            _db_connection.Close();
            return product;
        }

        public List<Product> GetByType(int type)
        {
            List<Product> product = new List<Product>();
            _db_connection.Open();
            product = SqlMapper.Query<Product>(_db_connection, "SELECT * FROM products WHERE product_type = @type ", new { type = type }).ToList();
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
        public Product AddNewProduct(string name, int type,string image,  int count, int place1, int place2, decimal price)
        {
            using (_db_connection)
            {
                _db_connection.Open();

                using (var item = SqlMapper.QueryMultiple(_db_connection, "INSERT INTO products (product_name, product_count, product_place1, product_price, product_type, product_image, product_place2) VALUES" +
                " (@name,@count,@place1,@price,@type,@image,@place2); " +
                    "SELECT * FROM products WHERE product_id= LAST_INSERT_ID()", new { name, count, place1, price, type, image, place2 })) { return item.ReadFirstOrDefault<Product>(); };
            }
        }
    }
}