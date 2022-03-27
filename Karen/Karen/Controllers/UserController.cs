using Karen.Models.Entities;
using Karen.Models.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Archaeological_office.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;


        public UserController(ILogger<UserController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger; _webHostEnvironment = webHostEnvironment; _configuration = configuration;
        }
        public IActionResult RegistrationPage()
        {
            return View();
        }

        public IActionResult RegistrationAccess()
        {
            return View();
        }
        public IActionResult SignInPage()
        {
            return View();
        }
        public IActionResult UserInfo(long user_id)
        {
            UserRepository userRepository = new UserRepository(_configuration);
            User user = userRepository.GetUserById(user_id);
            return View(user);
        }
        public IActionResult MyUserInfo()
        {
            User user = new User();
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                string login = HttpContext.User.Identity.Name;
                user = userRepository.SearchLogin(login);
            }
            return View(user);
        }
        public IActionResult UserUpdatePage()
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                User user = userRepository.SearchLogin(HttpContext.User.Identity.Name);
                return View(user);

            }
        }
        public IActionResult Registration(string login, string password, string user_type)
        {
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                var error = false;

                if (string.IsNullOrWhiteSpace(login)) error = true;
                if (string.IsNullOrWhiteSpace(password)) error = true;
                if (userRepository.SearchLogin(login) != null) error = true;

                if (error)
                {

                    return Json(false);//не прошел проверку
                }
                else
                {
                    //прошел проверки

                    userRepository.AddNewUser(login, password);
                    return Json(true);
                }


            }
        }



        public async Task<IActionResult> SignIn(string login, string password)
        {

            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                User user = userRepository.SearchLogPas(login, password);
                if (user == null)
                {
                    return Json(false);
                }
                else
                {
                    //HttpContext.Response.Cookies.Append("Login", login);
                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, login), new Claim("Type", user.user_type.ToString()) };
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                    return Json(true);
                }


            }
        }
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignInPage");
        }

        public IActionResult UserApdate(string oldPassword, string newPassword, string login)
        {
            var error = false;
            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                if (userRepository.SearchLogin(HttpContext.User.Identity.Name).user_password == oldPassword)
                {

                    if (User.Identity.Name != login)
                    {
                        if (userRepository.SearchLogin(login) != null)
                        {
                            error = true;
                        }

                    }


                }
                else
                {
                    error = true;
                }


                if (!error)
                {
                    long id = userRepository.SearchLogin(User.Identity.Name).user_id;
                    SignOutShort();
                    if (string.IsNullOrWhiteSpace(newPassword))
                    {
                        userRepository.Update(newPassword, login, oldPassword,  id);

                        SignInShort(login, oldPassword);
                        return Json(true);
                    }
                    else
                    {
                        userRepository.Update(newPassword, login, oldPassword, id);

                        SignInShort(login, newPassword);
                        return Json(true);


                    }

                }
                else
                {
                    return Json(false);

                }

            }
        }
        public async void SignInShort(string login, string password)
        {

            using (UserRepository userRepository = new UserRepository(_configuration))
            {
                User user = userRepository.SearchLogPas(login, password);
                if (user == null)
                {

                }
                else
                {
                    //HttpContext.Response.Cookies.Append("Login", login);
                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, login) };
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));


                }


            }
        }
        public async void SignOutShort()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
    }
}