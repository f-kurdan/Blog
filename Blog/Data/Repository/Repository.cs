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
            var pageSize = 2;
            var skipAmount = pageSize * (pageNumber - 1);
            var allPosts = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                allPosts = allPosts.Where(x => x.Category.ToLower().Equals(category.ToLower()));
            }

            var postsCount = allPosts.Count();
            var pagesCount = (int)Math.Ceiling((double)postsCount / pageSize);
            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PagesCount = pagesCount,
                Pages = GetPageNumbers(pageNumber, pagesCount),
                Category = category,
                CanGoNext = skipAmount + pageSize < postsCount,
                Posts = allPosts
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public IEnumerable<int> GetPageNumbers(int currentPage, int pageCount)
        {
            if (pageCount <= 5)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    yield return i;
                }
            }
            else
            {
                var midPoint = currentPage < 3 ? 3
               : currentPage > pageCount - 2 ? pageCount - 2
               : currentPage;

                var lowerBound = midPoint - 2;
                var upperBound = midPoint + 2;

                if (lowerBound != 1)
                {
                    yield return 1;
                    if (lowerBound - 1 > 1)
                    {
                        yield return -1;
                    }
                }

                for (int i = lowerBound; i <= upperBound; i++)
                {
                    yield return i;
                }

                if (upperBound != pageCount)
                {
                    if (pageCount - upperBound > 1)
                    {
                        yield return -1;
                    }
                    yield return pageCount;
                }
            }
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