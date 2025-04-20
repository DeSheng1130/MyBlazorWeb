namespace MyModels.DTO.Paged
{
    public class PagedResultDTO<T>: PagedResultBase<T>
    {
        public PagedResultDTO(int currentPage, int pageSize, int totalCount, List<T> dataList)
        {
            Initialize(currentPage, pageSize, totalCount, dataList);
        }
    }
}
