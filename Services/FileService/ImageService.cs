using BlazorWebAppMovies.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorWebAppMovies.Services.FileService
{
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _webHost;
		public ImageService(IWebHostEnvironment webHost)
		{
			_webHost = webHost;
		}
		public async Task<Result<string>> UploadAsync(IBrowserFile file, string folder)
		{
			if (file == null)
				return Result<string>.Failure("file is not exist");

			long maxImageSize = 2 *1024 * 1024;
			var allowedExtentions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
			var extention = Path.GetExtension(file.Name).ToLower();

			if (file.Size > maxImageSize)
				return Result<string>.Failure("Size of image is not allowed ,Maximum is 2 mega");

			var folderPath = Path.Combine(_webHost.WebRootPath, "images", folder);
			if(!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fileName = $"{Guid.NewGuid()}{extention}";
			var fullPath = Path.Combine(folderPath, fileName);

			await using var stream = new FileStream(fullPath, FileMode.Create);
			await file.OpenReadStream(maxImageSize).CopyToAsync(stream);
			return Result<string>.Success($"images/{folder}/{fileName}");
		}
		public async Task<Result> DeleteAsync(string? imagePath)
		{
			if (string.IsNullOrEmpty(imagePath))
				return Result.Success(string.Empty); 
			var fullPath = Path.Combine(_webHost.WebRootPath, imagePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
			if(File.Exists(fullPath))
				File.Delete(fullPath);
			return  Result.Success("Image deleted successfully");

		}
	}
}
