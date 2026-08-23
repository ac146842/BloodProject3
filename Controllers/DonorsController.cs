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
    public class DonorsController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public DonorsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Donors
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewData["DateOfBirthSortParm"] = sortOrder == "DateOfBirth" ? "dateofbirth_desc" : "DateOfBirth";
            ViewData["BloodTypeIDSortParm"] = sortOrder == "BloodTypeID" ? "bloodtypeid_desc" : "BloodTypeID";
            ViewData["LastDonationDateSortParm"] = sortOrder == "LastDonationDate" ? "lastdonationdate_desc" : "LastDonationDate";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var donors = from s in _context.Donor
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                donors = donors.Where(s => s.FirstName.Contains(searchString)
                                      || s.LastName.Contains(searchString)
                                      || s.Phone.Contains(searchString)
                                      || s.DateOfBirth.ToString().Contains(searchString)
                                      || s.BloodTypeID.ToString().Contains(searchString)
                                      || s.LastDonationDate.ToString().Contains(searchString));
            }

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

            int pageSize = 10;
            return View(await PaginatedList<Donor>.CreateAsync(donors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Donors/Create
        public IActionResult Create()
        {
            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text");

            return View();
        }

        // POST: Donors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorID,FirstName,LastName,Phone,DateOfBirth,BloodTypeID")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text");

            return View(donor);
        }

        // GET: Donors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonorID,FirstName,LastName,Phone,DateOfBirth,BloodTypeID")] Donor donor)
        {
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        private bool DonorExists(int id)
        {
            return _context.Donor.Any(e => e.DonorID == id);
        }
    }
}