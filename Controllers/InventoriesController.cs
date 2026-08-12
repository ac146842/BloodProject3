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
    public class InventoriesController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public InventoriesController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["DonationIDSortParm"] = sortOrder == "DonationID" ? "donationid_desc" : "DonationID";
            ViewData["BloodTypeIDSortParm"] = sortOrder == "BloodTypeID" ? "bloodtypeid_desc" : "BloodTypeID";
            ViewData["CurrentVolumeMLSortParm"] = sortOrder == "CurrentVolumeML" ? "currentvolumeml_desc" : "CurrentVolumeML";
            ViewData["StorageLocationSortParm"] = sortOrder == "StorageLocation" ? "storagelocation_desc" : "StorageLocation";
            ViewData["BloodStatusSortParm"] = sortOrder == "BloodStatus" ? "bloodstatus_desc" : "BloodStatus";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var inventories = from s in _context.Inventory
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                inventories = inventories.Where(s => s.DonationID.ToString().Contains(searchString)
                                       || s.BloodTypeID.ToString().Contains(searchString)
                                       || s.CurrentVolumeML.ToString().Contains(searchString)
                                       || s.StorageLocation.ToString().Contains(searchString)
                                       || s.BloodStatus.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Questions":
                    inventories = inventories.OrderBy(s => s.DonationID);
                    break;
                case "Donor":
                    inventories = inventories.OrderBy(s => s.BloodTypeID);
                    break;
                case "AnswersID":
                    inventories = inventories.OrderBy(s => s.CurrentVolumeML);
                    break;
                case "AnswersText":
                    inventories = inventories.OrderBy(s => s.StorageLocation);
                    break;
                case "AnswerDate":
                    inventories = inventories.OrderBy(s => s.BloodStatus);
                    break;
                default:
                    inventories = inventories.OrderBy(s => s.DonationID);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Inventory>.CreateAsync(inventories.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BloodBankID,DonationID,BloodTypeID,CurrentVolumeML,StorageLocation,BloodStatus")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
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

        // GET: Inventories/Edit/5
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
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypeID = new SelectList(bloodTypeList, "Value", "Text", inventory.BloodTypeID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.BloodBankID == id);
        }
    }
}