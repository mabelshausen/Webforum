using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class PostsController : Controller
    {
        private ForumContext fc;

        public PostsController (ForumContext context)
        {
            fc = context;
        }

        public IActionResult Index(string theme, string category)
        {
            var model = new PostsIndexVm();

            model.Theme = fc.Themes
                .Where(t => t.Title.ToLower() == theme.ToLower())
                .First();

            model.Category = fc.Categories
                .Where(c => c.Theme == model.Theme)
                .Where(c => c.Title.ToLower() == category.ToLower())
                .First();

            model.PostsByCategory = fc.Posts
                .Where(p => p.Category == model.Category)
                .ToList();

            return View(model);
        }
    }
}