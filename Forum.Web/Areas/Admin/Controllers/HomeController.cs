using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Entities;
using Forum.Web.Models;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Areas.Admin.Models;

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
            var model = new ThemeViewModel();

            model.Themes = _themeRepo.GetAll();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTheme(ThemeViewModel themeViewModel)
        {
            var NewTheme = new Theme();

            if (themeViewModel.Theme != null && themeViewModel.Description != null){
                NewTheme.Title = themeViewModel.Theme;
                NewTheme.Description = themeViewModel.Description;

                _themeRepo.Add(NewTheme);
            }
            return RedirectToAction("Index");
        }
    }
}