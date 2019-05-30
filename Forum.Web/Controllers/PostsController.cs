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

    public class PostsController : Controller
    {
        private readonly IRepository<Theme> _themeRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<User> _userRepo;

        public PostsController (IRepository<Theme> themeRepo, 
            IRepository<Category> categoryRepo,
            IRepository<Post> postRepo,
            IRepository<User> userRepo)
        {
            _themeRepo = themeRepo;
            _categoryRepo = categoryRepo;
            _postRepo = postRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index(string theme, string category, string search = "")
        {
            var model = new PostsIndexVm();

            model.Theme = _themeRepo.GetAll()
                .Where(t => t.Title.ToLower() == theme.ToLower())
                .First();

            model.Category = _categoryRepo.GetAll()
                .Where(c => c.Theme == model.Theme)
                .Where(c => c.Title.ToLower() == category.ToLower())
                .First();

            if (search == "")
            {
                model.PostsByCategory = _postRepo.GetAll()
                    .Where(p => p.Category == model.Category)
                    .Include(p => p.User)
                    .Include(p => p.LikedPosts)
                    .OrderByDescending(p => p.DateTime)
                    .ToList();
            }
            else
            {
                model.PostsByCategory = _postRepo.GetAll()
                    .Where(p => p.Category == model.Category)
                    .Where(p => p.Title.ToLower().Contains(search.ToLower()))
                    .Include(p => p.User)
                    .OrderByDescending(p => p.DateTime)
                    .ToList();
            }
            
            
            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);
            model.IsLoggedIn = userState.IsLoggedIn;
            model.UserId = userState.UserId;

            TCPState tcp = new TCPState
            {
                ThemeId = model.Theme.Id,
                CategoryId = model.Category.Id
            };

            HttpContext.Session.SetString(Constants.TCPStateKey, JsonConvert.SerializeObject(tcp));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(PostsIndexVm model)
        {
            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);
            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);

            if (model.Content != null && model.Content.Replace(" ", "") != "" &&
                model.Title != null && model.Title.Replace(" ", "") != "")
            {
                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    DateTime = DateTime.Now,
                    Category = _categoryRepo.GetById(tcp.CategoryId),
                    User = _userRepo.GetById(userState.UserId)
            };

                _postRepo.Add(post);
            }

            return RedirectToAction("Index", new
            {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title
            });
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepo.GetById(Guid.Parse(id));
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(Post post)
        {
            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);

            _postRepo.Update(post);

            return RedirectToAction("Index", new
            {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title
            });
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepo.GetById(Guid.Parse(id));
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult DeletePost(Post post)
        {
            if (post == null)
            {
                return NotFound();
            }
            
            var postToDelete = _postRepo.GetById(post.Id);
            
            _postRepo.Delete(postToDelete);

            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);

            return RedirectToAction("Index", new
            {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(PostsIndexVm vm)
        {
            string sessionTCP = HttpContext.Session.GetString(Constants.TCPStateKey);
            var tcp = JsonConvert.DeserializeObject<TCPState>(sessionTCP);

            return RedirectToAction("Index", new {
                theme = _themeRepo.GetById(tcp.ThemeId).Title,
                category = _categoryRepo.GetById(tcp.CategoryId).Title,
                search = vm.SearchString
            });
        }
    }
}