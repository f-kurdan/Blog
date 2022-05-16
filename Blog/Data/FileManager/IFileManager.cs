﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
	public interface IFileManager
	{
		FileStream ImageStream(string image);
		Task<string> SaveImage(IFormFile image);
	}
}
