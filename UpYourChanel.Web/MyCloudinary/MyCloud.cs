using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace UpYourChannel.Web.MyCloudinary
{
    public class MyCloud
    {
        public const string CLOUD_NAME = "upyourchannel";
        public const string API_KEY = "121139682549889";
        public const string API_SECRET = "L9xHHNUPbpGsw9mY6jaNZfdGT9E";
        Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);

        public void Upload(string name, Stream path)
        {
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, path)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
        }
    }
}
