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
    public class DonatedBloodsController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public DonatedBloodsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: DonatedBloods
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonatedBlood.ToListAsync());
        }

        // GET: DonatedBloods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatedBlood = await _context.DonatedBlood
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
            return View();
        }

        // POST: DonatedBloods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationID,AppointmentID,BloodTypeID,DonorID,CollectionDate,VolumeML,ExpiryDate,BloodStatus")] DonatedBlood donatedBlood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donatedBlood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(donatedBlood);
        }

        // POST: DonatedBloods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
