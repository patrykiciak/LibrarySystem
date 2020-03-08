using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> GetAllAsyncIncludeRentals()
        {
            return await _booksRepository.GetAllAsyncIncludeRentals();
        }

        public async Task<List<BookViewModel>> GetAllWithAvailability()
        {
            var books = await _booksRepository.GetAllAsyncIncludeRentals();
            var viewModels = new List<BookViewModel>();
            books.ForEach(book => viewModels.Add(new BookViewModel { Book = book, IsAvailable = !IsRented(book) }));

            return viewModels;
        }

        public async Task<List<Book>> GetAllAvailableAsync()
        {
            return await _booksRepository.GetAllAvailableAsync();
        }

        public bool IsRented(Book book)
        {
            return _booksRepository.IsRented(book);
        }

        public async Task<Book> GetBookAsync(int? id)
        {
            if (id == null)
                throw new ArgumentException();

            return await _booksRepository.GetBookAsync((int)id);
        }

        public async Task Add(Book book)
        {
            await _booksRepository.Add(book);
        }

        public async Task Update(Book book)
        {
            await _booksRepository.Update(book);
        }

        public async Task Remove(int id)
        {
            await _booksRepository.Remove(id);
        }

        public bool BookExists(int id)
        {
            return _booksRepository.BookExists(id);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _booksRepository.GetAllAsync();
        }
    }
}
