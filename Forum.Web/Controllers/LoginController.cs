﻿using System;
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
                    return new RedirectToActionResult("Index", "Home", null);
                }
                else
                {
                    return View(registerViewModel);
                }

            }
            else
            {
                return View(registerViewModel);
            }

        }
    }
}