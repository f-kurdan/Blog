using Blog.Models;
using Blog.Models.Comments;
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

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public List<Post> GetAllPosts(string category)
        {
            return _context.Posts
                .Where(x => x.Category.ToLower().Equals(category.ToLower()))
                .ToList();
        }

        public Post GetPost(int? id)
        {
            return _context.Posts
                 .Include(p => p.MainComments)
                     .ThenInclude(c => c.SubComments)
                .FirstOrDefault(x => x.ID == id);
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
    }
}