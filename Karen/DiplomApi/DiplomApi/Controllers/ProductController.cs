using DiplomApi.Exceptions;
using DiplomApi.Utilities;
using DiplomData.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiplomApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public ProductController(ILogger<ProductController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Entities.Category>> List()
        {
            using (CategoryRepository categoryRepository = new CategoryRepository(_configuration))
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            {
                var products = await productRepository.GetAll();

                var categories = await categoryRepository.List();

                return categories.Select(x=> new Entities.Category()
                {
                    ID = x.category_id,
                    Name = x.category_name,
                    Products = products.Where(c=>c.product_category_id == x.category_id).Select(c=> new Entities.Product()
                    {
                        ID = c.product_id,
                        Count = c.product_count,
                        Image = $"https://www.meme-arsenal.com/memes/20a12ee075efafbc11e355298f607535.jpg",
                        Name = c.product_name,
                        Place1 = c.product_place1,
                        Plcae2 = c.product_place2,
                        Price = c.product_price,
                        CategoryID = x.category_id,
                    })
                });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Entities.Option>> GetOptions([FromQuery] long product_id)
        {
            using (OptionRepository optionRepository = new OptionRepository(_configuration))
            {
                var options = await optionRepository.ListByProduct(product_id);

                return options.Select(x => new Entities.Option()
                {
                    ID = x.option_id,
                    ProductID = x.option_product_id,
                    Name = x.option_name,
                    Price = x.option_price
                });
            }
        }
    }
}
