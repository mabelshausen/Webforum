using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;

namespace Forum.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            return View();
        }

        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            return View();
        }
    }
}