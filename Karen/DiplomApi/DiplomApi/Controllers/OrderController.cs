using DiplomApi.Exceptions;
using DiplomData.Entities;
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
    public class OrderController : Controller
    {
        public string Ticket
        {
            get
            {
                string _ticket = null;

                var authHeader = Request.Headers["Authorization"];

                if (authHeader.Count > 0)
                {
                    var parameter = AuthenticationHeaderValue.Parse(authHeader);

                    if (parameter.Scheme == "Bearer")
                    {
                        _ticket = parameter.Parameter;
                    }
                }

                return _ticket;
            }
        }

        private ILogger<OrderController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public OrderController(ILogger<OrderController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Entities.Order>> List()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (OrderRepository orderRepository = new OrderRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                if (user == null)
                {
                    throw new UnauthorizedException();
                }

                var orders = await orderRepository.ListByUser(user.user_id);

                return orders.Select(x => new Entities.Order()
                {
                    ID = x.order_id,
                    Amount = x.order_amount,
                    ContractName = x.order_contract_name,
                    CustomerID = x.order_customer_id,
                    Date = x.order_date,
                    State = x.order_state,
                    WorkerID = x.order_worker_id
                });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<Entities.Order> Get([FromQuery] long order_id)
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (OrderRepository orderRepository = new OrderRepository(_configuration))
            using (OrderItemRepository orderItemRepository = new OrderItemRepository(_configuration))
            using (OrderItemOptionRepository orderItemOptionRepository = new OrderItemOptionRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                if (user == null)
                {
                    throw new UnauthorizedException();
                }

                var order = await orderRepository.GetByID(order_id);

                if(order == null)
                {
                    throw new WebApiException("Такой заказ отсутствует");
                }

                var order_items = await orderItemRepository.ListByOrder(order_id);

                var options = await orderItemOptionRepository.ListByOrderItems(order_items.Select(x => x.order_item_id).ToArray());

                return new Entities.Order()
                {
                    ID = order.order_id,
                    Amount = order.order_amount,
                    ContractName = order.order_contract_name,
                    CustomerID = order.order_customer_id,
                    Date = order.order_date,
                    State = order.order_state,
                    WorkerID = order.order_worker_id,
                    Items = order_items.Select(x=> new Entities.OrderItem()
                    {
                        ID = x.order_item_id,
                        Name = x.order_item_name,
                        OrderID = x.order_item_order_id,
                        Price = x.order_item_price,
                        ProductID = x.order_item_product_id,
                        Quantity = x.order_item_quantity,
                        Options = options.Where(opt => x.order_item_id == opt.order_item_option_order_item_id).Select(opt => new Entities.OrderItemOption()
                        {
                            ID = opt.order_item_option_id,
                            Name = opt.order_item_option_name,
                            OptionID = opt.order_item_option_option_id,
                            OrderItemID = opt.order_item_option_order_item_id,
                            Price = opt.order_item_option_price
                        })
                    })
                };
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Entities.Order> Create([Required][FromForm] Dictionary<long, int> orderItems, [FromForm] long[] options)
        {
            using (OptionRepository optionRepository = new OptionRepository(_configuration))
            using (OrderItemOptionRepository orderItemOptionRepository = new OrderItemOptionRepository(_configuration))
            using (OrderRepository orderRepository = new OrderRepository(_configuration))
            using (ProductRepository productRepository = new ProductRepository(_configuration))
            using (OrderItemRepository orderItemRepository = new OrderItemRepository(_configuration))
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                if (user == null)
                {
                    throw new UnauthorizedException();
                }

                var order = orderRepository.AddOrder(new Order()
                {
                    order_state = DiplomData.Enums.OrderState.New,
                    order_date = DateTime.Now,
                    order_amount = 0,
                    order_customer_id = user.user_id
                });

                var products = await productRepository.GetAll();

                var options_list = await optionRepository.List();

                foreach (var orderItem in orderItems)
                {
                    var product = products.FirstOrDefault(x => x.product_id == orderItem.Key);

                    var order_item = await orderItemRepository.Create(new OrderItem()
                    {
                        order_item_product_id = product.product_id,
                        order_item_name = product.product_name,
                        order_item_order_id = order.order_id,
                        order_item_price = product.product_price,
                        order_item_quantity = orderItem.Value
                    });

                    if(options_list != null)
                    {
                        foreach(var opt in options)
                        {
                            var selected_option = options_list.FirstOrDefault(x => x.option_product_id == product.product_id && x.option_id == opt);

                            if(selected_option != null)
                            {
                                var option = await orderItemOptionRepository.Create(new OrderItemOption()
                                {
                                    order_item_option_name = selected_option.option_name,
                                    order_item_option_option_id = selected_option.option_id,
                                    order_item_option_order_item_id = order_item.order_item_id,
                                    order_item_option_price = selected_option.option_price
                                });

                                order.order_amount += (decimal)option.order_item_option_price * (decimal)order_item.order_item_quantity;
                            }
                        }
                    }

                    order.order_amount += (decimal)order_item.order_item_price * (decimal)order_item.order_item_quantity;
                }

                await orderRepository.Update(order);

                var orderDetails = await orderItemRepository.ListByOrder(order.order_id);

                var items_options = await orderItemOptionRepository.ListByOrderItems(orderDetails.Select(x => x.order_item_id).ToArray());

                return new Entities.Order()
                {
                    Date = order.order_date,
                    ID = order.order_id,
                    Amount = order.order_amount,
                    ContractName = order.order_contract_name,
                    State = order.order_state,
                    CustomerID = order.order_customer_id,
                    WorkerID = order.order_worker_id,
                    Items = orderDetails.Select(orderDetail => new Entities.OrderItem()
                    {
                        ID = orderDetail.order_item_id,
                        ProductID = orderDetail.order_item_product_id,
                        Name = orderDetail.order_item_name,
                        Price = orderDetail.order_item_price,
                        Quantity = orderDetail.order_item_quantity,
                        OrderID = orderDetail.order_item_order_id,
                        Options = items_options.Where(opt => orderDetail.order_item_id == opt.order_item_option_order_item_id).Select(opt=> new Entities.OrderItemOption()
                        {
                            ID = opt.order_item_option_id,
                            Name = opt.order_item_option_name,
                            OptionID = opt.order_item_option_option_id,
                            OrderItemID = opt.order_item_option_order_item_id,
                            Price = opt.order_item_option_price
                        })
                    }).ToList()
                };
            }
        }
    }
}
