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
    public class PostsController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Post> _postRepo;

        public PostsController (IRepository<Theme> themeRepo, 
            IRepository<Category> categoryRepo,
            IRepository<Post> postRepo)
        {
            _themeRepo = themeRepo;
            _categoryRepo = categoryRepo;
            _postRepo = postRepo;
        }

        public IActionResult Index(string theme, string category)
        {
            var model = new PostsIndexVm();

            model.Theme = _themeRepo.GetAll()
                .Where(t => t.Title.ToLower() == theme.ToLower())
                .First();

            model.Category = _categoryRepo.GetAll()
                .Where(c => c.Theme == model.Theme)
                .Where(c => c.Title.ToLower() == category.ToLower())
                .First();

            model.PostsByCategory = _postRepo.GetAll()
                .Where(p => p.Category == model.Category)
                .Include(p => p.User)
                .OrderBy(p => p.DateTime)
                .ToList();

            return View(model);
        }
    }
}