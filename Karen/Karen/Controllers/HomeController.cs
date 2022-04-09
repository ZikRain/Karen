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

        public IActionResult Catalog(int type = 0)
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
            using (CartRepository cartRepository = new CartRepository(_configuration))
            {
                User user = userRepository.SearchLogin(User.Identity.Name);
                Cart cart = cartRepository.GetByUserId(user.user_id);
                cart.Items = cartItemsRepository.GetByCartId(cart.cart_id);

                List<Product> item = new List<Product>();
                if(type == 0)
                {
                    ProductRepository rep = new ProductRepository(_configuration);
                    item = rep.GetAll();
                    foreach(var t in item)
                    {
                        if (cart.Items.FirstOrDefault(x => x.cartitem_productid == t.product_id) != null) t.incart = true;
                    }
                    return View(item);
                }
                else
                {
                    ProductRepository rep = new ProductRepository(_configuration);
                    item = rep.GetByType(type);
                    foreach (var t in item)
                    {
                        if (cart.Items.FirstOrDefault(x => x.cartitem_productid == t.product_id) != null) t.incart = true;
                    }
                    return View(item);
                }
           
            }


        }
        public IActionResult Work()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                List<User> user = userRepository.GetAll();

                return View(user);
            }
        }
        public IActionResult Storage()
        {
            using (ProductRepository rep = new ProductRepository(_configuration))
            {
                List<Product> product = rep.GetAll();

                return View(product);
            }
        }
        public IActionResult ViewProduct(long id)
        {
            using(ProductRepository rep = new ProductRepository(_configuration))
            {
                Product product = rep.GetById(id);
                return View(product);
            }
           
            
                
        }
<<<<<<< HEAD
=======
        public IActionResult Work()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                List<User> user = userRepository.GetAll();

                return View(user);
            }
        }
        public IActionResult Storage()
        {
            using (ProductRepository rep = new ProductRepository(_configuration))
            {
                List<Product> product = rep.GetAll();

                return View(product);
            }
        }
        public IActionResult ViewProduct(long id)
        {
            using(ProductRepository rep = new ProductRepository(_configuration))
            {
                Product product = rep.GetById(id);
                return View(product);
            }
           
            
                
        }
>>>>>>> f6262419e0189b5c8d9c95c58611153adaf07202

    }
}
