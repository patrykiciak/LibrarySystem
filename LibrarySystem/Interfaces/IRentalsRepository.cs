using LibrarySystem.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrarySystem.Interfaces
{
    public interface IRentalsRepository
    {
        Task Add(Rental rental);
        Task Delete(int id);
        Task<Rental> FindAsync(int? id);
        Task<List<Rental>> GetAllIncludeBookAndCustomer();
        Task<Rental> GetRentalIncludeBookAndCustomer(int? id);
        bool RentalExists(int id);
        Task Update(Rental rental);
    }
}