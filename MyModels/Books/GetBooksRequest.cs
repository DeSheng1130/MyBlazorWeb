using System.Linq.Expressions;
using MyModels.DTO.Paged;

namespace MyModels.Books
{
    public class GetBooksRequest: PagedRequestBase
    {
        public Expression<Func<Book, bool>>? Search()
        {
            // 關鍵字搜尋處理：搜尋「名稱」欄位
            Expression<Func<Book, bool>>? search = null;
            if (!string.IsNullOrWhiteSpace(SearchKeyword))
            {
                string keyword = SearchKeyword.ToLower();
                search = b => b.Title.ToLower().Contains(keyword);
            }

            return search!;
        }
    }
}
