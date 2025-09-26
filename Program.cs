using System.Collections.Generic;
using TourBookingApp.Models;

namespace TourBookingApp.ViewModels
{
    public class TourListViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
        public string SearchTerm { get; set; }
        public int? MinDuration { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TourBookingApp.Data;
using TourBookingApp.ViewModels;

namespace TourBookingApp.Controllers
{
    public class ToursController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 10;

        public ToursController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchTerm, int? minDuration, int page = 1)
        {
            var query = _context.Tours.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(t => t.Name.Contains(searchTerm));

            if (minDuration.HasValue && minDuration.Value > 0)
                query = query.Where(t => t.Duration > minDuration.Value);

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(t => t.Name),
                "price" => query.OrderBy(t => t.Price),
                "price_desc" => query.OrderByDescending(t => t.Price),
                "duration" => query.OrderBy(t => t.Duration),
                "duration_desc" => query.OrderByDescending(t => t.Duration),
                _ => query.OrderBy(t => t.Name),
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var tours = await query.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var vm = new TourListViewModel
            {
                Tours = tours,
                CurrentPage = page,
                TotalPages = totalPages,
                SortOrder = sortOrder,
                SearchTerm = searchTerm,
                MinDuration = minDuration
            };

            return View(vm);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TourBookingApp.Data;
using TourBookingApp.Models;

namespace TourBookingApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {
            ViewData["TourId"] = new SelectList(await _context.Tours.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingDate,CustomerName,CustomerEmail,TourId,NumberOfPeople")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tours");
            }
            ViewData["TourId"] = new SelectList(await _context.Tours.ToListAsync(), "Id", "Name", booking.TourId);
            return View(booking);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["TourId"] = new SelectList(await _context.Tours.ToListAsync(), "Id", "Name", booking.TourId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingDate,CustomerName,CustomerEmail,TourId,NumberOfPeople")] Booking booking)
        {
            if (id != booking.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tours");
            }
            ViewData["TourId"] = new SelectList(await _context.Tours.ToListAsync(), "Id", "Name", booking.TourId);
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.Include(b => b.Tour).FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Tours");
        }
    }
}
