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
    public class CommentsController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<Comment> _commentRepo;

        public CommentsController(IRepository<Theme> themeRepo,
            IRepository<Category> categoryRepo,
            IRepository<Post> postRepo,
            IRepository<Comment> commentRepo)
        {
            _themeRepo = themeRepo;
            _categoryRepo = categoryRepo;
            _postRepo = postRepo;
            _commentRepo = commentRepo;
        }

        public IActionResult Index(string theme, string category, string postid)
        {
            var model = new CommentsIndexVm();

            model.Theme = _themeRepo.GetAll()
                .Where(t => t.Title.ToLower() == theme.ToLower())
                .First();

            model.Category = _categoryRepo.GetAll()
                .Where(c => c.Theme == model.Theme)
                .Where(c => c.Title.ToLower() == category.ToLower())
                .First();

            model.Post = _postRepo.GetAll()
                .Where(p => p.Category == model.Category)
                .Where(p => p.Id == Guid.Parse(postid))
                .Include(p => p.User)
                .First();

            model.Comments = _commentRepo.GetAll()
                .Where(c => c.Post == model.Post)
                .Include(c => c.User)
                .OrderBy(c => c.DateTime)
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(CommentsIndexVm model)
        {
            if (model.Content != "")
            {
                Comment comment = new Comment
                {
                    Content = model.Content,
                    DateTime = DateTime.Now,
                    User = null,
                    Post = _postRepo.GetById(Guid.Parse(model.PostId))
                };

                _commentRepo.Add(comment);
            }

            return RedirectToAction("Index", new {
                theme = model.ThemeName,
                category = model.CatName,
                postid = model.PostId
            });
        }
    }
}