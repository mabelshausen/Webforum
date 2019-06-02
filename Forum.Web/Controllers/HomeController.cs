using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.ViewModels;
using Forum.Web.Entities;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;

        public HomeController (IRepository<Theme> themeRepo)
        {
            _themeRepo = themeRepo;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexVm();

            model.Themes = _themeRepo.GetAll().OrderBy(t => t.Title);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
