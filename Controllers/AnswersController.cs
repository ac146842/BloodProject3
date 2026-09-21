using BloodProject3.Areas.Identity.Data;
using BloodProject3.Migrations;
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
    // A controller to manage all CRUD functionality for the Answers model, including sorting, searching, and pagination.
    [Authorize(Roles = "Admin")]
    public class AnswersController : Controller
    {
        // Database context field for access with stored records
        private readonly BloodProject3DbContext _context;

        // A constructor to inject the database context into the controller
        public AnswersController(BloodProject3DbContext context)
        {
            _context = context;
        }

        // GET: Answers
        // Displays a paginated, searchable, and sortable list of answer records, including related question and donor information.
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            // Stores the current sorting parameter
            ViewData["CurrentSort"] = sortOrder;

            // Toggles sorting values between ascending and descending for column headers
            ViewData["HealthQIDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "healthqid_desc" : "";
            ViewData["DonorSortParm"] = sortOrder == "Donor" ? "donor_desc" : "Donor";
            ViewData["AnswersIDSortParm"] = sortOrder == "AnswersID" ? "answersid_desc" : "AnswersID";
            ViewData["AnswersTextSortParm"] = sortOrder == "AnswersText" ? "answerstext_desc" : "AnswersText";
            ViewData["AnswerDateSortParm"] = sortOrder == "AnswerDate" ? "answerdate_desc" : "AnswerDate";

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

            // Fetches answers and joins the needed Questions and Donor records
            var answers = _context.Answers
                .Include(a => a.Questions)
                .Include(a => a.Donor)
                .AsQueryable();

            // Filters records for a search query if one is given checking every field in the Answers model, as well as the related Questions and Donor models
            if (!String.IsNullOrEmpty(searchString))
            {
                answers = answers.Where(s => (s.Questions != null && s.Questions.FormQuestions.Contains(searchString))
                                       || (s.Donor != null && (s.Donor.FirstName.Contains(searchString) || s.Donor.LastName.Contains(searchString)))
                                       || s.HealthQID.ToString().Contains(searchString)
                                       || s.DonorID.ToString().Contains(searchString)
                                       || s.AnswersID.ToString().Contains(searchString)
                                       || s.AnswersText.Contains(searchString)
                                       || s.AnswerDate.ToString().Contains(searchString));
            }

            // Applies an order based off the selected column and direction
            switch (sortOrder)
            {
                case "healthqid_desc":
                    answers = answers.OrderByDescending(s => s.HealthQID);
                    break;
                case "Donor":
                    answers = answers.OrderBy(s => s.DonorID);
                    break;
                case "donor_desc":
                    answers = answers.OrderByDescending(s => s.DonorID);
                    break;
                case "AnswersID":
                    answers = answers.OrderBy(s => s.AnswersID);
                    break;
                case "answersid_desc":
                    answers = answers.OrderByDescending(s => s.AnswersID);
                    break;
                case "AnswersText":
                    answers = answers.OrderBy(s => s.AnswersText);
                    break;
                case "answerstext_desc":
                    answers = answers.OrderByDescending(s => s.AnswersText);
                    break;
                case "AnswerDate":
                    answers = answers.OrderBy(s => s.AnswerDate);
                    break;
                case "answerdate_desc":
                    answers = answers.OrderByDescending(s => s.AnswerDate);
                    break;
                default:
                    answers = answers.OrderBy(s => s.FormID);
                    break;
            }

            // Splits the search results into pages where each page has 10 items and passes to the view
            int pageSize = 10;
            return View(await PaginatedList<Answers>.CreateAsync(answers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Answers/Details/5
        // Displays details for a single answer record by its ID
        public async Task<IActionResult> Details(int? id)
        {
            // Return a 404 error if no ID is passed
            if (id == null)
            {
                return NotFound();
            }

            // Fetches the matching records including related Questions and Donor info
            var answers = await _context.Answers
                .Include(a => a.Questions)
                .Include(a => a.Donor)
                .FirstOrDefaultAsync(m => m.AnswersID == id);

            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        // Displays the form for adding a new answer record and saves it to the database if valid
        public IActionResult Create()
        {
            // Populates a drop-down list with all available health questions
            ViewData["HealthQID"] = new SelectList(_context.Questions, "HealthQID", "FormQuestions");
            return View();
        }

        // POST: Answers/Create
        // Handles form creation to save a new answer record to the database
        public async Task<IActionResult> Create([Bind("AnswersID,FormID,HealthQID,DonorID,AnswersText,AnswerDate")] Answers answers)
        {
            // Saves answer if all user inputs pass model validation
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reloads the drop-down list if form submission was invalid
            ViewData["HealthQID"] = new SelectList(_context.Questions, "HealthQID", "FormQuestions", answers.HealthQID);
            return View(answers);
        }

        // GET: Answers/Edit/5
        // Displays the form to edit an existing answer record
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }
            ViewData["HealthQID"] = new SelectList(_context.Questions, "HealthQID", "FormQuestions", answers.HealthQID);
            return View(answers);
        }

        // POST: Answers/Edit/5
        // Handles saving updated answer details
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("AnswersID,FormID,HealthQID,DonorID,AnswersText,AnswerDate")] Answers answers)
        {
            // Ensures the ID matches the edited model ID
            if (id != answers.AnswersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if record was deleted by another user during edit
                    if (!AnswersExists(answers.AnswersID))
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
            ViewData["HealthQID"] = new SelectList(_context.Questions, "HealthQID", "FormQuestions", answers.HealthQID);
            return View(answers);
        }

        // GET: Answers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.Questions)
                .Include(a => a.Donor)
                .FirstOrDefaultAsync(m => m.AnswersID == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        // Handles permanently removing an answer record
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            if (answers != null)
            {
                _context.Answers.Remove(answers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to verify if an answer record exists in the database
        private bool AnswersExists(int id)
        {
            return _context.Answers.Any(e => e.AnswersID == id);
        }
    }
}