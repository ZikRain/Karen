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
    }
}