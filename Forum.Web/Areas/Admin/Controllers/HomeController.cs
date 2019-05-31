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
using Forum.Web.Constants;

namespace Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;

        public HomeController(IRepository<Theme> themeRepo, IRepository<Category> categoryRepo)
        {
            _themeRepo = themeRepo;
            _categoryRepo = categoryRepo;
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
            if (ModelState.IsValid){
                var NewTheme = new Theme();
                NewTheme.Title = themeViewModel.Theme;
                NewTheme.Description = themeViewModel.Description;
                var ThemeAlreadyExists = _themeRepo.GetAll().Where(t => t.Title == NewTheme.Title).FirstOrDefault();

                if (NewTheme.Title == null)
                {
                    TempData[TemporaryMessage.temporaryMessage] = $"Something went wrong while attempting to create a new theme, did you fill everything in correctly?";
                    return RedirectToAction("Index");
                }

                if (ThemeAlreadyExists == null){
                    if (themeViewModel.Theme != null && themeViewModel.Description != null){
                        _themeRepo.Add(NewTheme);
                        TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully created {NewTheme.Title} as a new theme. ";  
                    }
                }
                else { TempData[TemporaryMessage.temporaryMessage] = $@"{NewTheme.Title} already exists as a theme"; }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}