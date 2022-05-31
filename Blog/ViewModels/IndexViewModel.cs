using Blog.Models;

namespace Blog.ViewModels
{
    public class IndexViewModel
	{
		public int PageNumber { get; set; }
		public int PagesCount { get; set; }
        public string SearchString { get; set; }
		public IEnumerable<int> Pages { get; set; }
        public string Category { get; set; }
        public bool CanGoNext { get; set; }
		public IEnumerable<Post> Posts { get; set; }
	}
}
