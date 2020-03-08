using LibrarySystem.Entities;
using LibrarySystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrarySystem.Interfaces
{
    public interface IBooksService
    {
        Task Add(Book book);
        bool BookExists(int id);
        Task<List<Book>> GetAllAsyncIncludeRentals();
        Task<List<Book>> GetAllAvailableAsync();
        Task<Book> GetBookAsync(int? id);
        bool IsRented(Book book);
        Task Remove(int id);
        Task Update(Book book);
        Task<List<BookViewModel>> GetAllWithAvailability();
        Task<List<Book>> GetAllAsync();
    }
}