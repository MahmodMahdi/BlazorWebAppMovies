using BlazorWebAppMovies.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorWebAppMovies.Services.FileService
{
	public interface IImageService
	{
		Task<Result<string>> UploadAsync(IBrowserFile file, string Folder);
		Task<Result> DeleteAsync (string? imagePath);
	}
}
