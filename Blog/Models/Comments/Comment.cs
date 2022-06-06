namespace Blog.Models.Comments
{
    public class Comment
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}
