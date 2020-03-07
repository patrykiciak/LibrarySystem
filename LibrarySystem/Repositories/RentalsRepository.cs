using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Repositories
{
    public class RentalsRepository : IRentalsRepository
    {
        private readonly LibrarySystemContext _context;

        public RentalsRepository(LibrarySystemContext context)
        {
            _context = context;
        }

        public async Task<List<Rental>> GetAllIncludeBookAndCustomer()
        {
            return await _context.Rental.Include(r => r.Book).Include(r => r.Customer).ToListAsync();
        }

        public async Task<Rental> GetRentalIncludeBookAndCustomer(int? id)
        {
            if (id == null)
                return null;

            return await _context.Rental
                .Include(r => r.Book)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Rental rental)
        {
            _context.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<Rental> FindAsync(int? id)
        {
            if (id == null)
                return null;

            return await _context.Rental.FindAsync(id);
        }

        public async Task Update(Rental rental)
        {
            _context.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rental = await _context.Rental.FindAsync(id);
            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();
        }

        public bool RentalExists(int id)
        {
            return _context.Rental.Any(e => e.Id == id);
        }
    }
}
