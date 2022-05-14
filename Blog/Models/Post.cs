namespace Blog.Models
{
	public class Post
	{
		public int? ID { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;

		public DateTime Created { get; set; } = DateTime.Now;
	}
}