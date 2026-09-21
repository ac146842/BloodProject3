using BloodProject3.Areas.Identity.Data;
using BloodProject3.Models;
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
    // A controller to manage all CRUD functionality for the Questions model.
    [Authorize(Roles = "Admin")]
    public class QuestionsController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public QuestionsController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Questions
        // Displays a list of question records.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        // Displays details for a single question record by its ID
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            // Returns 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Find matching question record
            var questions = await _context.Questions
                .FirstOrDefaultAsync(m => m.HealthQID == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        // Displays the form for adding new question details
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // Handles form submission to save a new question record to the database
        [HttpPost]
        public async Task<IActionResult> Create([Bind("HealthQID,FormQuestions")] Questions questions)
        {
            // Save question if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questions);
        }

        // GET: Questions/Edit/5
        // Displays the form to edit an existing question record
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            return View(questions);
        }

        // POST: Questions/Edit/5
        // Handles saving updated question details
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("HealthQID,FormQuestions")] Questions questions)
        {
            if (id != questions.HealthQID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if record was deleted by another user during edit
                    if (!QuestionsExists(questions.HealthQID))
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
            return View(questions);
        }

        // GET: Questions/Delete/5
        // Displays confirmation screen prior to deleting a record
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find matching question record
            var questions = await _context.Questions
                .FirstOrDefaultAsync(m => m.HealthQID == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        // Handles permanently removing a question record
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions != null)
            {
                _context.Questions.Remove(questions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to verify if a question record exists in the database
        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.HealthQID == id);
        }
    }
}