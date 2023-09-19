using Amazon.S3.Transfer;
using Amazon.S3;
using BE_SOCIALNETWORK.Services.Interface;
using static System.IO.MemoryStream;
using BE_SOCIALNETWORK.Config;
using Microsoft.Extensions.Options;
using BE_SOCIALNETWORK.DTO;

namespace BE_SOCIALNETWORK.Services
{
    public class S3Service : IS3Service
    {
        private readonly AWSSetings aWSSetings;
        public S3Service(IOptions<AWSSetings> aWSSetings)
        {
            this.aWSSetings = aWSSetings.Value;
        }
        public async Task<List<MediaDto>> UploadFilesToS3(List<IFormFile> files, string subFolder)
        {
            List<MediaDto> mediaTypeDtos = new List<MediaDto>();
            var tasks = new List<Task>();
            foreach (var file in files)
            {
                tasks.Add(Task.Run(async () => {
                    MediaDto mediaTypeDto = await UploadFileToS3(file, subFolder);
                    mediaTypeDtos.Add(mediaTypeDto);
                }));
            }
            Task t = Task.WhenAll(tasks);
            try
            {
                t.Wait();
            }
            catch { }
            return mediaTypeDtos;
        }


        public async Task<MediaDto> UploadFileToS3(IFormFile myfile, string subFolder = "")
        {
            var result = "";
            try
            {
                var s3Client = new AmazonS3Client(aWSSetings.AccessKey, aWSSetings.SecretKey, Amazon.RegionEndpoint.APSoutheast2);
                var bucketName = aWSSetings.BucketName;
                var fileNameSplit = myfile.FileName.Split(".");
                string fileName = fileNameSplit[fileNameSplit.Length - 1];
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = Guid.NewGuid() + "." + fileName;
                }
                else
                {
                    fileName = Guid.NewGuid().ToString();
                }
                var keyName = aWSSetings.DefaultFolder;
                if (!string.IsNullOrEmpty(subFolder)) { }
                    keyName = keyName + "/" + subFolder.Trim();
                keyName = keyName + "/" + fileName;

                var fs = myfile.OpenReadStream();
                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = fs,
                    ContentType = myfile.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };
                await s3Client.PutObjectAsync(request);

                result = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, keyName);
                return new MediaDto
                {
                    Src = result,
                    Type = myfile.ContentType
                };

            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
