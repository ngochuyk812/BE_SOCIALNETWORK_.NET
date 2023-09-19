
namespace BE_SOCIALNETWORK.Payload.Request
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }
        public int IdParent { get; set; } = -1;
        public int IdPost { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}