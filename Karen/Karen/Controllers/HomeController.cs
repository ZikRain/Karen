using Karen.Models;
using Karen.Models.Entities;
using Karen.Models.Enums;
using Karen.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Karen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy(int type = 0)
        {
            List<Product> item = new List<Product>();
            if(type == 0)
            {
                ProductRepository rep = new ProductRepository(_configuration);
                item = rep.GetAll();
                return View(item);
            }
            else
            {
                ProductRepository rep = new ProductRepository(_configuration);
                item = rep.GetByType(type);
                return View(item);
            }
           
            
            
        }
       
            
    }
}
