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
    public class AppointmentsController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public AppointmentsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = _context.Appointment.Include(a => a.Donor).Include(a => a.Nurse);
            return View(await appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Donor)
                .Include(a => a.Nurse)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.DonorList = new SelectList(_context.Donor.Select(d => new { Id = d.DonorID, Name = $"{d.FirstName} {d.LastName}" }), "Id", "Name");
            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new { Id = n.NurseID, Name = $"{n.FirstName} {n.LastName}" }), "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentID,DonorID,NurseID,AppointmentDateTime,Location,TypeOfAppointment,AppointmentStatus,DurationEndTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DonorList = new SelectList(_context.Donor.Select(d => new { Id = d.DonorID, Name = $"{d.FirstName} {d.LastName}" }), "Id", "Name", appointment.DonorID);
            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new { Id = n.NurseID, Name = $"{n.FirstName} {n.LastName}" }), "Id", "Name", appointment.NurseID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null) return NotFound();

            ViewBag.DonorList = new SelectList(_context.Donor.Select(d => new { Id = d.DonorID, Name = $"{d.FirstName} {d.LastName}" }), "Id", "Name", appointment.DonorID);
            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new { Id = n.NurseID, Name = $"{n.FirstName} {n.LastName}" }), "Id", "Name", appointment.NurseID);

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentID,DonorID,NurseID,AppointmentDateTime,Location,TypeOfAppointment,AppointmentStatus,DurationEndTime")] Appointment appointment)
        {
            if (id != appointment.AppointmentID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DonorList = new SelectList(_context.Donor.Select(d => new { Id = d.DonorID, Name = $"{d.FirstName} {d.LastName}" }), "Id", "Name", appointment.DonorID);
            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new { Id = n.NurseID, Name = $"{n.FirstName} {n.LastName}" }), "Id", "Name", appointment.NurseID);

            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Donor)
                .Include(a => a.Nurse)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id) => _context.Appointment.Any(e => e.AppointmentID == id);
    }
}