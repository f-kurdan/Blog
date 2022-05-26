using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost(id);
                var vm = new PostViewModel()
                {
                    ID = post.ID,
                    Title = post.Title,
                    Body = post.Body,
                    Description = post.Description,
                    Tags = post.Tags,
                    Category = post.Category, 
                    CurrentImage = post.Image
                };
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post()
            {
                ID = vm.ID,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
            {
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                    _fileManager.RemoveImage(vm.CurrentImage);
               post.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (post.ID == 0)
                _repo.AddPost(post);
            else
                _repo.UpdatePost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index", "Panel");
            return View(vm);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index", "Panel");
        }
    }
}