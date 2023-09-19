using BE_SOCIALNETWORK.Database.Contracts;

namespace BE_SOCIALNETWORK.Database.Model
{
    public class Room : BaseModel
    {
        public string Name { get; set; }
        public int Type { get; set; }

    }
}
