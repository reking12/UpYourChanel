using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System.Threading.Tasks;
using UpYourChannel.Data.Data;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private Account account;

        public CloudinaryService(string CLOUD_NAME, string API_KEY, string API_SECRET)
        {
            account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
        }

        public async Task<Image> UploadProfilePictureAsync(string name, Stream stream, string publicIdToDelete)
        {
            // think a little how can make this method better
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream)
            };
            if (publicIdToDelete != "Facebook-no-profile-picture-icon-620x389_hwyvki")
            {
                await cloudinary.DeleteResourcesAsync(publicIdToDelete);
            }
            var img = await cloudinary.UploadAsync(uploadParams);
            var image = new Image
            {
                ProfilePictureUrl = img.Uri.ToString(),
                ProfilePicturePublicId = img.PublicId
            };
            return image;
        }
    }
}
