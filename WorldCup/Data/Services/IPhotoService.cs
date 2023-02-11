using CloudinaryDotNet.Actions;

namespace WorldCup.Data.Services
{

	public interface IPhotoService
	{

		Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

		Task<DeletionResult> DeletePhotoAsync(string publicId);
	}
}