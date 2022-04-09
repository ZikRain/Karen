using Karen.Models.Entities;
using Karen.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Karen.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public ProductController(ILogger<ProductController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }
        public IActionResult NewProduct()
        {
            return View();
        }


        public async Task<IActionResult> AddNewProduct(string name, int type, IFormFile image, int count, int place1, int place2, decimal price)
        {
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            {
                var imagepath = await PostFiles(image);
                productRepository.AddNewProduct(name,type,imagepath, count, place1,place2,price);
                return Json(true);

            }
        }


        public async Task<string> PostFiles(IFormFile file)
        {
            if (file == null)
            {
                return "logo.bmp";
            }
            else
            {


                var ext = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString() + ext;

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, $"Images\\{fileName}");
                try
                {
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }catch (Exception ex) 
                { 

                }
                
                return fileName;
            }
        }
    }
}
