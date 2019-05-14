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
            return View(model);
        }
    }
}