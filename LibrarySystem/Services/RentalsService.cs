using LibrarySystem.Entities;
using LibrarySystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalsRepository _rentalsRepository;

        public RentalsService(IRentalsRepository rentalsRepository)
        {
            _rentalsRepository = rentalsRepository;
        }

        public async Task<List<Rental>> GetAllIncludeBookAndCustomer()
        {
            return await _rentalsRepository.GetAllIncludeBookAndCustomer();
        }

        public async Task<Rental> GetRentalIncludeBookAndCustomer(int id)
        {
            return await _rentalsRepository.GetRentalIncludeBookAndCustomer(id);
        }

        public async Task Add(Rental rental)
        {
            if (rental == null || rental.Id < 1 || rental.BookId < 1 || rental.CustomerId.Equals(Guid.Empty) || rental.StartDate == DateTime.MinValue || rental.StartDate > rental.EndDate)
                throw new ArgumentException();

            await _rentalsRepository.Add(rental);
        }

        public async Task<Rental> FindAsync(int id)
        {
            return await _rentalsRepository.FindAsync(id);
        }

        public async Task Update(Rental rental)
        {
            if (rental == null || rental.Id < 1 || rental.BookId < 1 || rental.CustomerId.Equals(Guid.Empty) || rental.StartDate == DateTime.MinValue || rental.StartDate > rental.EndDate)
                throw new ArgumentException();
            await _rentalsRepository.Update(rental);
        }

        public async Task Delete(int id)
        {
            await _rentalsRepository.Delete(id);
        }

        public bool RentalExists(int id)
        {
            return _rentalsRepository.RentalExists(id);
        }
    }
}
