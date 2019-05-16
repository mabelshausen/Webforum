using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.ViewModels;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private ForumContext fc;

        public HomeController (ForumContext context)
        {
            fc = context;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexVm();

            model.Themes = fc.Themes;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
