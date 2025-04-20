using MyModels.DTO.Paged;

namespace MyModels.Books
{
    public class GetBooksResponse: PagedResultBase<Book>
    {
        public GetBooksResponse(int currentPage, int pageSize, int totalCount, List<Book> dataList) {
            Initialize(currentPage, pageSize, totalCount, dataList);
        }
    }
}
