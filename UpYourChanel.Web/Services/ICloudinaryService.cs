using System.IO;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;

namespace UpYourChannel.Web.Services
{
    public interface ICloudinaryService
    {
        Task<Image> UploadProfilePictureAsync(string name, Stream stream, string publicIdToDelete);
    }
}
