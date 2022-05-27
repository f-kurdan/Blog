using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.ViewModels
{
	public class IndexViewModel
	{
		public double PageNumber { get; set; }
        public string Category { get; set; }
        public bool CanGoNext { get; set; }
		public IEnumerable<Post> Posts { get; set; }
	}
}
