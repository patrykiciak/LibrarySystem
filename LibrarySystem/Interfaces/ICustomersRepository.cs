using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrarySystem.Interfaces
{
    public interface ICustomersRepository
    {
        Task Add(Customer customer);
        bool CustomerExists(Guid id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetCustomerAsync(Guid? id);
        Task Remove(Guid id);
        Task Update(Customer customer);
    }
}