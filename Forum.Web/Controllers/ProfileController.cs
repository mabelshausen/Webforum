using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Forum.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Controllers
{
    using Forum.Web.Constants;
    using Forum.Web.Data;
    using Forum.Web.Entities;

    public class ProfileController : Controller
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<Comment> _commentRepo;

        public ProfileController(IRepository<User> userRepo,
            IRepository<Post> postRepo,
            IRepository<Comment> commentRepo)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _commentRepo = commentRepo;
        }

        public IActionResult Index(Guid Id)
        {
            var model = new ProfileIndexVm();

            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);
            model.UserStateId = userState.UserId;
            model.IsDeleted = false;

            if (Id == Guid.Empty)
            {
                Id = userState.UserId;
            }

            model.UserId = Id;
            model.IsAdmin = userState.IsAdmin;

            var User = _userRepo.GetAll()
                .Where(u => u.Id == model.UserId)
                .FirstOrDefault() ;

            if (User == null)
            {
                model.IsDeleted = true;
                return View(model);
            }

            model.UserPosts = _postRepo.GetAll()
                .Include(p => p.Category)
                .ThenInclude(c => c.Theme)
                .Where(p => p.User == User)
                .OrderByDescending(p => p.DateTime)
                .ToList();

            model.UserComments = _commentRepo.GetAll()
                .Include(c => c.Post)
                .ThenInclude(p => p.Category)
                .ThenInclude(c => c.Theme)
                .Where(p => p.User == User)
                .OrderByDescending(p => p.DateTime)
                .ToList();

            model.Username = User.Username;

            return View(model);
        }

        public IActionResult ChangeUsername()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeUsername(ChangeNameVm model)
        {
            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);
            var user = _userRepo.GetById(userState.UserId);
            User nameCheck = null;

            try
            {
                nameCheck = _userRepo.GetAll()
                .Where(u => u.Username == model.Username)
                .First();
            }
            catch (Exception)
            {
                nameCheck = null;
            }

            if (ModelState.IsValid)
            {
                if (nameCheck != null)
                {
                    TempData[TemporaryMessage.temporaryMessage] = "There already exists a user with that username. Please enter a different username.";
                    return View(model);
                }
                else
                {
                    user.Username = model.Username;
                    _userRepo.Update(user);

                    userState.Username = model.Username;
                    HttpContext.Session.SetString(Constants.UserStatekey, JsonConvert.SerializeObject(userState));

                    TempData[TemporaryMessage.temporaryMessage] = "Username has been changed.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordVm model)
        {
            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);
            var user = _userRepo.GetById(userState.UserId);
            

            if (ModelState.IsValid)
            {
                var password = model.Password;
                var hashedPassword = PasswordHasher.Hashing(password);

                if (hashedPassword != user.Password)
                {
                    TempData[TemporaryMessage.temporaryMessage] = "Current password is wrong";
                    return View(model);
                }
                else
                {
                    user.Password = PasswordHasher.Hashing(model.NewPassword);
                    _userRepo.Update(user);

                    TempData[TemporaryMessage.temporaryMessage] = "Password has been changed.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult DeleteProfile(ProfileIndexVm model)
        {
            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);

            var user = _userRepo.GetById(userState.UserId);
            _userRepo.Delete(user);

            userState = new UserState { UserId = Guid.Empty, Username = null, IsLoggedIn = false, IsAdmin = false };
            HttpContext.Session.SetString(Constants.UserStatekey, JsonConvert.SerializeObject(userState));

            return RedirectToAction("Index", "Home");
        }
    }
}