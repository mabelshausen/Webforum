using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.Entities;
using Forum.Web.Models;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Forum.Web.Controllers
{
    using Forum.Web.Constants;

    public class CommentsController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<Comment> _commentRepo;
        private readonly IRepository<User> _userRepo;

        public CommentsController(IRepository<Theme> themeRepo,
            IRepository<Category> categoryRepo,
            IRepository<Post> postRepo,
            IRepository<Comment> commentRepo,
            IRepository<User> userRepo)
        {
            _themeRepo = themeRepo;
            _categoryRepo = categoryRepo;
            _postRepo = postRepo;
            _commentRepo = commentRepo;
            _userRepo = userRepo;
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

            model.Comments = _commentRepo.GetAllWithDeleted()
                .Where(c => c.Post == model.Post)
                .Include(c => c.User)
                .Include(c => c.LikedComments)
                .OrderBy(c => c.DateTime)
                .ToList();

            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);
            model.IsLoggedIn = userState.IsLoggedIn;
            model.UserId = userState.UserId;

            TCPState tcp = new TCPState
            {
                ThemeId = model.Theme.Id,
                CategoryId = model.Category.Id,
                PostId = model.Post.Id
            };

            HttpContext.Session.SetString(Constants.TCPStateKey, JsonConvert.SerializeObject(tcp));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(Comment comment)
        {
            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);

            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);

            if (comment.Content != null && comment.Content.Replace(" ", "") != "")
            {
                comment.DateTime = DateTime.Now;
                comment.Post = _postRepo.GetById(tcp.PostId);
                comment.User = _userRepo.GetById(userState.UserId);

                _commentRepo.Add(comment);
            }

            return RedirectToAction("Index", new {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title,
                postid = tcp.PostId
            });
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepo.GetById(Guid.Parse(id));
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateComment(Comment comment)
        {
            if (comment == null)
            {
                return NotFound();
            }

            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);

            _commentRepo.Update(comment);

            return RedirectToAction("Index", new
            {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title,
                postid = tcp.PostId
            });
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentRepo.GetById(Guid.Parse(id));
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        public IActionResult DeleteComment(Comment comment)
        {
            if (comment == null)
            {
                return NotFound();
            }

            var commentToDelete = _commentRepo.GetById(comment.Id);
            _commentRepo.Delete(commentToDelete);

            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);

            return RedirectToAction("Index", new
            {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title,
                postid = tcp.PostId
            });
        }
    }
}