using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
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

        public IActionResult Index(int pageNumber, string category)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            var vm = _repo.GetAllPosts(pageNumber, category);        
            return View(vm);
        }

        public IActionResult Post(int? id)
        {
            if (id == null)
            {
                return View(new Post());
            }

            return View(_repo.GetPost(id));
        }

        [HttpGet("/image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image)
        {
            var mime = Path.GetExtension(image).Substring(1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = vm.PostID });
            }

            if (vm.MainCommentID == 0)
            {
                var post = _repo.GetPost(vm.PostID);
                post.MainComments ??= new List<MainComment>();

                post.MainComments.Add(new MainComment()
                {
                    Message = vm.Message,
                    Created = DateTime.Now
                });

                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentID = vm.MainCommentID,
                    Message = vm.Message,
                    Created = DateTime.Now
                };
                _repo.AddSubComment(comment);
            }

            await _repo.SaveChangesAsync();

            return RedirectToAction("Post", new { id = vm.PostID });
        }
    }
}