using DiplomApi.Exceptions;
using DiplomApi.Utilities;
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
    public class UserController : Controller
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

        private ILogger<UserController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<string> Register([FromForm][Required] string login, [FromForm][Required] string password)
        {
            using (TicketRepository ticketRepository = new TicketRepository(_configuration))
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                if(string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    throw new WebApiException("Логин или пароль заполнены некорректно");
                }

                if (await userRepository.GetByUserAndPassword(login, password) != null) 
                { 
                    throw new WebApiException("Пользователь с этими логином и паролем уже существует"); 
                }

                var user = await userRepository.Create(new DiplomData.Entities.User()
                {
                    user_login = login,
                    user_password = password,
                    user_type = DiplomData.Enums.UserType.Customer
                });

                var raw = Guid.NewGuid().ToString();

                var ticket = await ticketRepository.Create(new DiplomData.Entities.Ticket()
                {
                    ticket_creation_date = DateTime.Now,
                    ticket_user_id = user.user_id,
                    ticket_raw = raw
                });

                return raw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<string> Auth([FromForm][Required] string login, [FromForm][Required] string password)
        {
            using (TicketRepository ticketRepository = new TicketRepository(_configuration))
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                var user = await userRepository.GetByUserAndPassword(login, password);

                if (user == null)
                {
                    throw new WebApiException("Пользователя с таким логином и паролем не существует");
                }

                var raw = Guid.NewGuid().ToString();

                var ticket = await ticketRepository.Create(new DiplomData.Entities.Ticket()
                {
                    ticket_creation_date = DateTime.Now,
                    ticket_user_id = user.user_id,
                    ticket_raw = raw
                });

                return raw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async void SignOut()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (TicketRepository ticketRepository = new TicketRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                if (user == null)
                {
                    throw new UnauthorizedException();
                }

                await ticketRepository.Delete(Ticket);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> IsValidTicket()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                return user != null;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Entities.User> UpdateProfile([FromForm][Required] string name, [FromForm][Required] string surname, [FromForm][Required] string patronymic, [FromForm][Required] string telephone)
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (TicketRepository ticketRepository = new TicketRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                if (user == null)
                {
                    throw new UnauthorizedException();
                }

                user.user_name = name;
                user.user_surname = surname;
                user.user_patronymic = patronymic;
                user.user_telephone = telephone;

                user = await userRepository.Update(user);

                return new Entities.User()
                {
                    ID = user.user_id,
                    Name = user.user_name,
                    Surname = user.user_surname,
                    Patronymic = user.user_patronymic,
                    Telephone = user.user_telephone
                };
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<Entities.User> Get()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            using (TicketRepository ticketRepository = new TicketRepository(_configuration))
            {
                var user = await userRepository.GetByTicket(Ticket);

                if (user == null)
                {
                    throw new UnauthorizedException();
                }

                return new Entities.User()
                {
                    ID = user.user_id,
                    Name = user.user_name,
                    Surname = user.user_surname,
                    Patronymic = user.user_patronymic,
                    Telephone = user.user_telephone
                };
            }
        }
    }
}
