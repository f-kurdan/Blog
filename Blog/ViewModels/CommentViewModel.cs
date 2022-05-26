using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
	public class CommentViewModel
	{
        [Required]
        public int PostID { get; set; }
        [Required]
        public int MainCommentID { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
