using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            var model = new CategoriesIndexVm();

            model.Categories = fc.Categories;

            return View(model);
        }
    }
}