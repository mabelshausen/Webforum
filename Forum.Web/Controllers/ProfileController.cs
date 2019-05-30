using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Forum.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Forum.Web.Controllers
{
    using Forum.Web.Constants;
    using Forum.Web.Data;
    using Forum.Web.Entities;

    public class ProfileController : Controller
    {
        private readonly IRepository<User> _userRepo;

        public ProfileController(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            var model = new ProfileIndexVm();

            string sessionUserState = HttpContext.Session.GetString(Constants.UserStatekey);
            var userState = JsonConvert.DeserializeObject<UserState>(sessionUserState);
            model.UserId = userState.UserId;

            var User = _userRepo.GetAll()
                .Where(u => u.Id == model.UserId)
                .First();

            model.Username = User.Username;

            return View(model);
        }

    }
}