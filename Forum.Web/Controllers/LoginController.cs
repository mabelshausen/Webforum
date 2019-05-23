using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.Entities;
using Forum.Web.Data;

namespace Forum.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly ForumContext _context;

        public LoginController(ForumContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var password = registerViewModel.Password;
                var hashedPassword = PasswordHasher.Hashing(password);
                var user = new User();
                user.Username = registerViewModel.Username;
                user.Password = hashedPassword;

                var UsernameAlreadyTaken = _context.Users.Where(u => u.Username == user.Username).FirstOrDefault();

                if (UsernameAlreadyTaken is null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
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
                TempData[TemporaryMessage.temporaryMessage] = "An error occured while attempting to create your account. Did you correctly fill everything in? ";
                return View(registerViewModel);
            }

        }
    }
}