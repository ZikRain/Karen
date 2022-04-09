using Karen.Models.Entities;
using Karen.Models.Repositories;
using System;
using Xunit;

namespace Test.Data.Repositories
{
    public class ProductRepositoryTest : BaseRepositoryTest
    {
        [Fact]
        public void GetByIdTest1()
        {
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            {
                var product = new Product()
                {
                    product_id = 15,
                    product_name = "asd",
                    product_type = Karen.Models.Enums.ProductType.Table,
                    product_image = "123123123",
                    product_count = 10,
                    product_place1 = 15,
                    product_place2 = 15,
                    product_price = 1000,
                };

                var newProduct = productRepository.AddNewProduct(product.product_name, (int)product.product_type, product.product_image, product.product_count, product.product_place1, product.product_place2, product.product_price);


                Assert.NotNull(newProduct);
            }
        }
    }
}
