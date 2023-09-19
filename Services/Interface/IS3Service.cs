using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Repositories.Contracts;
using BE_SOCIALNETWORK.Repositories.IRespositories;

namespace BE_SOCIALNETWORK.Services.Interface
{
    public interface IS3Service
    {
        Task<MediaDto> UploadFileToS3(IFormFile file, string subFolder);
        Task<List<MediaDto>> UploadFilesToS3(List<IFormFile> files, string subFolder);
    }
}
