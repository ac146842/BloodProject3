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
    public class AnswersController : Controller
    {
        private readonly BloodProject3DbContext _context;

        public AnswersController(BloodProject3DbContext context)
        {
            _context = context;
        }

    // GET: Answers
    public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["QuestionsSortParm"] = sortOrder == "Questions" ? "questions_desc" : "Questions";
            ViewData["DonorSortParm"] = sortOrder == "Donor" ? "donor_desc" : "Donor";
            ViewData["AnswersIDSortParm"] = sortOrder == "AnswersID" ? "answersid_desc" : "AnswersID";
            ViewData["AnswersTextSortParm"] = sortOrder == "AnswersText" ? "answerstext_desc" : "AnswersText";
            ViewData["AnswerDateSortParm"] = sortOrder == "AnswerDate" ? "answerdate_desc" : "AnswerDate";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var answers = from s in _context.Answers
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                answers = answers.Where(s => s.HealthQID.ToString().Contains(searchString)
                                       || s.DonorID.ToString().Contains(searchString)
                                       || s.AnswersID.ToString().Contains(searchString)
                                       || s.AnswersText.Contains(searchString)
                                       || s.AnswerDate.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Questions":
                    answers = answers.OrderBy(s => s.HealthQID);
                    break;
                case "Donor":
                    answers = answers.OrderBy(s => s.DonorID);
                    break;
                case "AnswersID":
                    answers = answers.OrderBy(s => s.AnswersID);
                    break;
                case "AnswersText":
                    answers = answers.OrderBy(s => s.AnswersText);
                    break;
                case "AnswerDate":
                    answers = answers.OrderBy(s => s.AnswerDate);
                    break;
                default:
                    answers = answers.OrderBy(s => s.FormID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Answers>.CreateAsync(answers.Include(a => a.Questions).Include(a => a.Donor).AsNoTracking(), pageNumber ?? 1, pageSize));
        }



        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswersID == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswersID,FormID,HealthQID,DonorID,AnswersText,AnswerDate")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answers);
        }

        // GET: Answers/Edit/5
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswersID,FormID,HealthQID,DonorID,AnswersText,AnswerDate")] Answers answers)
        {
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .FirstOrDefaultAsync(m => m.AnswersID == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            if (answers != null)
            {
                _context.Answers.Remove(answers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswersExists(int id)
        {
            return _context.Answers.Any(e => e.AnswersID == id);
        }
    }
}