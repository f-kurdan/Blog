using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class CommentViewModel
	{
        [Required]
        public int PostID { get; set; }
        [Required]
        public int MainCommentID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
