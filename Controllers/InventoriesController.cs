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
    // A controller to manage all CRUD functionality for the Inventory model, including sorting, searching, and pagination.
    public class InventoriesController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public InventoriesController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        // Displays a paginated, searchable, and sortable list of inventory records, including related blood type information.
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
            ViewData["BloodTypeIDSortParm"] = sortOrder == "BloodTypeID" ? "bloodtypeid_desc" : "BloodTypeID";
            ViewData["CurrentVolumeMLSortParm"] = sortOrder == "CurrentVolumeML" ? "currentvolumeml_desc" : "CurrentVolumeML";
            ViewData["StorageLocationSortParm"] = sortOrder == "StorageLocation" ? "storagelocation_desc" : "StorageLocation";
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

            // Fetches inventory records with linked BloodType records as a queryable collection
            var inventories = _context.Inventory
                .Include(i => i.BloodType)
                .AsQueryable();

            // Filters records if a search query is given, checking any relevant fields
            if (!String.IsNullOrEmpty(searchString))
            {
                inventories = inventories.Where(s => s.DonationID.ToString().Contains(searchString)
                                       || s.BloodTypeID.ToString().Contains(searchString)
                                       || s.CurrentVolumeML.ToString().Contains(searchString)
                                       || s.StorageLocation.ToString().Contains(searchString)
                                       || s.BloodStatus.ToString().Contains(searchString));
            }

            // Applys an order based off the selected column and direction
            switch (sortOrder)
            {
                case "DonationID":
                    inventories = inventories.OrderBy(s => s.DonationID);
                    break;
                case "donationid_desc":
                    inventories = inventories.OrderByDescending(s => s.DonationID);
                    break;
                case "BloodTypeID":
                    inventories = inventories.OrderBy(s => s.BloodTypeID);
                    break;
                case "bloodtypeid_desc":
                    inventories = inventories.OrderByDescending(s => s.BloodTypeID);
                    break;
                case "CurrentVolumeML":
                    inventories = inventories.OrderBy(s => s.CurrentVolumeML);
                    break;
                case "currentvolumeml_desc":
                    inventories = inventories.OrderByDescending(s => s.CurrentVolumeML);
                    break;
                case "StorageLocation":
                    inventories = inventories.OrderBy(s => s.StorageLocation);
                    break;
                case "storagelocation_desc":
                    inventories = inventories.OrderByDescending(s => s.StorageLocation);
                    break;
                case "BloodStatus":
                    inventories = inventories.OrderBy(s => s.BloodStatus);
                    break;
                case "bloodstatus_desc":
                    inventories = inventories.OrderByDescending(s => s.BloodStatus);
                    break;
                default:
                    inventories = inventories.OrderBy(s => s.DonationID);
                    break;
            }

            // Splits the search results into pages where each page has 10 items and passes to the view
            int pageSize = 10;
            return View(await PaginatedList<Inventory>.CreateAsync(inventories.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Inventories/Details/5
        // Displays details for a single inventory record by its ID
        public async Task<IActionResult> Details(int? id)
        {
            // Returns 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Find matching inventory record including linked BloodType info
            var inventory = await _context.Inventory
                .Include(i => i.BloodType)
                .FirstOrDefaultAsync(m => m.BloodBankID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        // Displays the form for adding new blood stock and populates blood type drop-down
        public IActionResult Create()
        {
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypeID = new SelectList(bloodTypeList, "Value", "Text");
            return View();
        }

        // POST: Inventories/Create
        // Handles form submission to save a new inventory record to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BloodBankID,DonationID,BloodTypeID,CurrentVolumeML,StorageLocation,BloodStatus")] Inventory inventory)
        {
            // Save inventory if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Reloads drop-down list if form submission was invalid
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypeID = new SelectList(bloodTypeList, "Value", "Text", inventory.BloodTypeID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        // Displays the form to edit an existing inventory record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            // Populate drop-down selection list with currently selected blood type
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypeID = new SelectList(bloodTypeList, "Value", "Text", inventory.BloodTypeID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // Handles saving updated inventory details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BloodBankID,DonationID,BloodTypeID,CurrentVolumeML,StorageLocation,BloodStatus")] Inventory inventory)
        {
            if (id != inventory.BloodBankID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if record was deleted by another user during edit
                    if (!InventoryExists(inventory.BloodBankID))
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
            ViewBag.BloodTypeID = new SelectList(bloodTypeList, "Value", "Text", inventory.BloodTypeID);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        // Displays confirmation screen prior to deleting a record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find matching inventory record including linked BloodType info
            var inventory = await _context.Inventory
                .Include(i => i.BloodType)
                .FirstOrDefaultAsync(m => m.BloodBankID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        // Handles permanently removing an inventory record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to verify if an inventory record exists in the database
        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.BloodBankID == id);
        }
    }
}