using Karen.Models.Entities;
using Karen.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Karen.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public CartController(ILogger<CartController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }
        public IActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (UserRepository userRepository = new UserRepository(_configuration))
                using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
                using (CartRepository cartRepository = new CartRepository(_configuration))
                {
                    User user = userRepository.SearchLogin(User.Identity.Name);
                    Cart cart = cartRepository.GetByUserId(user.user_id);
                    if (cart == null)
                    {
                        cartRepository.AddByUserId(user.user_id);
                        cart = cartRepository.GetByUserId(user.user_id);
                    }
                    cart.Items = cartItemsRepository.GetByCartId(cart.cart_id);
                    return View(cart);
                }
            }
            else
            {
                return Redirect("/User/SignInPage");
            }
        }
        public IActionResult AddCartItem(long productid)
        {
            if (User.Identity.IsAuthenticated)
            {
                using(UserRepository userRepository = new UserRepository(_configuration))
                using (ProductRepository productRepository = new ProductRepository(_configuration))
                using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
                using (CartRepository cartRepository = new CartRepository(_configuration))
                {
                    User user = userRepository.SearchLogin(User.Identity.Name);
                    Product product = productRepository.GetById(productid);
                    Cart cart = cartRepository.GetByUserId(user.user_id);
                    if (cart == null)
                    {
                        cartRepository.AddByUserId(user.user_id);
                        cart = cartRepository.GetByUserId(user.user_id);
                    }
                    CartItem cartItem = new CartItem()
                    {
                        cartitem_name = product.product_name,
                        cartitem_price = product.product_price,
                        cartitem_productid = product.product_id,
                        cartitem_quantity = 1,
                        cartitem_cartid = cart.cart_id
                    };

                    cartItemsRepository.Add(cartItem);
                    return Json(true);
                }
            }
            else
            {
                return Json(false) ;
            }

            
        }
        public IActionResult PlusCartItem(long cartitemid)
        {
            using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
            {
                cartItemsRepository.PlusById(cartitemid);
                return Json(true);
            }
        }
        public IActionResult MinusCartItem(long cartitemid)
        {
            using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
            {
                cartItemsRepository.MinusById(cartitemid);
                return Json(true);
            }
        }
        public IActionResult DeleteCartItem(long cartitemid)
        {
            using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
            {
                cartItemsRepository.DeleteById(cartitemid);
                return Json(true);
            }
        }
        public IActionResult AddOrder()
        {
            using(OrderRepository orderRepository = new OrderRepository(_configuration))
            using(OrderItemRepository orderItemRepository = new OrderItemRepository(_configuration))
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            using (CartItemsRepository cartItemsRepository = new CartItemsRepository(_configuration))
            using (CartRepository cartRepository = new CartRepository(_configuration))
            {
                //доделать сбор заказа и удаление всех предметов из корзины
                User user = userRepository.SearchLogin(User.Identity.Name);
                Cart cart = cartRepository.GetByUserId(user.user_id);
                cart.Items = cartItemsRepository.GetByCartId(cart.cart_id);

                Order order = new Order() {
                order_amount=cart.Total,
                order_customer_id=cart.cart_userid,
                order_date=System.DateTime.Now,
                order_state=Models.Enums.OrderState.New,
                };
                order = orderRepository.AddOrder(order);

                List<OrderItem> orderitems = new List<OrderItem>();
                foreach (var t in cart.Items)
                {
                    orderitems.Add(new OrderItem()
                    {
                        order_item_name = t.cartitem_name,
                        order_item_order_id = order.order_id,
                        order_item_price = t.cartitem_price,
                        order_item_product_id = t.cartitem_productid,
                        order_item_quantity = t.cartitem_quantity,
                        
                    });
                }
                orderItemRepository.AddItems(orderitems);

                cartItemsRepository.DeleteByCartId(cart.cart_id);
                cartRepository.DeleteById(cart.cart_id);

                return Json(true);
            }
        }
    }
}
