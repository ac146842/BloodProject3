using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;
using BloodProject3.Views;

namespace BloodProject3.Controllers
{
    // A controller to manage all CRUD functionality for the Nurses model, including sorting, searching, and pagination.
    public class NursesController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public NursesController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Nurses
        // Displays a paginated, searchable, and sortable list of nurse records.
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            // Stores the current sorting parameter
            ViewData["CurrentSort"] = sortOrder;

            // Toggles sorting values between ascending and descending for column headers
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // Resets search results back to page 1 if a new search string is entered, otherwise keeps the current filter
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Saves the current search filter
            ViewData["CurrentFilter"] = searchString;

            // Fetches nurse records as a queryable collection
            var nurses = _context.Nurse.AsQueryable();

            // Filters records if a search query is given, checking any relevant fields
            if (!String.IsNullOrEmpty(searchString))
            {
                nurses = nurses.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            // Applys an order based off the selected column and direction
            switch (sortOrder)
            {
                case "name_desc":
                    nurses = nurses.OrderByDescending(s => s.LastName);
                    break;
                default:
                    nurses = nurses.OrderBy(s => s.LastName);
                    break;
            }

            // Splits the search results into pages where each page has 10 items and passes to the view
            int pageSize = 10;
            return View(await PaginatedList<Nurse>.CreateAsync(nurses.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Nurses/Details/5
        // Displays details for a single nurse record by its ID
        public async Task<IActionResult> Details(int? id)
        {
            // Returns 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Find matching nurse record
            var nurse = await _context.Nurse
                .FirstOrDefaultAsync(m => m.NurseID == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // GET: Nurses/Create
        // Displays the form for adding new nurse details
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nurses/Create
        // Handles form submission to save a new nurse record to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NurseID,FirstName,LastName,Phone,JobRole,EmployedStartDate,LicenseNumber")] Nurse nurse)
        {
            // Save nurse if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nurse);
        }

        // GET: Nurses/Edit/5
        // Displays the form to edit an existing nurse record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }

        // POST: Nurses/Edit/5
        // Handles saving updated nurse details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NurseID,FirstName,LastName,Phone,JobRole,EmployedStartDate,LicenseNumber")] Nurse nurse)
        {
            if (id != nurse.NurseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if record was deleted by another user during edit
                    if (!NurseExists(nurse.NurseID))
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
            return View(nurse);
        }

        // GET: Nurses/Delete/5
        // Displays confirmation screen prior to deleting a record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find matching nurse record
            var nurse = await _context.Nurse
                .FirstOrDefaultAsync(m => m.NurseID == id);
            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        // POST: Nurses/Delete/5
        // Handles permanently removing a nurse record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nurse = await _context.Nurse.FindAsync(id);
            if (nurse != null)
            {
                _context.Nurse.Remove(nurse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to verify if a nurse record exists in the database
        private bool NurseExists(int id)
        {
            return _context.Nurse.Any(e => e.NurseID == id);
        }
    }
}