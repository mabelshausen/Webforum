using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Entities;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ForumContext fc;

        public CategoriesController (ForumContext context)
        {
            fc = context;
        }

        public IActionResult Index(string theme)
        {
            var model = new CategoriesIndexVm();

            model.Categories = fc.Categories;
            model.CategoriesBytheme = fc.Categories
                .Where(c => c.Theme.Title.ToLower() == theme.ToLower())
                .ToList();

            return View(model);
        }
    }
}