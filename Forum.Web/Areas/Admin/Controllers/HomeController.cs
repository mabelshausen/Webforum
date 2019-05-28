using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Entities;
using Forum.Web.Models;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;

        public HomeController(IRepository<Theme> themeRepo)
        {
            _themeRepo = themeRepo;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexVm();

            model.Themes = _themeRepo.GetAll();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}