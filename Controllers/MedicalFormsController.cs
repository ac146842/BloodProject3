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
    public class MedicalFormsController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public MedicalFormsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: MedicalForms
        public async Task<IActionResult> Index()
        {
            var medicalForms = _context.MedicalForm
                .Include(m => m.Nurse)
                .Include(m => m.Appointment)
                    .ThenInclude(a => a.Donor);

            return View(await medicalForms.ToListAsync());
        }

        // GET: MedicalForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalForm = await _context.MedicalForm
                .Include(m => m.Nurse)
                .Include(m => m.Appointment)
                    .ThenInclude(a => a.Donor)
                .FirstOrDefaultAsync(m => m.FormID == id);

            if (medicalForm == null)
            {
                return NotFound();
            }

            return View(medicalForm);
        }

        // GET: MedicalForms/Create
        public IActionResult Create()
        {
            var nurseList = _context.Nurse.ToList().Select(n => new SelectListItem
            {
                Value = n.NurseID.ToString(),
                Text = $"{n.FirstName} {n.LastName}"
            }).ToList();
            ViewBag.NurseList = new SelectList(nurseList, "Value", "Text");

            return View();
        }

        // POST: MedicalForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormID,NurseID,AppointmentID,FormDate")] MedicalForm medicalForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var nurseList = _context.Nurse.ToList().Select(n => new SelectListItem
            {
                Value = n.NurseID.ToString(),
                Text = $"{n.FirstName} {n.LastName}"
            }).ToList();
            ViewBag.NurseList = new SelectList(nurseList, "Value", "Text");

            return View(medicalForm);
        }

        // GET: MedicalForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medicalForm = await _context.MedicalForm.FindAsync(id);
            if (medicalForm == null) return NotFound();

            // Populate dropdowns
            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new {
                Id = n.NurseID,
                Name = $"{n.FirstName} {n.LastName}"
            }), "Id", "Name", medicalForm.NurseID);

            ViewBag.AppointmentList = new SelectList(_context.Appointment.Include(a => a.Donor).Select(a => new {
                Id = a.AppointmentID,
                Name = $"{a.Donor.FirstName} {a.Donor.LastName} ({a.AppointmentDateTime.ToString("dd/MM/yyyy")})"
            }), "Id", "Name", medicalForm.AppointmentID);

            return View(medicalForm);
        }

        // POST: MedicalForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormID,NurseID,AppointmentID,FormDate")] MedicalForm medicalForm)
        {
            if (id != medicalForm.FormID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalFormExists(medicalForm.FormID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new { Id = n.NurseID, Name = $"{n.FirstName} {n.LastName}" }), "Id", "Name", medicalForm.NurseID);
            ViewBag.AppointmentList = new SelectList(_context.Appointment.Include(a => a.Donor).Select(a => new { Id = a.AppointmentID, Name = $"{a.Donor.FirstName} {a.Donor.LastName} ({a.AppointmentDateTime.ToString("dd/MM/yyyy")})" }), "Id", "Name", medicalForm.AppointmentID);

            return View(medicalForm);
        }

        // GET: MedicalForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalForm = await _context.MedicalForm
                .Include(m => m.Nurse)
                .Include(m => m.Appointment)
                .FirstOrDefaultAsync(m => m.FormID == id);
            if (medicalForm == null)
            {
                return NotFound();
            }

            return View(medicalForm);
        }

        // POST: MedicalForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalForm = await _context.MedicalForm.FindAsync(id);
            if (medicalForm != null)
            {
                _context.MedicalForm.Remove(medicalForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalFormExists(int id)
        {
            return _context.MedicalForm.Any(e => e.FormID == id);
        }
    }
}