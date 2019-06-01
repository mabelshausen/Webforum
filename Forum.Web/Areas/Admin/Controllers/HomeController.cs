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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Forum.Web.Areas.Admin.Controllers
{
    using Constants;

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
            model.Categories = _categoryRepo.GetAll();

            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);

            if (userState.IsAdmin)
            {
                return View(model);
            }

            return NotFound();
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
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTheme(ThemeViewModel themeViewModel)
        {
            var ThemeToUpdate = _themeRepo.GetById(Guid.Parse(themeViewModel.Theme));

            ThemeToUpdate.Title = themeViewModel.NewThemeInput;
            ThemeToUpdate.Description = themeViewModel.Description;
            TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully updated the selected theme. ";
            _themeRepo.Update(ThemeToUpdate);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveTheme(ThemeViewModel themeViewModel)
        {
            var ThemeToRemove = _themeRepo.GetById(Guid.Parse(themeViewModel.Theme));

            ThemeToRemove.IsDeleted = true;
            TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully removed the theme {ThemeToRemove.Title} from the website. ";
            _themeRepo.Delete(ThemeToRemove);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(ThemeViewModel themeViewModel)
        {
            if (ModelState.IsValid)
            {
                var NewCategory = new Category();
                NewCategory.Title = themeViewModel.Category;
                NewCategory.Theme = _themeRepo.GetById(Guid.Parse(themeViewModel.CategoryTheme));
                NewCategory.Description = themeViewModel.Description;

                var CategoryAlreadyExists = _categoryRepo.GetAll().Where(c => c.Title == NewCategory.Title).FirstOrDefault();

                if (NewCategory.Title == null)
                {
                    TempData[TemporaryMessage.temporaryMessage] = $"Something went wrong while attempting to create a new category, did you fill everything in correctly?";
                    return RedirectToAction("Index");
                }
                if (CategoryAlreadyExists == null)
                {
                    _categoryRepo.Add(NewCategory);
                    TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully created {NewCategory.Title} as a new category. ";
                }
                else { TempData[TemporaryMessage.temporaryMessage] = $@"{NewCategory.Title} already exists as a category"; }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(ThemeViewModel themeViewModel)
        {
            var CategoryToUpdate = _categoryRepo.GetById(Guid.Parse(themeViewModel.Category));

            CategoryToUpdate.Title = themeViewModel.NewCategoryInput;
            CategoryToUpdate.Description = themeViewModel.Description;
            TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully updated the selected category. ";
            _categoryRepo.Update(CategoryToUpdate);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCategory(ThemeViewModel themeViewModel)
        {
            var CategoryToRemove = _categoryRepo.GetById(Guid.Parse(themeViewModel.Category));

            CategoryToRemove.IsDeleted = true;
            TempData[TemporaryMessage.temporaryMessage] = $@"You have successfully removed the category {CategoryToRemove.Title} from the website. ";
            _categoryRepo.Delete(CategoryToRemove);

            return RedirectToAction("Index");
        }
    }
}