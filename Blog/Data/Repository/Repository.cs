using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Add(post);
        }

        public IndexViewModel GetAllPosts(int pageNumber, string category)
        {
            var pageSize = 5;
            var skipAmount = pageSize * (pageNumber - 1);
            var postsCount = _context.Posts.Count();
            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category.ToLower().Equals(category.ToLower()));
                postsCount = query.Count();
            }

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                Category = category,
                CanGoNext = skipAmount + pageSize < postsCount,
                Posts = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public Post GetPost(int? id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Posts
                 .Include(p => p.MainComments)
                     .ThenInclude(c => c.SubComments)
                .FirstOrDefault(x => x.ID == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
    }
}