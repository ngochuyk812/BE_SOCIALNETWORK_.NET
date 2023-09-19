
namespace BE_SOCIALNETWORK.Payload.Request
{
    public class CreatePostRequest
    {
        public string Caption { get; set; }
        public int Layout { get; set; }
        public int Audience { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}