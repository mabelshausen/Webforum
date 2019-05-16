using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Entities;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;

        public CategoriesController(IRepository<Theme> themeRepo, IRepository<Category> categoryRepo)
        {
            _themeRepo = themeRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index(string theme)
        {
            var model = new CategoriesIndexVm();

            model.Theme = _themeRepo.GetAll()
                .Where(t => t.Title.ToLower() == theme.ToLower())
                .First();

            model.CategoriesBytheme = _categoryRepo.GetAll()
                .Where(c => c.Theme == model.Theme)
                .ToList();

            return View(model);
        }
    }
}