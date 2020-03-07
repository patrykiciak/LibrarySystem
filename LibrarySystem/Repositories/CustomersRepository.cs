using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly LibrarySystemContext _context;

        public CustomersRepository(LibrarySystemContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> GetCustomerAsync(Guid? id)
        {
            if (id == null)
                return null;

            return await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            _context.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var customer = await GetCustomerAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public bool CustomerExists(Guid id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
