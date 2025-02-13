using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookDirectory.Data;
using BookDirectory.Models;

namespace moment3.Controllers
{
    public class LoanController : Controller
    {
        private readonly BookContext _context;

        public LoanController(BookContext context)
        {
            _context = context;
        }

        // GET: Loan
        public async Task<IActionResult> Index()
        {
            var bookContext = _context.Loan.Include(l => l.Book).Include(l => l.User);
            return View(await bookContext.ToListAsync());
        }

        // GET: Loan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            ViewBag.BookId = new SelectList(_context.Books.Where(b => b.IsAvailable), "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name"); //"Users"?
            return View();
        }

        // POST: Loan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,UserId,LoanDate,ReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                // Markera boken som utlånad
                var book = await _context.Books.FindAsync(loan.BookId);
                if (book != null)
                {
                    book.IsAvailable = false; // Boken är nu utlånad
                    _context.Update(book);
                }

                _context.Add(loan);
                await _context.SaveChangesAsync(); // Spara både lånet och bokens statusändring
                return RedirectToAction(nameof(Index));
            }

            ViewData["BookId"] = new SelectList(_context.Books.Where(b => b.IsAvailable), "Id", "Title", loan.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name", loan.UserId);
            return View(loan);
        }

        // GET: Loan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name", loan.UserId);
            return View(loan);
        }

        // POST: Loan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,UserId,LoanDate,ReturnDate")] Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loan.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name", loan.UserId);
            return View(loan);
        }

        // GET: Loan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan != null)
            {
                // Markera boken som tillgänglig igen
                var book = await _context.Books.FindAsync(loan.BookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                    _context.Update(book);
                }

                _context.Loan.Remove(loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.Id == id);
        }
    }
}
