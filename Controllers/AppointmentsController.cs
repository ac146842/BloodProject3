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
    public class AppointmentsController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public AppointmentsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["DonorIDSortParm"] = sortOrder == "DonorID" ? "donorid_desc" : "DonorID";
            ViewData["NurseIDSortParm"] = sortOrder == "NurseID" ? "nurseid_desc" : "NurseID";
            ViewData["AppointmentDateTimeSortParm"] = sortOrder == "AppointmentDateTime" ? "appointmentdatetime_desc" : "AppointmentDateTime";
            ViewData["LocationSortParm"] = sortOrder == "Location" ? "location_desc" : "Location";
            ViewData["TypeOfAppointmentSortParm"] = sortOrder == "TypeOfAppointment" ? "typeofappointment_desc" : "TypeOfAppointment";
            ViewData["AppointmentStatusSortParm"] = sortOrder == "AppointmentStatus" ? "appointmentstatus_desc" : "AppointmentStatus";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var appointment = from s in _context.Appointment
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                appointment = appointment.Where(s => s.DonorID.ToString().Contains(searchString)
                                       || s.NurseID.ToString().Contains(searchString)
                                       || s.AppointmentDateTime.ToString().Contains(searchString)
                                       || s.Location.ToString().Contains(searchString)
                                       || s.TypeOfAppointment.ToString().Contains(searchString)
                                       || s.AppointmentStatus.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "DonorID":
                    appointment = appointment.OrderBy(s => s.DonorID);
                    break;
                case "NurseID":
                    appointment = appointment.OrderBy(s => s.NurseID);
                    break;
                case "AppointmentDateTime":
                    appointment = appointment.OrderBy(s => s.AppointmentDateTime);
                    break;
                case "Location":
                    appointment = appointment.OrderBy(s => s.Location);
                    break;
                case "TypeOfAppointment":
                    appointment = appointment.OrderBy(s => s.TypeOfAppointment);
                    break;
                default:
                    appointment = appointment.OrderBy(s => s.AppointmentID);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Appointment>.CreateAsync(appointment.AsNoTracking(), pageNumber ?? 1, pageSize));
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