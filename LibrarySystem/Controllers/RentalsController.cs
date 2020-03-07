using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Entities;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly ICustomersRepository _customersRepository;

        public RentalsController(IRentalsRepository rentalsRepository, IBooksRepository booksRepository, ICustomersRepository customersRepository)
        {
            _rentalsRepository = rentalsRepository;
            _booksRepository = booksRepository;
            _customersRepository = customersRepository;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalsRepository.GetAllIncludeBookAndCustomer();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _rentalsRepository.GetRentalIncludeBookAndCustomer(id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var customers = await _customersRepository.GetAllAsync();
            var books = await _booksRepository.GetAllAvailableAsync();

            ViewData["BookId"] = new SelectList(books, "Id", "Title");
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,CustomerId,BookId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                await _rentalsRepository.Add(rental);
                return RedirectToAction(nameof(Index));
            }

            var books = await _booksRepository.GetAllAvailableAsync();
            var customers = await _customersRepository.GetAllAsync();
            ViewData["BookId"] = new SelectList(books, "Id", "Title", rental.BookId);
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", rental.CustomerId);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _rentalsRepository.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            var books = await _booksRepository.GetAllAvailableAsync();
            var customers = await _customersRepository.GetAllAsync();

            ViewData["BookId"] = new SelectList(books, "Id", "Title", rental.BookId);
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", rental.CustomerId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,CustomerId,BookId")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _rentalsRepository.Update(rental);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_rentalsRepository.RentalExists(rental.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var books = await _booksRepository.GetAllAvailableAsync();
            var customers = await _customersRepository.GetAllAsync();

            ViewData["BookId"] = new SelectList(books, "Id", "Title", rental.BookId);
            ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName", rental.CustomerId);
            
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _rentalsRepository.GetRentalIncludeBookAndCustomer(id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rentalsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
