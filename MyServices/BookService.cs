using MyModels;
using System.Net.Http.Json;

namespace MyServices
{
    public class BookService(HttpClient client) : IBookService
    {
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await client.GetFromJsonAsync<List<Book>>("api/Books") ?? Enumerable.Empty<Book>();
        }

        public async Task<IQueryable<Book>> GetAllBooksGrid()
        {
            var books = await client.GetFromJsonAsync<List<Book>>("api/Books");
            return books?.AsQueryable() ?? Enumerable.Empty<Book>().AsQueryable();
        }

        public async Task<Book?> GetBook(int id)
        {
            try
            {
                return await client.GetFromJsonAsync<Book>($"api/Books/{id}");
            }
            catch (Exception)
            {
                return default!;
            }
        }

        public async Task UpdateBook(Book updatedBook)
        {
            var response = await client.PutAsJsonAsync($"api/books/{updatedBook.Id}", updatedBook);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Book> CreateBook(Book book)
        {
            var response = await client.PostAsJsonAsync($"api/books", book);
            response.EnsureSuccessStatusCode();
            var createdBook = await response.Content.ReadFromJsonAsync<Book>();
            if (createdBook is null)
            {
                throw new InvalidOperationException("Insert failed!"); 
            }
            return createdBook;
        }

        public async Task DeleteBook(int id)
        {
            var response = await client.DeleteAsync($"api/books/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
