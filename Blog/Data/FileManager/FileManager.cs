using PhotoSauce.MagicScaler;

namespace Blog.Data.FileManager
{
	public class FileManager : IFileManager
	{
		private string _imagePath;

		public FileManager(IConfiguration config)
		{
			_imagePath = config["Path:Images"];
		}

        public FileStream ImageStream(string image)
        {
			return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public void RemoveImage(string image)
        {
            try
            {
				var path = Path.Combine(_imagePath, image);
				if (File.Exists(path))
					File.Delete(path);
            }
			catch(Exception e)
            {
				Console.WriteLine(e);
            }
        }

        public async Task<string> SaveImage(IFormFile image)
		{
			try
			{
				var save_path = Path.Combine(_imagePath);
				if (!Directory.Exists(save_path))
				{
					Directory.CreateDirectory(save_path);
				}
				//Path.GetExtension(image.FileName)
				var mime = Path.GetExtension(image.FileName);
				var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

				using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
				{
					MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());
				}

				return fileName;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return "Error"; 
			}
		}


		public ProcessImageSettings ImageOptions()
        {
			var settings = new ProcessImageSettings()
			{
				Width = 800,
				Height = 500,
				ResizeMode = CropScaleMode.Crop,
				EncoderOptions = new JpegEncoderOptions(100, ChromaSubsampleMode.Subsample420, false)
			};

			settings.TrySetEncoderFormat(ImageMimeTypes.Jpeg);

			return settings;
        }
	}
}