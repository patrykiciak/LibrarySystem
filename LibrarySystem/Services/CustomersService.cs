using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customersRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            return await _customersRepository.GetCustomerAsync(id);
        }

        public async Task Add(Customer customer)
        {
            await _customersRepository.Add(customer);
        }

        public async Task Update(Customer customer)
        {
            await _customersRepository.Update(customer);
        }

        public async Task Remove(Guid id)
        {
            await _customersRepository.Remove(id);
        }

        public bool CustomerExists(Guid id)
        {
            return _customersRepository.CustomerExists(id);
        }
    }
}
