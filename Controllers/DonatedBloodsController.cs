using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;
using BloodProject3.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodProject3.Controllers
{
    // A controller to manage all CRUD functionality for the DonatedBlood model, including sorting, searching, and pagination.
    [Authorize(Roles = "Admin")]
    public class DonatedBloodsController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public DonatedBloodsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: DonatedBloods
        // Displays a paginated, searchable, and sortable list of donated blood records with linked details
        [HttpGet]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            // Stores the current sorting parameter
            ViewData["CurrentSort"] = sortOrder;

            // Toggles sorting values between ascending and descending for column headers
            ViewData["DonationIDSortParm"] = sortOrder == "DonationID" ? "donationid_desc" : "DonationID";
            ViewData["AppointmentIDSortParm"] = sortOrder == "AppointmentID" ? "appointmentid_desc" : "AppointmentID";
            ViewData["BloodTypeIDSortParm"] = sortOrder == "BloodTypeID" ? "bloodtypeid_desc" : "BloodTypeID";
            ViewData["DonorIDSortParm"] = sortOrder == "DonorID" ? "donorid_desc" : "DonorID";
            ViewData["CollectionDateSortParm"] = sortOrder == "CollectionDate" ? "collectiondate_desc" : "CollectionDate";
            ViewData["VolumeMLSortParm"] = sortOrder == "VolumeML" ? "volumeml_desc" : "VolumeML";
            ViewData["BloodStatusSortParm"] = sortOrder == "BloodStatus" ? "bloodstatus_desc" : "BloodStatus";

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

            // Fetches donated blood with it's linked Appointment, BloodType, and Donor records as a queryable collection
            var DonatedBloods = _context.DonatedBlood
                .Include(d => d.Appointment)
                .Include(d => d.BloodType)
                .Include(d => d.Donor)
                .AsQueryable();

            // Filters records if a search query is given, searching all fields of the DonatedBlood model
            if (!String.IsNullOrEmpty(searchString))
            {
                DonatedBloods = DonatedBloods.Where(s => s.DonationID.ToString().Contains(searchString)
                                                       || s.AppointmentID.ToString().Contains(searchString)
                                                       || s.BloodTypeID.ToString().Contains(searchString)
                                                       || s.DonorID.ToString().Contains(searchString)
                                                       || s.CollectionDate.ToString().Contains(searchString)
                                                       || s.VolumeML.ToString().Contains(searchString)
                                                       || s.BloodStatus.ToString().Contains(searchString));
            }

            // Apply ordering based on the selected column and direction
            switch (sortOrder)
            {
                case "DonationID":
                    DonatedBloods = DonatedBloods.OrderBy(s => s.DonationID);
                    break;
                case "DonorID":
                    DonatedBloods = DonatedBloods.OrderBy(s => s.DonorID);
                    break;
                case "BloodTypeID":
                    DonatedBloods = DonatedBloods.OrderBy(s => s.BloodTypeID);
                    break;
                case "CollectionDate":
                    DonatedBloods = DonatedBloods.OrderBy(s => s.CollectionDate);
                    break;
                case "VolumeML":
                    DonatedBloods = DonatedBloods.OrderBy(s => s.VolumeML);
                    break;
                case "BloodStatus":
                    DonatedBloods = DonatedBloods.OrderBy(s => s.BloodStatus);
                    break;
                default:
                    DonatedBloods = DonatedBloods.OrderBy(s => s.DonationID);
                    break;
            }

            // Split results into pages of 10 items and returns the paginated list to the view
            int pageSize = 10;
            return View(await PaginatedList<DonatedBlood>.CreateAsync(DonatedBloods.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: DonatedBloods/Details/5
        // Displays details for a single donated blood record by its ID
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            // Return a 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Find matching record including linked Donor, BloodType, and Appointment info
            var donatedBlood = await _context.DonatedBlood
                .Include(d => d.Donor)
                .Include(d => d.BloodType)
                .Include(d => d.Appointment)
                .FirstOrDefaultAsync(m => m.DonationID == id);

            if (donatedBlood == null)
            {
                return NotFound();
            }

            return View(donatedBlood);
        }

        // GET: DonatedBloods/Create
        // Displays the form for adding a new donation record and populates blood type drop-down
        [HttpGet]
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

        // POST: DonatedBloods/Create
        // Handles form submission for making a new donation record and calculates dates automatically
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DonationID,AppointmentID,BloodTypeID,DonorID,VolumeML,BloodStatus")] DonatedBlood donatedBlood)
        {
            // Exclude auto-generated dates from validation check
            ModelState.Remove("CollectionDate");
            ModelState.Remove("ExpiryDate");

            // Saves donation if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                // Sets the creation date to now and sets expiry date to 42 days out
                donatedBlood.CollectionDate = DateTime.Now;
                donatedBlood.ExpiryDate = DateTime.Now.AddDays(42);

                _context.Add(donatedBlood);
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

            return View(donatedBlood);
        }

        // GET: DonatedBloods/Edit/5
        // Displays the form to edit an existing donation record
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatedBlood = await _context.DonatedBlood.FindAsync(id);
            if (donatedBlood == null)
            {
                return NotFound();
            }

            // Populate drop-down selection list with currently selected blood type
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text", donatedBlood.BloodTypeID);

            return View(donatedBlood);
        }

        // POST: DonatedBloods/Edit/5
        // Handles saving updated donation details
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("DonationID,AppointmentID,BloodTypeID,DonorID,CollectionDate,VolumeML,ExpiryDate,BloodStatus")] DonatedBlood donatedBlood)
        {
            // Ensures ID matches the edited model ID
            if (id != donatedBlood.DonationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donatedBlood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if record was deleted by another user during edit
                    if (!DonatedBloodExists(donatedBlood.DonationID))
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
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text", donatedBlood.BloodTypeID);

            return View(donatedBlood);
        }

        // GET: DonatedBloods/Delete/5
        // Displays confirmation screen prior to deleting a record
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetches the donation record to be deleted including linked Donor, BloodType, and Appointment info
            var donatedBlood = await _context.DonatedBlood
                .Include(d => d.Donor)
                .Include(d => d.BloodType)
                .Include(d => d.Appointment)
                .FirstOrDefaultAsync(m => m.DonationID == id);

            if (donatedBlood == null)
            {
                return NotFound();
            }

            return View(donatedBlood);
        }

        // POST: DonatedBloods/Delete/5
        // Handles permanently removing a donation record
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donatedBlood = await _context.DonatedBlood.FindAsync(id);
            if (donatedBlood != null)
            {
                _context.DonatedBlood.Remove(donatedBlood);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a donation record exists in the database
        private bool DonatedBloodExists(int id)
        {
            return _context.DonatedBlood.Any(e => e.DonationID == id);
        }
    }
}