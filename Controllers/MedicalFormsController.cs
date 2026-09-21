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
    // A controller to manage all CRUD functionality for the MedicalForms model, including sorting, searching, and pagination.
    [Authorize(Roles = "Admin")]
    public class MedicalFormsController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public MedicalFormsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: MedicalForms
        // Displays a paginated, searchable, and sortable list of medical form records, including related nurse and appointment information.
        public async Task<IActionResult> Index(
         string sortOrder,
         string currentFilter,
         string searchString,
         int? pageNumber)
        {
            // Stores the current sorting parameter
            ViewData["CurrentSort"] = sortOrder;

            // Toggles sorting values between ascending and descending for column headers
            ViewData["NurseIDSortParm"] = sortOrder == "NurseID" ? "nurseid_desc" : "NurseID";
            ViewData["AppointmentSortParm"] = sortOrder == "Appointment" ? "appointment_desc" : "Appointment";
            ViewData["FormDateSortParm"] = sortOrder == "FormDate" ? "formdate_desc" : "FormDate";

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

            // Fetches medical form records with linked Nurse and Appointment records as a queryable collection
            var MedicalForm = _context.MedicalForm
                .Include(m => m.Nurse)
                .Include(m => m.Appointment)
                    .ThenInclude(a => a.Donor)
                .AsQueryable();

            // Filters records if a search query is given, checking any relevant fields
            if (!String.IsNullOrEmpty(searchString))
            {
                MedicalForm = MedicalForm.Where(s => s.NurseID.ToString().Contains(searchString)
                                       || s.AppointmentID.ToString().Contains(searchString)
                                       || s.FormDate.ToString().Contains(searchString));
            }

            // Applys an order based off the selected column and direction
            switch (sortOrder)
            {
                case "NurseID":
                    MedicalForm = MedicalForm.OrderBy(s => s.NurseID);
                    break;
                case "nurseid_desc":
                    MedicalForm = MedicalForm.OrderByDescending(s => s.NurseID);
                    break;
                case "Appointment":
                    MedicalForm = MedicalForm.OrderBy(s => s.AppointmentID);
                    break;
                case "appointment_desc":
                    MedicalForm = MedicalForm.OrderByDescending(s => s.AppointmentID);
                    break;
                case "FormDate":
                    MedicalForm = MedicalForm.OrderBy(s => s.FormDate);
                    break;
                case "formdate_desc":
                    MedicalForm = MedicalForm.OrderByDescending(s => s.FormDate);
                    break;
                default:
                    MedicalForm = MedicalForm.OrderBy(s => s.FormID);
                    break;
            }

            // Splits the search results into pages where each page has 10 items and passes to the view
            int pageSize = 10;
            return View(await PaginatedList<MedicalForm>.CreateAsync(MedicalForm.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: MedicalForms/Details/5
        // Displays details for a single medical form record by its ID
        public async Task<IActionResult> Details(int? id)
        {
            // Returns 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Find matching medical form record including linked Nurse and Appointment info
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
        // Displays the form for adding new medical form details and populates nurse drop-down
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
        // Handles form submission to save a new medical form record to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormID,NurseID,AppointmentID,FormDate")] MedicalForm medicalForm)
        {
            // Save medical form if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(medicalForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reloads drop-down list if form submission was invalid
            var nurseList = _context.Nurse.ToList().Select(n => new SelectListItem
            {
                Value = n.NurseID.ToString(),
                Text = $"{n.FirstName} {n.LastName}"
            }).ToList();
            ViewBag.NurseList = new SelectList(nurseList, "Value", "Text");

            return View(medicalForm);
        }

        // GET: MedicalForms/Edit/5
        // Displays the form to edit an existing medical form record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalForm = await _context.MedicalForm.FindAsync(id);
            if (medicalForm == null)
            {
                return NotFound();
            }

            // Populate drop-down selection list with currently selected nurse and appointment
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
        // Handles saving updated medical form details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormID,NurseID,AppointmentID,FormDate")] MedicalForm medicalForm)
        {
            if (id != medicalForm.FormID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if record was deleted by another user during edit
                    if (!MedicalFormExists(medicalForm.FormID))
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

            ViewBag.NurseList = new SelectList(_context.Nurse.Select(n => new { Id = n.NurseID, Name = $"{n.FirstName} {n.LastName}" }), "Id", "Name", medicalForm.NurseID);
            ViewBag.AppointmentList = new SelectList(_context.Appointment.Include(a => a.Donor).Select(a => new { Id = a.AppointmentID, Name = $"{a.Donor.FirstName} {a.Donor.LastName} ({a.AppointmentDateTime.ToString("dd/MM/yyyy")})" }), "Id", "Name", medicalForm.AppointmentID);

            return View(medicalForm);
        }

        // GET: MedicalForms/Delete/5
        // Displays confirmation screen prior to deleting a record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find matching medical form record including linked Nurse and Appointment info
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
        // Handles permanently removing a medical form record
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

        // Helper method to verify if a medical form record exists in the database
        private bool MedicalFormExists(int id)
        {
            return _context.MedicalForm.Any(e => e.FormID == id);
        }
    }
}