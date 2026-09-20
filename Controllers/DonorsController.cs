using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;
using BloodProject3.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodProject3.Controllers
{
    // A controller to manage all CRUD functionality for the Donors model, including sorting, searching, and pagination.
    public class DonorsController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public DonorsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Donors
        // Displays a paginated, searchable, and sortable list of donor records, including related information.
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            // Stores the current sorting parameter
            ViewData["CurrentSort"] = sortOrder;

            // Toggles sorting values between ascending and descending for column headers
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewData["DateOfBirthSortParm"] = sortOrder == "DateOfBirth" ? "dateofbirth_desc" : "DateOfBirth";
            ViewData["BloodTypeIDSortParm"] = sortOrder == "BloodTypeID" ? "bloodtypeid_desc" : "BloodTypeID";
            ViewData["LastDonationDateSortParm"] = sortOrder == "LastDonationDate" ? "lastdonationdate_desc" : "LastDonationDate";

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

            // Fetches initial donor records as a queryable collection
            var donors = from s in _context.Donor
                         select s;

            // Filters records if a search query is entered, checks all fields including related information
            if (!String.IsNullOrEmpty(searchString))
            {
                donors = donors.Where(s => s.FirstName.Contains(searchString)
                                      || s.LastName.Contains(searchString)
                                      || s.Phone.Contains(searchString)
                                      || s.DateOfBirth.ToString().Contains(searchString)
                                      || s.BloodTypeID.ToString().Contains(searchString)
                                      || s.LastDonationDate.ToString().Contains(searchString));
            }

            // Applys an order based off the selected column and direction
            switch (sortOrder)
            {
                case "FirstName":
                    donors = donors.OrderBy(s => s.FirstName);
                    break;
                case "LastName":
                    donors = donors.OrderBy(s => s.LastName);
                    break;
                case "Phone":
                    donors = donors.OrderBy(s => s.Phone);
                    break;
                case "DateOfBirth":
                    donors = donors.OrderBy(s => s.DateOfBirth);
                    break;
                case "BloodTypeID":
                    donors = donors.OrderBy(s => s.BloodTypeID);
                    break;
                case "LastDonationDate":
                    donors = donors.OrderBy(s => s.LastDonationDate);
                    break;
                default:
                    donors = donors.OrderBy(s => s.DonorID);
                    break;
            }

            // Splits the search results into pages where each page has 10 items and passes to the view
            int pageSize = 10;
            return View(await PaginatedList<Donor>.CreateAsync(donors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Donors/Create
        // Displays the form for adding a new donor and populates blood type drop-down
        public IActionResult Create()
        {
            // Populate drop-down selection list with blood types
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text");

            return View();
        }

        // POST: Donors/Create
        // Handles form submission to save a new donor record to the database
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public async Task<IActionResult> Create([Bind("DonorID,FirstName,LastName,Phone,DateOfBirth,BloodTypeID")] Donor donor)
        {
            // Saves donor if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reloads drop-down list if form submission was invalid
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text");

            return View(donor);
        }

        // GET: Donors/Edit/5
        // Displays the form to edit an existing donor record
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Populate drop-down selection list with currently selected blood type
            var donor = await _context.Donor.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }

            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text", donor.BloodTypeID);

            return View(donor);
        }

        // POST: Donors/Edit/5
        // Handles saving updated donor details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonorID,FirstName,LastName,Phone,DateOfBirth,BloodTypeID")] Donor donor)
        {
            // Ensures ID matches the edited model ID
            if (id != donor.DonorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if record was deleted by another user during edit
                    if (!DonorExists(donor.DonorID))
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

            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text", donor.BloodTypeID);

            return View(donor);
        }

        // GET: Donors/Delete/5
        // Displays confirmation screen prior to deleting a record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Finds matching donor record including linked BloodType info
            var donor = await _context.Donor
                .Include(d => d.BloodType)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // POST: Donors/Delete/5
        // Handles permanently removing a donor record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donor = await _context.Donor.FindAsync(id);
            if (donor != null)
            {
                _context.Donor.Remove(donor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to verify if a donor record exists in the database
        private bool DonorExists(int id)
        {
            return _context.Donor.Any(e => e.DonorID == id);
        }
    }
}