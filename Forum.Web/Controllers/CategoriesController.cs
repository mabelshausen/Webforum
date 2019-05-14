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
        public IActionResult Index()
        {
            var model = new CategoriesIndexVm();
            return View(model);
        }
    }
}