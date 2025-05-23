﻿using MyModels;
using MyModels.Books;

namespace MyServices
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetBook(int id);
        Task UpdateBook(Book book);
        Task<Book> CreateBook(Book book);
        Task DeleteBook(int id);
        Task<IQueryable<Book>> GetAllBooksGrid();
        Task<GetBooksResponse> GetBooksPages(GetBooksRequest request);
    }
}
