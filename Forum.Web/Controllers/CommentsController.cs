using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Controllers
{
    public class CommentsController : Controller
    {
        private ForumContext fc;

        public CommentsController(ForumContext context)
        {
            fc = context;
        }

        public IActionResult Index(string theme, string category, string post)
        {
            var model = new CommentsIndexVm();

            model.Theme = fc.Themes
                .Where(t => t.Title.ToLower() == theme.ToLower())
                .First();

            model.Category = fc.Categories
                .Where(c => c.Theme == model.Theme)
                .Where(c => c.Title.ToLower() == category.ToLower())
                .First();

            model.Post = fc.Posts
                .Where(p => p.Category == model.Category)
                .Where(p => p.Title.ToLower() == post.ToLower())
                .First();

            model.Comments = fc.Comments
                .Where(c => c.Post == model.Post)
                .ToList();

            return View(model);
        }
    }
}