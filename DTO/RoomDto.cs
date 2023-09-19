using BE_SOCIALNETWORK.Database.Contracts;

namespace BE_SOCIALNETWORK.DTO
{
    public class RoomDto : BaseModel
    {
        public string Name { get; set; }
        public int Type { get; set; }

    }
}
