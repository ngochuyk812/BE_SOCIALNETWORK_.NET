using System.IO;
namespace BE_SOCIALNETWORK.Mapping
{
    public static class UploadMedia
    {
        public static async Task<string> UploadFileAsync(IFormFile file, string path, string type)
        {
            string randomName = Path.GetRandomFileName();
            var filePath = Path.Combine("wwwroot", path,
                     randomName + "." +type);
            var stream = File.Create(filePath);
            await file.CopyToAsync(stream);
            return path + "/" + randomName + "." + type;
        }

    }
}
