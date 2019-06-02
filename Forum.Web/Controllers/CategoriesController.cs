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
    using Forum.Web.Constants;
    using Forum.Web.Models;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public class CategoriesController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;

        public CategoriesController(IRepository<Theme> themeRepo, 
            IRepository<Category> categoryRepo)
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
                .OrderBy(c => c.Title)
                .ToList();

            TCPState tcp = new TCPState
            {
                ThemeId = model.Theme.Id
            };

            HttpContext.Session.SetString(Constants.TCPStateKey, JsonConvert.SerializeObject(tcp));

            return View(model);
        }
    }
}