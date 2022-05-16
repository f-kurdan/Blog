using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFileManager _fileManager;

        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int? id)
        {
            if (id == null)
            {
                return View(new Post());
            }
            var post = _repo.GetPost(id);
            if (post == null)
                return NotFound();
            return View(post);
        }

        [HttpGet("/image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = Path.GetExtension(image).Substring(1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}