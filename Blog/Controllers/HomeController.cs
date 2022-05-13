﻿using Blog.Data;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
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

        [HttpGet]
        public IActionResult Edit(int? id)
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

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.ID == 0)
            {
                _repo.AddPost(post);
            }

            _repo.UpdatePost(post);
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            return View(post);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
