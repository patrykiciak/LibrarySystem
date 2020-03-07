using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibrarySystemContext _context;

        public BooksRepository(LibrarySystemContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsyncIncludeRentals()
        {
            return await _context.Book.Include(book => book.Rental).ToListAsync();
        }

        public async Task<List<Book>> GetAllAvailableAsync()
        {
            System.Linq.Expressions.Expression<Func<Book, bool>> isAvailable = book =>
                    !book.Rental.Where(rental => rental.BookId == book.Id
                        && (rental.EndDate > DateTime.Now || rental.EndDate == null)
                    ).Any();

            return await _context.Book.Include(book => book.Rental).Where(isAvailable).ToListAsync();
        }


        public bool IsRented(Book book)
        {
            return book.Rental.Where(
                    rental => rental.BookId == book.Id
                    && (rental.EndDate > DateTime.Now || rental.EndDate == null)
                ).Any();
        }

        public async Task<Book> GetBookAsync(int? id)
        {
            if (id == null)
                return null;

            return await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }

        public bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
