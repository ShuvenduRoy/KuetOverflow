using System;
using System.Linq;
using KuetOverflow_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KuetOverflow_ASP.NET.Controllers
{
    public class BlogController : Controller
    {
        private readonly KuetDataContext _db;
        public BlogController(KuetDataContext db)
        {
            _db = db;
        }

        public IActionResult Post(long id)
        {
            var post =_db.Posts.FirstOrDefault(x=>x.Id==id);
            return View(post);
        }

        public IActionResult Index(int page = 0)
        {
            var pageSize = 2;
            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.previousPage = previousPage;
            ViewBag.nextPage = nextPage;
            ViewBag.hasPreviousPage = previousPage >= 0;
            ViewBag.hasNextPage = nextPage < totalPages;


            var posts = _db.Posts.Skip(pageSize * page).Take(pageSize).ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPost post)
        {
            post.Author = "Shuvendu Roy";
            post.DateTime = DateTime.Today;

            _db.Posts.Add(post);
            _db.SaveChanges();

            return View();
        }
    }
}
