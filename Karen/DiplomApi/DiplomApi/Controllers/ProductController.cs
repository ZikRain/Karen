using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomData.Entities;
using DiplomData.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DiplomApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private ILogger<ProductController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public ProductController(ILogger<ProductController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Entities.Product>> List()
        {
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            {
                var products = await productRepository.GetAll();

                return products.Select(x=> new Entities.Product()
                {
                    ID = x.product_id,
                    Count = x.product_count,
                    Image = x.product_image,
                    Name = x.product_name,
                    Place1 = x.product_place1,
                    Plcae2 = x.product_place2,
                    Price = x.product_price,
                    Type = x.product_type
                });
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Entities.Product>> Liswt()
        {
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            {
                var products = await productRepository.GetAll();

                return products.Select(x => new Entities.Product()
                {
                    ID = x.product_id,
                    Count = x.product_count,
                    Image = x.product_image,
                    Name = x.product_name,
                    Place1 = x.product_place1,
                    Plcae2 = x.product_place2,
                    Price = x.product_price,
                    Type = x.product_type
                });
            }
        }
    }
}
