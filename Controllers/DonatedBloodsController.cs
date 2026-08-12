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
    public class DonatedBloodsController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public DonatedBloodsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
         string sortOrder,
         string currentFilter,
         string searchString,
         int? pageNumber)
        {
            ViewData["DonationIDSortParm"] = sortOrder == "DonationID" ? "donationid_desc" : "DonationID";
            ViewData["AppointmentIDSortParm"] = sortOrder == "AppointmentID" ? "appointmentid_desc" : "AppointmentID";
            ViewData["BloodTypeIDSortParm"] = sortOrder == "BloodTypeID" ? "bloodtypeid_desc" : "BloodTypeID";
            ViewData["DonorIDSortParm"] = sortOrder == "DonorID" ? "donorid_desc" : "DonorID";
            ViewData["CollectionDateSortParm"] = sortOrder == "CollectionDate" ? "collectiondate_desc" : "CollectionDate";
            ViewData["VolumeMLSortParm"] = sortOrder == "VolumeML" ? "volumeml_desc" : "VolumeML";
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

            var DonatedBloods = from s in _context.DonatedBlood
                          select s;
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


            //add both a default case and a case for each column
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

            int pageSize = 10;
            return View(await PaginatedList<DonatedBlood>.CreateAsync(DonatedBloods.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: DonatedBloods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        // POST: DonatedBloods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationID,AppointmentID,BloodTypeID,DonorID,VolumeML,BloodStatus")] DonatedBlood donatedBlood)
        {
            ModelState.Remove("CollectionDate");
            ModelState.Remove("ExpiryDate");

            if (ModelState.IsValid)
            {
                donatedBlood.CollectionDate = DateTime.Now;
                donatedBlood.ExpiryDate = DateTime.Now.AddDays(42);

                _context.Add(donatedBlood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text");

            return View(donatedBlood);
        }

        // GET: DonatedBloods/Edit/5
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

            var bloodTypeList = _context.BloodType.ToList().Select(b => new SelectListItem
            {
                Value = b.BloodTypeID.ToString(),
                Text = b.SelectedBloodType.ToString()
            }).ToList();
            ViewBag.BloodTypes = new SelectList(bloodTypeList, "Value", "Text", donatedBlood.BloodTypeID);

            return View(donatedBlood);
        }

        // POST: DonatedBloods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationID,AppointmentID,BloodTypeID,DonorID,CollectionDate,VolumeML,ExpiryDate,BloodStatus")] DonatedBlood donatedBlood)
        {
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        private bool DonatedBloodExists(int id)
        {
            return _context.DonatedBlood.Any(e => e.DonationID == id);
        }
    }
}