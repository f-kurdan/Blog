﻿namespace Blog.ViewModels
{
	public class PostViewModel
	{
		public int? ID { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public IFormFile Image { get; set; } = null;
	}
}