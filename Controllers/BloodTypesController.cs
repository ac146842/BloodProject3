using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;

namespace BloodProject3.Controllers
{
    // A controller to manage all CRUD functionality for the BloodType model
    public class BloodTypesController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public BloodTypesController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: BloodTypes
        // Displays a list of all available blood types
        public async Task<IActionResult> Index()
        {
            return View(await _context.BloodType.ToListAsync());
        }

        // GET: BloodTypes/Details/5
        // Displays details for a single blood type record by its ID
        public async Task<IActionResult> Details(int? id)
        {
            // Return a 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Finds the matching blood type record
            var bloodType = await _context.BloodType
                .FirstOrDefaultAsync(m => m.BloodTypeID == id);
            if (bloodType == null)
            {
                return NotFound();
            }

            return View(bloodType);
        }

        // GET: BloodTypes/Create
        // Displays the form for adding a new blood type
        public IActionResult Create()
        {
            return View();
        }

        // POST: BloodTypes/Create
        // Handles form submission to save a new blood type record to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BloodTypeID,SelectedBloodType")] BloodType bloodType)
        {
            // Saves blood type if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(bloodType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodType);
        }

        // GET: BloodTypes/Edit/5
        // Displays the form to edit an existing blood type record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodType = await _context.BloodType.FindAsync(id);
            if (bloodType == null)
            {
                return NotFound();
            }
            return View(bloodType);
        }

        // POST: BloodTypes/Edit/5
        // Handles saving updated blood type details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BloodTypeID,SelectedBloodType")] BloodType bloodType)
        {
            // Checks whether ID matches the edited model ID
            if (id != bloodType.BloodTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if record was deleted by another user during edit
                    if (!BloodTypeExists(bloodType.BloodTypeID))
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
            return View(bloodType);
        }

        // GET: BloodTypes/Delete/5
        // Displays confirmation screen prior to deleting a record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodType = await _context.BloodType
                .FirstOrDefaultAsync(m => m.BloodTypeID == id);
            if (bloodType == null)
            {
                return NotFound();
            }

            return View(bloodType);
        }

        // POST: BloodTypes/Delete/5
        // Handles permanently removing a blood type record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloodType = await _context.BloodType.FindAsync(id);
            if (bloodType != null)
            {
                _context.BloodType.Remove(bloodType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to verify if a blood type record exists in the database
        private bool BloodTypeExists(int id)
        {
            return _context.BloodType.Any(e => e.BloodTypeID == id);
        }
    }
}
