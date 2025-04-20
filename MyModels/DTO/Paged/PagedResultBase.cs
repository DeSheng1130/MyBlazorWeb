namespace MyModels.DTO.Paged
{
    public class PagedResultBase<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public List<T> DataList { get; set; } = new List<T>();

        protected void Initialize(int currentPage, int pageSize, int totalCount, List<T> dataList)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            DataList = dataList ?? [];
        }
    }
}
