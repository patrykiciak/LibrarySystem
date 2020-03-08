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
        private readonly IRentalsService _rentalsService;
        private readonly IBooksService _booksService;
        private readonly ICustomersService _customersService;

        public RentalsController(IRentalsService rentalsService, IBooksService booksService, ICustomersService customersService)
        {
            _rentalsService = rentalsService;
            _booksService = booksService;
            _customersService = customersService;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalsService.GetAllIncludeBookAndCustomer();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _rentalsService.GetRentalIncludeBookAndCustomer((int)id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            var customers = await _customersService.GetAllAsync();
            var books = await _booksService.GetAllAvailableAsync();

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
                await _rentalsService.Add(rental);
                return RedirectToAction(nameof(Index));
            }

            var books = await _booksService.GetAllAvailableAsync();
            var customers = await _customersService.GetAllAsync();
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

            var rental = await _rentalsService.FindAsync((int)id);
            if (rental == null)
            {
                return NotFound();
            }

            var books = await _booksService.GetAllAsync();
            var customers = await _customersService.GetAllAsync();

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
                    await _rentalsService.Update(rental);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_rentalsService.RentalExists(rental.Id))
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

            var books = await _booksService.GetAllAsync();
            var customers = await _customersService.GetAllAsync();

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

            var rental = await _rentalsService.GetRentalIncludeBookAndCustomer((int)id);
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
            await _rentalsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
