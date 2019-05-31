using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.Entities;
using Forum.Web.Data;
using Microsoft.AspNetCore.Http;

namespace Forum.Web.Controllers
{
    using Forum.Web.Constants;
    using Newtonsoft.Json;

    public class LoginController : Controller
    {
        private readonly IRepository<User> _userRepo;

        public LoginController(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var password = loginViewModel.Password;
                var hashedPassword = PasswordHasher.Hashing(password);
                var user = _userRepo.GetAll()
                    .Where(u => u.Username == loginViewModel.Username)
                    .Where(u => u.Password == hashedPassword)
                    .FirstOrDefault();

                if (user is null || user.IsDeleted == true)
                {
                    TempData[TemporaryMessage.temporaryMessage] = "An error occured while attempting to log in. Did you fill everything in correctly? ";
                    return View(loginViewModel);
                }
                else
                {
                    UserState userState = new UserState { 
                        UserId = user.Id,
                        Username = user.Username,
                        IsLoggedIn = true,
                        IsAdmin = false
                    };
                    HttpContext.Session.SetString(Constants.UserStatekey, JsonConvert.SerializeObject(userState));
                    return new RedirectToActionResult("Index", "Home", null);
                }
            }
            else
            {
                return View(loginViewModel);
            }

        }

        public IActionResult Logout()
        {
            UserState userState = new UserState { UserId = Guid.Empty, Username = null, IsLoggedIn = false, IsAdmin = false };
            HttpContext.Session.SetString(Constants.UserStatekey, JsonConvert.SerializeObject(userState));
            return new RedirectToActionResult("Login", "Login", null);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var password = registerViewModel.Password;
                var hashedPassword = PasswordHasher.Hashing(password);
                var user = new User();
                user.Username = registerViewModel.Username;
                user.Password = hashedPassword;

                var UsernameAlreadyTaken = _userRepo.GetAll()
                    .Where(u => u.Username == user.Username)
                    .FirstOrDefault();

                if (UsernameAlreadyTaken is null)
                {
                    _userRepo.Add(user);
                    TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully been registered as {registerViewModel.Username}. ";
                    return new RedirectToActionResult("Index", "Home", null);
                }
                else
                {
                    TempData[TemporaryMessage.temporaryMessage] = "There already exists a user with that username. Please enter a different username. ";
                    return View(registerViewModel);
                }

            }
            else
            {
                TempData[TemporaryMessage.temporaryMessage] = "An error occured while attempting to create your account. Did you fill everything in correctly? ";
                return View(registerViewModel);
            }

        }

        public IActionResult TermsOfService()
        {
            return View();
        }
    }
}