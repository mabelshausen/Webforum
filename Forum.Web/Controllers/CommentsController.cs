using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
        }
    }
}