using BE_SOCIALNETWORK.Database.Contracts;

namespace BE_SOCIALNETWORK.Extensions
{
    public class PaginatedItems<T> where T : BaseModel
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPage { get; private set; }
        public long Count { get; private set; }
        public IEnumerable<T> Data { get;  set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPage;
        public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        }



    }
}
